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
using Plateaumed.EHR.Staff.Abstractions;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class EncounterSummaryQueryHandlerTest
{
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IGetPatientSymptomSummaryQueryHandler _symptpomQueryHandlerMock =
        Substitute.For<IGetPatientSymptomSummaryQueryHandler>();
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandlerMock =
        Substitute.For<IGetPatientPhysicalExamSummaryWithEncounterQueryHandler>();
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandlerMock =
        Substitute.For<IGetReviewAndSavePatientVitalSignQueryHandler>();
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQueryMock =
        Substitute.For<IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler>();
    private readonly IGetPatientDiagnosisWithEncounterQueryHandler _getPatientDiagnosisQueryMock =
        Substitute.For<IGetPatientDiagnosisWithEncounterQueryHandler>();
    private readonly IGetPatientDischargeWithEncounterIdQueryHandler _getPatientDischargeQueryMock =
        Substitute.For<IGetPatientDischargeWithEncounterIdQueryHandler>();
    private readonly IGetStaffMemberByUserIdQueryHandler _getStaffMemberMock =
        Substitute.For<IGetStaffMemberByUserIdQueryHandler>();
     

    [Fact]
    public async Task EncounterSummaryQueryHandler_Handle_With_Successful_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        var staffEncounter = Util.Common.GetPatientEncounters(encounterId, staffUserId, patientId).FirstOrDefault();
        var symptoms = Util.Common.GetPatientSymptoms();
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        _symptpomQueryHandlerMock.Handle(Convert.ToInt32(patientId), 1,1).ReturnsForAnyArgs(symptoms);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));
        _getPatientDiagnosisQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDiagnosis(patientId, encounterId));

        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));

        _getStaffMemberMock.Handle(staffUserId).ReturnsForAnyArgs(Util.Common.GetUserById(staffUserId));

        var handler = new EncounterSummaryQueryHandler(_abpSessionMock, _symptpomQueryHandlerMock, _physicalExaminationQueryHandlerMock,
            _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock, _getPatientDiagnosisQueryMock, _getPatientDischargeQueryMock,
            _getStaffMemberMock);

        //Act
        var result = await handler.Handle(staffUserId, staffEncounter);
        long resultId = result == null ? 0 : result.DoctorUserId;
        //Assert
        resultId.Equals(staffUserId);
    }


    [Fact]
    public async Task EncounterSummaryQueryHandler_Handle_With_No_Patient_Result()
    {

        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        var staffEncounter = Util.Common.GetPatientEncounters(encounterId, staffUserId, patientId, true,false).FirstOrDefault();
        var symptoms = Util.Common.GetPatientSymptoms();
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);
        _symptpomQueryHandlerMock.Handle(Convert.ToInt32(patientId), 1, 1).ReturnsForAnyArgs(symptoms);
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));
        _getPatientDiagnosisQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDiagnosis(patientId, encounterId));

        _getPatientDischargeQueryMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientDischarge(patientId, encounterId));

        _getStaffMemberMock.Handle(staffUserId).ReturnsForAnyArgs(Util.Common.GetUserById(staffUserId));

        var handler = new EncounterSummaryQueryHandler(_abpSessionMock, _symptpomQueryHandlerMock, _physicalExaminationQueryHandlerMock,
            _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock, _getPatientDiagnosisQueryMock, _getPatientDischargeQueryMock,
            _getStaffMemberMock);


        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(staffUserId, staffEncounter));
        //Assert
        result.Message.ShouldBe("Patient does not exist.");
    }
}
