using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Query;
using Abp.Runtime.Session;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.BedMaking.Abstractions;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.Meals.Abstractions;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.NurseHistory.Abstractions;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.WoundDressing.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using System.Collections.Generic;
using Plateaumed.EHR.NurseHistory.Dtos;
using Plateaumed.EHR.WoundDressing.Dtos;
using Plateaumed.EHR.Meals.Dtos;
using Plateaumed.EHR.BedMaking.Dtos;
using System.Linq.Dynamic.Core;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class GetNursingRecordQueryHandlerTest
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQueryMock = Substitute.For<IDoctorReviewAndSaveBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IGetPatientMedicationQueryHandler _medicationRequestQueryHandlerMock = Substitute.For<IGetPatientMedicationQueryHandler>();
    private readonly IGetPatientProceduresQueryHandler _procedureRequestQueryHandlerMock = Substitute.For<IGetPatientProceduresQueryHandler>();
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandlerMock = Substitute.For<IGetPatientPhysicalExamSummaryWithEncounterQueryHandler>();
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQueryMock = Substitute.For<IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler>();
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandlerMock = Substitute.For<IGetReviewAndSavePatientVitalSignQueryHandler>();
    private readonly IGetPatientDischargeWithEncounterIdQueryHandler _getPatientDischargeQueryMock = Substitute.For<IGetPatientDischargeWithEncounterIdQueryHandler>();
    private readonly IGetNurseHistoryQueryHandler _getNurseHistoryQueryHandlerMock = Substitute.For<IGetNurseHistoryQueryHandler>();
    private readonly IGetPatientWoundDressingSummaryQueryHandler _getWoundDressingQueryHandlerMock = Substitute.For<IGetPatientWoundDressingSummaryQueryHandler>();
    private readonly IGetPatientMealsSummaryQueryHandler _getPatientMealsSummaryQueryMock = Substitute.For<IGetPatientMealsSummaryQueryHandler>();
    private readonly IGetPatientBedMakingSummaryQueryHandler _getBedmakingQueryHandlerMock = Substitute.For<IGetPatientBedMakingSummaryQueryHandler>();
    private readonly IGetIntakeOutputSavedHistoryQueryHandler _getIntakeOutputQueryHandlerMock = Substitute.For<IGetIntakeOutputSavedHistoryQueryHandler>();
    private readonly IGetNurseCareSummaryQueryHandler _nurseCareQueryHandlerMock = Substitute.For<IGetNurseCareSummaryQueryHandler>();

    [Fact]
    public async Task NursingNoteQueryHandler_Handle_With_Successful_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        _abpSessionMock.TenantId.Returns(1);
        var patientEncounter = Util.Common.GetPatientEncounters(patientId, true).FirstOrDefault();
        var staff = _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _medicationRequestQueryHandlerMock.Handle((int)patientId).ReturnsForAnyArgs(new List<PatientMedicationForReturnDto>());
        _getNurseHistoryQueryHandlerMock.Handle(patientId).ReturnsForAnyArgs(new List<NurseHistoryResponseDto>());
        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
           .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));
        _getWoundDressingQueryHandlerMock.Handle((int)patientId).ReturnsForAnyArgs(new List<WoundDressingSummaryForReturnDto>());
        _getPatientMealsSummaryQueryMock.Handle((int)patientId).ReturnsForAnyArgs(new List<MealsSummaryForReturnDto>());
        _getBedmakingQueryHandlerMock.Handle((int)patientId, 1).ReturnsForAnyArgs(new List<PatientBedMakingSummaryForReturnDto>());
        _getIntakeOutputQueryHandlerMock.Handle(patientId).ReturnsForAnyArgs(new List<IntakeOutputs.Dtos.PatientIntakeOutputDto>());
        _nurseCareQueryHandlerMock.Handle(new GetNurseCareRequest()
        {
            PatientId = patientId,
            EncounterId = encounterId,
        }).ReturnsForAnyArgs(new List<GetNurseCareSummaryResponse>());

        var handler = new NursingNoteQueryHandler(_basedQueryMock,
        _abpSessionMock,
        _medicationRequestQueryHandlerMock,
        _procedureRequestQueryHandlerMock,
        _physicalExaminationQueryHandlerMock,
        _vitalSignQueryHandlerMock,
        _getPhysicalExamSuggestionsQueryMock,
        _getPatientDischargeQueryMock,
        _getNurseHistoryQueryHandlerMock,
        _getWoundDressingQueryHandlerMock,
        _getPatientMealsSummaryQueryMock,
        _getBedmakingQueryHandlerMock,
        _getIntakeOutputQueryHandlerMock,
        _nurseCareQueryHandlerMock);

        //Act
        var result = await handler.Handle(staffUserId, patientEncounter);
        long resultNo = result == null ? 0 : result.VitalSignResults.Count;
        //Assert
        resultNo.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task NursingNoteQueryHandler_Handle_With_No_Patient_Encounter_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        _abpSessionMock.TenantId.Returns(1);
        var patientEncounter = Util.Common.GetPatientEncounters(patientId, false).FirstOrDefault();
        var staff = _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _medicationRequestQueryHandlerMock.Handle((int)patientId).ReturnsForAnyArgs(new List<PatientMedicationForReturnDto>());
        _getNurseHistoryQueryHandlerMock.Handle(patientId).ReturnsForAnyArgs(new List<NurseHistoryResponseDto>());
        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
           .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));
        _getWoundDressingQueryHandlerMock.Handle((int)patientId).ReturnsForAnyArgs(new List<WoundDressingSummaryForReturnDto>());
        _getPatientMealsSummaryQueryMock.Handle((int)patientId).ReturnsForAnyArgs(new List<MealsSummaryForReturnDto>());
        _getBedmakingQueryHandlerMock.Handle((int)patientId, 1).ReturnsForAnyArgs(new List<PatientBedMakingSummaryForReturnDto>());
        _getIntakeOutputQueryHandlerMock.Handle(patientId).ReturnsForAnyArgs(new List<IntakeOutputs.Dtos.PatientIntakeOutputDto>());
        _nurseCareQueryHandlerMock.Handle(new GetNurseCareRequest()
        {
            PatientId = patientId,
            EncounterId = encounterId,
        }).ReturnsForAnyArgs(new List<GetNurseCareSummaryResponse>());

        var handler = new NursingNoteQueryHandler(_basedQueryMock,
        _abpSessionMock,
        _medicationRequestQueryHandlerMock,
        _procedureRequestQueryHandlerMock,
        _physicalExaminationQueryHandlerMock,
        _vitalSignQueryHandlerMock,
        _getPhysicalExamSuggestionsQueryMock,
        _getPatientDischargeQueryMock,
        _getNurseHistoryQueryHandlerMock,
        _getWoundDressingQueryHandlerMock,
        _getPatientMealsSummaryQueryMock,
        _getBedmakingQueryHandlerMock,
        _getIntakeOutputQueryHandlerMock,
        _nurseCareQueryHandlerMock);

        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, patientEncounter));
        //Assert
        result.Message.ShouldBe("Patient does not exist.");
    }
}
