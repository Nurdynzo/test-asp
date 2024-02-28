using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Encounters.Abstractions;
using Plateaumed.EHR.Authorization.Users;
using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using System;
using Plateaumed.EHR.Symptom.Abstractions;
using Plateaumed.EHR.ReferAndConsults.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.Staff;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Diagnoses.Abstraction;

namespace Plateaumed.EHR.ReferAndConsults.Handlers;

public class GetReferralLetterQueryHandler : IGetReferralLetterQueryHandler
{
    private readonly IReferAndConsultBasedQuery _basedQuery;
    private readonly IGetPatientEncounterQueryHandler _patientEncounterQueryHandler;
    private readonly IGetStaffMemberWithUnitAndLevelQueryHandler _staffQueryHandler;
    private readonly IGetPatientSymptomSummaryQueryHandler _symptpomQueryHandler;
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandler;
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQuery;
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignReviewQueryHandler;
    private readonly IGetPatientDiagnosisWithEncounterQueryHandler _getPatientDiagnosisQuery;

    public GetReferralLetterQueryHandler(IReferAndConsultBasedQuery basedQuery,
        IGetPatientEncounterQueryHandler patientEncounterQueryHandler,
        IGetStaffMemberWithUnitAndLevelQueryHandler staffQueryHandler,
        IGetPatientSymptomSummaryQueryHandler symptpomQueryHandler,
        IAbpSession abpSession,
        IGetPatientPhysicalExamSummaryWithEncounterQueryHandler physicalExaminationQueryHandler,
        IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler getPhysicalExamSuggestionsQuery,
        IGetReviewAndSavePatientVitalSignQueryHandler vitalSignReviewQueryHandler,
        IGetPatientDiagnosisWithEncounterQueryHandler getPatientDiagnosisQuery
        )
    {
        _basedQuery = basedQuery;
        _patientEncounterQueryHandler = patientEncounterQueryHandler;
        _staffQueryHandler = staffQueryHandler;
        _symptpomQueryHandler = symptpomQueryHandler;
        _abpSession = abpSession;
        _physicalExaminationQueryHandler = physicalExaminationQueryHandler;
        _getPhysicalExamSuggestionsQuery = getPhysicalExamSuggestionsQuery;
        _vitalSignReviewQueryHandler = vitalSignReviewQueryHandler;
        _getPatientDiagnosisQuery = getPatientDiagnosisQuery;
    }

    public async Task<ReferralReturnDto> Handle(ReferralRequestDto request, User loginUser)
    {
        if (request.EncounterId == 0)
            throw new UserFriendlyException("User Id is required.");

        var encounter = await _patientEncounterQueryHandler.Handle(request.EncounterId) ??
             throw new UserFriendlyException("No patient encounter found.");

        var patient = encounter.Patient ?? throw new UserFriendlyException("Patient does not exist.");
        var facilityId = encounter.FacilityId.GetValueOrDefault();


        var ids = new EntityDto<long>();
        ids.Id = loginUser.Id;
        var staffMembers = await _staffQueryHandler.Handle(ids);

        var job = staffMembers.Jobs
            .FirstOrDefault(j => j.FacilityId == facilityId);

        if (job?.Unit == null)
            throw new UserFriendlyException("No unit found for the this user.");
        var unit = job.Unit;


        //get all presenting complaints - symptoms for the selected patient. Adjust to include encounter Id
        var symptoms = await _symptpomQueryHandler.Handle(Convert.ToInt32(patient.Id), _abpSession.TenantId.GetValueOrDefault());

        //get patient physical examination data
        var physicalExamination = await _physicalExaminationQueryHandler.Handle(patient.Id, request.EncounterId, _abpSession.TenantId.GetValueOrDefault());
        var physicalExaminationSuggestions = await _getPhysicalExamSuggestionsQuery.Handle(physicalExamination);
        var physicalExaminationSuggestionAnswers = $"On examination, {patient.DisplayName} is ";
        foreach (var item in physicalExaminationSuggestions)
        {
            physicalExaminationSuggestionAnswers = $"{physicalExaminationSuggestionAnswers} {item.Answer}";
        }

        //get patient vital sign reading data
        var vitalSignSuggestions = await _vitalSignReviewQueryHandler.Handle(patient.Id, request.EncounterId);
        var referalLetter = new ReferralReturnDto()
        {

            OriginatingUnit = encounter?.Unit?.DisplayName ?? "",
            OriginatingConsultant = staffMembers?.User?.DisplayName ?? "",
            PatientName = patient.DisplayName,
            PatientAge = $"{new DateTime(DateTime.Now.Subtract(patient.DateOfBirth).Ticks).Year - 1} yrs",
            PatientGender = patient.GenderType == GenderType.Male ? "Male" : "Female",
            PatientID = patient.IdentificationCode,
            IssuingDoctorName = loginUser.DisplayName,
            SummaryNotes = _basedQuery.GenerateSummary(patient, symptoms, loginUser.Id),
            PhysicalExaminationNotes = physicalExaminationSuggestionAnswers,
            VitalSignNotes = vitalSignSuggestions,
            Diagnosis = await _getPatientDiagnosisQuery.Handle(patient.Id, request.EncounterId) ?? null,
            ReceivingHospital = request.ReceivingHospital,
            ReceivingUnit = request.ReceivingUnit,
            ReceivingConsultant = request.ReceivingConsultant
        };

        return referalLetter;
    }

}
