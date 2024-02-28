using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Query;
using Abp.Runtime.Session;
using Plateaumed.EHR.Symptom.Abstractions;
using System;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using System.Linq.Dynamic.Core;
using Plateaumed.EHR.Admissions.Dto;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class DoctorsSummaryQueryHandlerTest
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQueryMock = Substitute.For<IDoctorReviewAndSaveBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IGetPatientSymptomSummaryQueryHandler _symptpomQueryHandlerMock = 
        Substitute.For<IGetPatientSymptomSummaryQueryHandler>();
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandlerMock = 
        Substitute.For<IGetPatientPhysicalExamSummaryWithEncounterQueryHandler>();
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandlerMock =
        Substitute.For<IGetReviewAndSavePatientVitalSignQueryHandler>();
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQueryMock = 
        Substitute.For<IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler>();
    private readonly IGetReviewAndSavePatientTypeNotesQueryHandler _getPatientTypeNoteQueryMock = 
        Substitute.For<IGetReviewAndSavePatientTypeNotesQueryHandler>();
    private readonly IGetPatientDiagnosisWithEncounterQueryHandler _getPatientDiagnosisQueryMock = 
        Substitute.For<IGetPatientDiagnosisWithEncounterQueryHandler>();
    private readonly IGetPatientDischargeWithEncounterIdQueryHandler _getPatientDischargeQueryMock =
        Substitute.For<IGetPatientDischargeWithEncounterIdQueryHandler>();
    private readonly IGetPlansResultQueryHandler _getPlansQueryHandlerMock =
        Substitute.For<IGetPlansResultQueryHandler>();

    [Fact]
    public async Task DoctorsSummaryQueryHandler_Handle_With_Successful_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        long encounterId = 1;
        var staffEncounter = Util.Common.GetPatientEncounters(encounterId, staffUserId, patientId).FirstOrDefault();
        var patientInfo = Util.Common.GetPatientDetails(patientId);
        var symptoms = Util.Common.GetPatientSymptoms();
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        var typenotes = Util.Common.GetPatientTypeNoteDto(staffUserId, symptoms, physicalExamination);

        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _symptpomQueryHandlerMock.Handle(Convert.ToInt32(patientId), 1).ReturnsForAnyArgs(symptoms);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId,1).ReturnsForAnyArgs(physicalExamination);
        _getPatientTypeNoteQueryMock.Handle(staffUserId, symptoms, physicalExamination)
            .ReturnsForAnyArgs(typenotes);

        _basedQueryMock.GetNoteTitle(patientId, 1, 1).ReturnsForAnyArgs(Util.Common.GetNoteTitle());
        _basedQueryMock.GenerateNoteIntroduction(patientInfo, symptoms, 1, facilityId,1, staffUserId)
            .ReturnsForAnyArgs(Util.Common.GenerateNoteIntroduction());

        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));

        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        _getPatientDiagnosisQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDiagnosis(patientId, encounterId));

        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));

        _getPlansQueryHandlerMock.Handle(patientId, encounterId, new AdmitPatientRequest())
            .ReturnsForAnyArgs(Util.Common.GetPlansResult(patientId));

        var handler = new DoctorsSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _symptpomQueryHandlerMock, _physicalExaminationQueryHandlerMock, 
            _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock,
            _getPatientTypeNoteQueryMock, _getPatientDiagnosisQueryMock, _getPatientDischargeQueryMock, _getPlansQueryHandlerMock);

        //Act
        var result = await handler.Handle(staffUserId, true, staffEncounter);
        long resultId = result == null ? 0 : result.DoctorUserId;
        //Assert
        resultId.Equals(staffUserId);
    }


    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_staff_With_No_Unit_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        long encounterId = 1;
        var staffEncounter = Util.Common.GetPatientEncounters(encounterId, staffUserId, patientId).FirstOrDefault();
        var patientInfo = Util.Common.GetPatientDetails(patientId);
        var symptoms = Util.Common.GetPatientSymptoms();
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        var typenotes = Util.Common.GetPatientTypeNoteDto(staffUserId, symptoms, physicalExamination);

        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetDoctorByUserId_No_JobUnit_Id(staffUserId));
        _symptpomQueryHandlerMock.Handle(Convert.ToInt32(patientId), 1).ReturnsForAnyArgs(symptoms);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPatientTypeNoteQueryMock.Handle(staffUserId, symptoms, physicalExamination)
            .ReturnsForAnyArgs(typenotes);

        _basedQueryMock.GetNoteTitle(patientId, 1, 1).ReturnsForAnyArgs(Util.Common.GetNoteTitle());
        _basedQueryMock.GenerateNoteIntroduction(patientInfo, symptoms, 1, facilityId, 1, staffUserId)
            .ReturnsForAnyArgs(Util.Common.GenerateNoteIntroduction());

        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));

        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        _getPatientDiagnosisQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDiagnosis(patientId, encounterId));

        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));

        _getPlansQueryHandlerMock.Handle(patientId, encounterId, new AdmitPatientRequest())
            .ReturnsForAnyArgs(Util.Common.GetPlansResult(patientId));

        var handler = new DoctorsSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _symptpomQueryHandlerMock, _physicalExaminationQueryHandlerMock,
            _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock,
            _getPatientTypeNoteQueryMock, _getPatientDiagnosisQueryMock, _getPatientDischargeQueryMock, _getPlansQueryHandlerMock);

        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, true, staffEncounter));
        //Assert
        result.Message.ShouldBe("No unit found for the selected doctor.");
    }


    [Fact]
    public async Task GenerateOnBehalfOfQueryHandler_Handle_staff_With_No_JobLevel_Result()
    {

        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long facilityId = 1;
        long encounterId = 1;
        var staffEncounter = Util.Common.GetPatientEncounters(encounterId, staffUserId, patientId).FirstOrDefault();
        var patientInfo = Util.Common.GetPatientDetails(patientId);
        var symptoms = Util.Common.GetPatientSymptoms();
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        var typenotes = Util.Common.GetPatientTypeNoteDto(staffUserId, symptoms, physicalExamination);

        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetDoctorByUserId_No_JobLevel_Id(staffUserId));
        _symptpomQueryHandlerMock.Handle(Convert.ToInt32(patientId), 1).ReturnsForAnyArgs(symptoms);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPatientTypeNoteQueryMock.Handle(staffUserId, symptoms, physicalExamination)
            .ReturnsForAnyArgs(typenotes);

        _basedQueryMock.GetNoteTitle(patientId, 1, 1).ReturnsForAnyArgs(Util.Common.GetNoteTitle());
        _basedQueryMock.GenerateNoteIntroduction(patientInfo, symptoms, 1, facilityId, 1, staffUserId)
            .ReturnsForAnyArgs(Util.Common.GenerateNoteIntroduction());

        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));

        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        _getPatientDiagnosisQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDiagnosis(patientId, encounterId));

        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));

        _getPlansQueryHandlerMock.Handle(patientId, encounterId, new AdmitPatientRequest())
            .ReturnsForAnyArgs(Util.Common.GetPlansResult(patientId));

        var handler = new DoctorsSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _symptpomQueryHandlerMock, _physicalExaminationQueryHandlerMock,
            _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock,
            _getPatientTypeNoteQueryMock, _getPatientDiagnosisQueryMock, _getPatientDischargeQueryMock, _getPlansQueryHandlerMock);

        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, true, staffEncounter));
        //Assert
        result.Message.ShouldBe("No job level found for the selected doctor.");
    }
}

