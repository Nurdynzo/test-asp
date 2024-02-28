using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Symptom.Abstractions;
namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class EncounterSummaryQueryHandler : IEncounterSummaryQueryHandler
{
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientSymptomSummaryQueryHandler _symptpomQueryHandler;
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandler;
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandler;
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQuery;
    private readonly IGetPatientDiagnosisWithEncounterQueryHandler _getPatientDiagnosisQuery;
    private readonly IGetPatientDischargeWithEncounterIdQueryHandler _getPatientDischargeQuery;
    private readonly IGetStaffMemberByUserIdQueryHandler _getStaffMember;



    public EncounterSummaryQueryHandler(IAbpSession abpSession,
        IGetPatientSymptomSummaryQueryHandler symptpomQueryHandler,
        IGetPatientPhysicalExamSummaryWithEncounterQueryHandler physicalExaminationQueryHandler,
        IGetReviewAndSavePatientVitalSignQueryHandler vitalSignQueryHandler,
        IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler getPhysicalExamSuggestionsQuery,
        IGetPatientDiagnosisWithEncounterQueryHandler getPatientDiagnosisQuery,
        IGetPatientDischargeWithEncounterIdQueryHandler getPatientDischargeQuery,
        IGetStaffMemberByUserIdQueryHandler getStaffMember)
    {
        _abpSession = abpSession;
        _symptpomQueryHandler = symptpomQueryHandler;
        _physicalExaminationQueryHandler = physicalExaminationQueryHandler;
        _vitalSignQueryHandler = vitalSignQueryHandler;
        _getPhysicalExamSuggestionsQuery = getPhysicalExamSuggestionsQuery;
        _getPatientDiagnosisQuery = getPatientDiagnosisQuery;
        _getPatientDischargeQuery = getPatientDischargeQuery;
        _getStaffMember = getStaffMember;
    }

    public async Task<EncounterSummaryDto> Handle(long encounterId, PatientEncounter encounter)
    {
        var staffUserId = encounter.CreatorUserId.Value;
        var staff = await _getStaffMember.Handle(staffUserId);
        var staffDisplayName = $"{staff?.DisplayName}";

        var patient = encounter.Patient != null ? new GetPatientDetailsOutDto()
        {
            Id = encounter.Patient.Id,
            FullName = encounter.Patient.FullName,
            DateOfBirth = encounter.Patient.DateOfBirth,
            BloodGroup = encounter.Patient.BloodGroup,
            BloodGenotype = encounter.Patient.BloodGenotype
        } : throw new UserFriendlyException("Patient does not exist.");

        //get all presenting complaints - symptoms for the selected patient. Adjust to include encounter Id
        var symptoms = await _symptpomQueryHandler.Handle(Convert.ToInt32(patient.Id), 
                                            _abpSession.TenantId.GetValueOrDefault(), 
                                            encounterId);

        //Filter symptoms when entry type is suggestion
        var pComplaints = symptoms == null ? null : symptoms.Where(x => x.CreatorUserId == staffUserId &&
                                                                    x.SymptomEntryType == Symptom.SymptomEntryType.Suggestion)
            .Select(s => s.SuggestionSummary).ToList();

        //get patient physical examination data
        var physicalExamination = await _physicalExaminationQueryHandler.Handle(patient.Id, encounterId, _abpSession.TenantId.GetValueOrDefault());

        var investigations = encounter?.InvestigationResults?.Select(i => new InvestigationResultDto()
        {
            Id = i.Id,
            InvestigationId = i.InvestigationId,
            InvestigationRequestId = i.InvestigationRequestId,
            Name = i.Name,
            Reference = i.Reference,
            SampleCollectionDate = i.SampleCollectionDate,
            ResultDate = i.ResultDate,
            SampleTime = i.SampleTime,
            ResultTime = i.ResultTime,
            Specimen = i.Specimen,
            Conclusion = i.Conclusion,
            SpecificOrganism = i.SpecificOrganism,
            View = i.View,
            Notes = i.Notes
        }).ToList() ?? null;

        //Produce final object with all data
        var summaryDto = new EncounterSummaryDto()
        {
            DoctorUserId = staffUserId,
            Title = $"Seen by {staffDisplayName}",
            Summary = pComplaints,
            GeneralPhysicalExaminationResults = await _getPhysicalExamSuggestionsQuery.Handle(physicalExamination) ?? null,
            VitalSignResults = await _vitalSignQueryHandler.Handle(patient.Id, encounterId) ?? null,
            InvestigationResults = investigations,
            DiagnosisResults = await _getPatientDiagnosisQuery.Handle(patient.Id, encounterId) ?? null,
            DischargeResult = await _getPatientDischargeQuery.Handle(patient.Id, encounterId) ?? null
        };

        return summaryDto;
    }
}