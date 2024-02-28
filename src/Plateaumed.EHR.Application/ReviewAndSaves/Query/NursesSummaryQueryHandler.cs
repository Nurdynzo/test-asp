using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class NursesSummaryQueryHandler : INursesSummaryQueryHandler
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQuery;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<NursingCareSummary, long> _nurseCarreRepository;
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandler;
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandler;
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQuery;

    public NursesSummaryQueryHandler(IDoctorReviewAndSaveBaseQuery basedQuery,
        IAbpSession abpSession,
        IRepository<NursingCareSummary, long> nurseCarreRepository,
        IGetPatientPhysicalExamSummaryWithEncounterQueryHandler physicalExaminationQueryHandler,
        IGetReviewAndSavePatientVitalSignQueryHandler vitalSignQueryHandler,
        IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler getPhysicalExamSuggestionsQuery)
    {
        _basedQuery = basedQuery;
        _nurseCarreRepository = nurseCarreRepository;
        _abpSession = abpSession;
        _physicalExaminationQueryHandler = physicalExaminationQueryHandler;
        _vitalSignQueryHandler = vitalSignQueryHandler;
        _getPhysicalExamSuggestionsQuery = getPhysicalExamSuggestionsQuery;
    }

    public async Task<NurseSummaryDto> Handle(long encounterId)
    {
        if (encounterId == 0)
            throw new UserFriendlyException("Encounter Id is required.");

        var nurseEncounter = await _nurseCarreRepository
            .GetAll()
            .Include(s => s.Encounter)
            .ThenInclude(s => s.Patient)
            .FirstOrDefaultAsync(encounter => encounter.EncounterId == encounterId);

        if(nurseEncounter == null)
            return null;

        var facilityId = nurseEncounter?.Encounter?.FacilityId.GetValueOrDefault();

        var staffUserId = nurseEncounter?.CreatorUserId;
        var staff = await _basedQuery.GetStaffByUserId(staffUserId.GetValueOrDefault());
        var job = staff.Jobs
            .FirstOrDefault(j => j.FacilityId == facilityId);

        var staffDisplayName = $"{staff?.User?.DisplayName}";

        var encounter = nurseEncounter?.Encounter ??
            throw new UserFriendlyException("No patient encounter found on nurse care summary.");

        var patient = encounter.Patient != null ? new GetPatientDetailsOutDto()
        {
            Id = encounter.Patient.Id,
            FullName = encounter.Patient.FullName,
            DateOfBirth = encounter.Patient.DateOfBirth,
            BloodGroup = encounter.Patient.BloodGroup,
            BloodGenotype = encounter.Patient.BloodGenotype
        } : throw new UserFriendlyException("Patient does not exist.");

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
        var summaryDto = new NurseSummaryDto()
        {
            Title = $"Seen by {staffDisplayName}",
            NursingDiagnosisText = nurseEncounter.NursingDiagnosisText,
            NursingEvaluationText = nurseEncounter.NursingEvaluationText,
            GeneralPhysicalExaminationResults = await _getPhysicalExamSuggestionsQuery.Handle(physicalExamination) ?? null,
            VitalSignResults = await _vitalSignQueryHandler.Handle(patient.Id, encounterId) ?? null,
            InvestigationResults = investigations
        };

        return summaryDto;
    }
}
