using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.NurseHistory.Abstractions;
using Plateaumed.EHR.WoundDressing.Abstractions;
using Plateaumed.EHR.Meals.Abstractions;
using Plateaumed.EHR.BedMaking.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using System.Threading;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.NurseCarePlans;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class NursingNoteQueryHandler : INursingNoteQueryHandler
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQuery;
    private readonly IAbpSession _abpSession;
    private readonly IGetPatientMedicationQueryHandler _medicationRequestQueryHandler;
    private readonly IGetPatientProceduresQueryHandler _procedureRequestQueryHandler;
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandler;
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQuery;
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandler;
    private readonly IGetPatientDischargeWithEncounterIdQueryHandler _getPatientDischargeQuery;
    private readonly IGetNurseHistoryQueryHandler _getNurseHistoryQueryHandler;
    private readonly IGetPatientWoundDressingSummaryQueryHandler _getWoundDressingQueryHandler;
    private readonly IGetPatientMealsSummaryQueryHandler _getPatientMealsSummaryQuery;
    private readonly IGetPatientBedMakingSummaryQueryHandler _getBedmakingQueryHandler;
    private readonly IGetIntakeOutputSavedHistoryQueryHandler _getIntakeOutputQueryHandler;
    private readonly IGetNurseCareSummaryQueryHandler _nurseCareQueryHandler;

    

    public NursingNoteQueryHandler(IDoctorReviewAndSaveBaseQuery basedQuery,
        IAbpSession abpSession, 
        IGetPatientMedicationQueryHandler medicationRequestQueryHandler,
        IGetPatientProceduresQueryHandler procedureRequestQueryHandler,
        IGetPatientPhysicalExamSummaryWithEncounterQueryHandler physicalExaminationQueryHandler,
        IGetReviewAndSavePatientVitalSignQueryHandler vitalSignQueryHandler,
        IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler getPhysicalExamSuggestionsQuery,
        IGetPatientDischargeWithEncounterIdQueryHandler getPatientDischargeQuery,
        IGetNurseHistoryQueryHandler getNurseHistoryQueryHandler,
        IGetPatientWoundDressingSummaryQueryHandler getWoundDressingQueryHandler,
        IGetPatientMealsSummaryQueryHandler getPatientMealsSummaryQuery,
        IGetPatientBedMakingSummaryQueryHandler getBedmakingQueryHandler,
        IGetIntakeOutputSavedHistoryQueryHandler getIntakeOutputQueryHandler,
        IGetNurseCareSummaryQueryHandler nurseCareQueryHandler)
    {
        _basedQuery = basedQuery;
        _abpSession = abpSession;
        _medicationRequestQueryHandler = medicationRequestQueryHandler;
        _procedureRequestQueryHandler = procedureRequestQueryHandler;
        _physicalExaminationQueryHandler = physicalExaminationQueryHandler;
        _vitalSignQueryHandler = vitalSignQueryHandler;
        _getPhysicalExamSuggestionsQuery = getPhysicalExamSuggestionsQuery;
        _getPatientDischargeQuery = getPatientDischargeQuery;
        _getNurseHistoryQueryHandler = getNurseHistoryQueryHandler;
        _getWoundDressingQueryHandler = getWoundDressingQueryHandler;
        _getPatientMealsSummaryQuery = getPatientMealsSummaryQuery;
        _getBedmakingQueryHandler = getBedmakingQueryHandler;
        _getIntakeOutputQueryHandler = getIntakeOutputQueryHandler;
        _nurseCareQueryHandler = nurseCareQueryHandler;
    }

    public async Task<NursingNoteDto> Handle(long staffUserId, PatientEncounter encounter)
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
        var staff = await _basedQuery.GetStaffByUserId(staffUserId);
        var job = staff.Jobs
            .FirstOrDefault(j => j.FacilityId == facilityId);

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

        var transferResult = new TransferResultDto();
        if (encounter.Status == EncounterStatusType.Transferred)
        {
            transferResult = new TransferResultDto()
            {
                PatientId = patientId,
                Status = encounter.Status,
                TimeOut = encounter.TimeOut,
                ServiceCentre = encounter.ServiceCentre,
                UnitName = encounter.Unit.DisplayName,
                FacilityName = encounter.Facility.Name,
                Ward = encounter.Ward.Name,
                WardBed = encounter.WardBed.BedNumber
            };
        }
        var summaryDto = new NursingNoteDto()
        {
            VitalSignResults = await _vitalSignQueryHandler.Handle(patientId, encounterId) ?? null,
            InvestigationResults = investigations,
            Procedures = await _procedureRequestQueryHandler.Handle((int)patientId, "RecordProcedure", null) ?? null,
            PhysicalExaminationResults = await _getPhysicalExamSuggestionsQuery.Handle(physicalExamination) ?? null,
            Prescriptions = await _medicationRequestQueryHandler.Handle((int)patientId) ?? null,
            //Miscellaneous Interventions
            NursingHistory = await _getNurseHistoryQueryHandler.Handle(patientId) ?? null,
            NurseCare = await _nurseCareQueryHandler.Handle(new NurseCarePlans.Dto.GetNurseCareRequest()
            {
                PatientId = patientId,
                EncounterId = encounterId,
            }) ?? null,
            TransferResult = transferResult,
            DischargeResult = await _getPatientDischargeQuery.Handle(patientId, encounterId) ?? null,
            WoundDressing = await _getWoundDressingQueryHandler.Handle((int)patientId) ?? null,
            Meals = await _getPatientMealsSummaryQuery.Handle((int)patientId) ?? null,
            BedMaking = await _getBedmakingQueryHandler.Handle((int)patientId,_abpSession.TenantId) ?? null,
            IntakeOutput =await _getIntakeOutputQueryHandler.Handle(patientId) ?? null,
        };

        return summaryDto;
    }
}
