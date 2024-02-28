using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Symptom.Abstractions;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class DoctorsSummaryQueryHandler : IDoctorsSummaryQueryHandler
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQuery;
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientSymptomSummaryQueryHandler _symptpomQueryHandler;
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandler;
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandler;
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQuery;
    private readonly IGetReviewAndSavePatientTypeNotesQueryHandler _getPatientTypeNoteQuery;
    private readonly IGetPatientDiagnosisWithEncounterQueryHandler _getPatientDiagnosisQuery;
    private readonly IGetPatientDischargeWithEncounterIdQueryHandler _getPatientDischargeQuery;
    private readonly IGetPlansResultQueryHandler _getPlansQueryHandler;
    
    public DoctorsSummaryQueryHandler(IDoctorReviewAndSaveBaseQuery basedQuery, 
        IAbpSession abpSession,
        IGetPatientSymptomSummaryQueryHandler symptpomQueryHandler,
        IGetPatientPhysicalExamSummaryWithEncounterQueryHandler physicalExaminationQueryHandler,
        IGetReviewAndSavePatientVitalSignQueryHandler vitalSignQueryHandler,
        IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler getPhysicalExamSuggestionsQuery,
        IGetReviewAndSavePatientTypeNotesQueryHandler getPatientTypeNoteQuery,
        IGetPatientDiagnosisWithEncounterQueryHandler getPatientDiagnosisQuery,
        IGetPatientDischargeWithEncounterIdQueryHandler getPatientDischargeQuery,
        IGetPlansResultQueryHandler getPlansQueryHandler)
    {
        _basedQuery = basedQuery;
        _abpSession = abpSession;
        _symptpomQueryHandler = symptpomQueryHandler;
        _physicalExaminationQueryHandler = physicalExaminationQueryHandler;
        _vitalSignQueryHandler = vitalSignQueryHandler;
        _getPhysicalExamSuggestionsQuery = getPhysicalExamSuggestionsQuery;
        _getPatientTypeNoteQuery = getPatientTypeNoteQuery;
        _getPatientDiagnosisQuery = getPatientDiagnosisQuery;
        _getPatientDischargeQuery = getPatientDischargeQuery;
        _getPlansQueryHandler = getPlansQueryHandler;
    }
    
    public async Task<DoctorSummaryDto> Handle(long doctorUserId, bool isOnBehalfOf, PatientEncounter encounter)
    {
        var encounterId = encounter.Id;

        var patient = encounter.Patient != null ? new GetPatientDetailsOutDto()
        {
            Id = encounter.Patient.Id,
            FullName = encounter.Patient.FullName,
            DateOfBirth = encounter.Patient.DateOfBirth,
            BloodGroup = encounter.Patient.BloodGroup,
            BloodGenotype = encounter.Patient.BloodGenotype
        } : throw new UserFriendlyException("Patient does not exist.");

        var patientId = encounter.Patient.Id;
        var facilityId = encounter.FacilityId.GetValueOrDefault();
        var staff = await _basedQuery.GetStaffByUserId(doctorUserId);
        var job = staff.Jobs
            .FirstOrDefault(j => j.FacilityId == facilityId);

        if (job?.Unit == null)
            throw new UserFriendlyException("No unit found for the selected doctor.");
        if (job?.JobLevel == null)
            throw new UserFriendlyException("No job level found for the selected doctor.");

        var encounterType = encounter.ServiceCentre == Misc.ServiceCentreType.OutPatient ? "OPD" : "WR";

        //get all presenting complaints - symptoms for the selected patient. Adjust to include encounter Id
        var symptoms = await _symptpomQueryHandler.Handle(Convert.ToInt32(patientId), _abpSession.TenantId.GetValueOrDefault(), encounterId);

        var onBehalfName = isOnBehalfOf ? $"on behalf of Dr. {staff.User.Name}" : string.Empty;
        var appointmentId = encounter.AppointmentId ?? 0;

        //Filter symptoms when entry type is suggestion
        var pComplaints = symptoms == null ? null : symptoms.Where(x => (x.CreatorUserId == _abpSession.UserId.GetValueOrDefault() ||
                                                                    x.CreatorUserId == doctorUserId) &&
                                                                    x.SymptomEntryType == Symptom.SymptomEntryType.Suggestion)
            .Select(s => new PresentingComplaintDto()
            {
                Id = s.Id,
                Note = s.SuggestionSummary,
                CreatedAt = s.CreationTime
            }).ToList();

        //get patient physical examination data
        var physicalExamination = await _physicalExaminationQueryHandler.Handle(patientId, encounterId, _abpSession.TenantId.GetValueOrDefault());
        physicalExamination = physicalExamination.Where(s => s.EncounterId == encounterId).ToList();
        
        var investigations = encounter.InvestigationResults?.Select(i => new InvestigationResultDto()
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

        var admission = GetAdmission(encounter);
        //Produce final object with all data
        var summaryDto = new DoctorSummaryDto()
        {
            DoctorUserId = doctorUserId,
            SummaryHeader = $"{job.Unit.ShortName} {job.JobLevel.Name} {encounterType} {onBehalfName}",
            DoctorNote = new DoctorNoteDto()
            {
                Title = await _basedQuery.GetNoteTitle(patientId, encounterId, _abpSession.TenantId.GetValueOrDefault()),
                Summary = await _basedQuery.GenerateNoteIntroduction(patient, symptoms, encounter.UnitId, facilityId, 
                                                _abpSession.TenantId.GetValueOrDefault(), _abpSession.UserId.GetValueOrDefault()),
                PresentingComplaintResults = pComplaints,
                TypeNoteResults = await _getPatientTypeNoteQuery.Handle(doctorUserId, symptoms, physicalExamination) ?? null,
                GeneralPhysicalExaminationResults = await _getPhysicalExamSuggestionsQuery.Handle(physicalExamination) ?? null,
                VitalSignResults = await _vitalSignQueryHandler.Handle(patientId, encounterId) ?? null,
                InvestigationResults = investigations,
                PlanResults = await _getPlansQueryHandler.Handle(patientId, encounterId, admission),
                DiagnosisResults = await _getPatientDiagnosisQuery.Handle(patientId, encounterId) ?? null,
                DischargeResult = await _getPatientDischargeQuery.Handle(patientId, encounterId) ?? null
            }
        };
        
        return summaryDto;
    }

    private AdmitPatientRequest GetAdmission(PatientEncounter encounter)
    {
        var admissionInfo = new AdmitPatientRequest()
        {
            UnitId = encounter?.UnitId,
            Unit = new OrganizationUnitDto()
            {
                DisplayName = encounter?.Unit?.DisplayName,
                ShortName = encounter?.Unit?.ShortName,
                Code = encounter?.Unit?.Code,
            },
            WardId = encounter?.WardId,
            Ward = new WardDto()
            {
                Name = encounter?.Ward?.Name,
                Description = encounter?.Ward?.Description
            },
            WardBedId = encounter?.WardBedId,
            WardBed = new WardBedDto()
            {
                BedTypeName = encounter?.WardBed?.BedType?.Name
            },
            AttendingPhysicianId = encounter.Admission?.AttendingPhysicianId,
            AttendingPhysician = new StaffMemberDto()
            {
                StaffCode = encounter?.Admission?.AttendingPhysician?.StaffCode,
                Title = encounter?.Admission?.AttendingPhysician?.UserFk?.Title,
                Name = encounter?.Admission?.AttendingPhysician?.UserFk?.Name,
                Surname = encounter?.Admission?.AttendingPhysician?.UserFk?.Surname,
                MiddleName = encounter?.Admission?.AttendingPhysician?.UserFk?.MiddleName
            },
            ServiceCentre = encounter.ServiceCentre,
            EncounterId = encounter?.Id
        };


        return admissionInfo;
    }
}