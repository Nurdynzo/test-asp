using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Query;
using Abp.Runtime.Session;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Abp.Domain.Repositories;
using Plateaumed.EHR.NurseCarePlans;
using MockQueryable.NSubstitute;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class NurseSummaryQueryHandlerTest
{
    private readonly IDoctorReviewAndSaveBaseQuery _basedQueryMock = Substitute.For<IDoctorReviewAndSaveBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IRepository<NursingCareSummary, long> _nurseCarreRepositoryMock = Substitute.For<IRepository<NursingCareSummary, long>>();
    private readonly IGetPatientPhysicalExamSummaryWithEncounterQueryHandler _physicalExaminationQueryHandlerMock =
        Substitute.For<IGetPatientPhysicalExamSummaryWithEncounterQueryHandler>();
    private readonly IGetReviewAndSavePatientVitalSignQueryHandler _vitalSignQueryHandlerMock =
        Substitute.For<IGetReviewAndSavePatientVitalSignQueryHandler>();
    private readonly IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler _getPhysicalExamSuggestionsQueryMock =
        Substitute.For<IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler>();


    [Fact]
    public async Task NurseSummaryQueryHandler_Handle_With_Successful_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        _abpSessionMock.TenantId.Returns(1);
        var nurseSummary = Util.Common.GetNurseSummary(patientId, encounterId);
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);

        _nurseCarreRepositoryMock.GetAll().ReturnsForAnyArgs(nurseSummary.BuildMock());
        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        var handler = new NursesSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _nurseCarreRepositoryMock,
            _physicalExaminationQueryHandlerMock, _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock);

        //Act
        var result = await handler.Handle(encounterId);
        long resultNo = result == null ? 0 : result.Title.Length;
        //Assert
        resultNo.ShouldBeGreaterThan(5);
    }

    [Fact]
    public async Task NurseSummaryQueryHandler_Handle_With_No_EncounterId_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        var nurseSummary = Util.Common.GetNurseSummary(patientId, encounterId);
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);

        _abpSessionMock.TenantId.Returns(1);
        _nurseCarreRepositoryMock.GetAll().ReturnsForAnyArgs(nurseSummary.BuildMock());
        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        var handler = new NursesSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _nurseCarreRepositoryMock,
            _physicalExaminationQueryHandlerMock, _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Encounter Id is required.");
    }

    
    [Fact]
    public async Task NurseSummaryQueryHandler_Handle_With_No_Patient_Encounter_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        var nurseSummary = Util.Common.GetNurseSummary(patientId, encounterId, false, true);
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);

        _abpSessionMock.TenantId.Returns(1);
        _nurseCarreRepositoryMock.GetAll().ReturnsForAnyArgs(nurseSummary.BuildMock());
        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        var handler = new NursesSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _nurseCarreRepositoryMock,
            _physicalExaminationQueryHandlerMock, _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(encounterId));
        //Assert
        result.Message.ShouldBe("No patient encounter found on nurse care summary.");
    }

    [Fact]
    public async Task NurseSummaryQueryHandler_Handle_With_No_Patient_Result()
    {
        //arrange 
        long patientId = 1;
        long staffUserId = 1;
        long encounterId = 1;
        var nurseSummary = Util.Common.GetNurseSummary(patientId, encounterId, true, false);
        var physicalExamination = Util.Common.GetPatientPhysicalExamination(patientId);

        _abpSessionMock.TenantId.Returns(1);
        _nurseCarreRepositoryMock.GetAll().ReturnsForAnyArgs(nurseSummary.BuildMock());
        _basedQueryMock.GetStaffByUserId(staffUserId).ReturnsForAnyArgs(Util.Common.GetStaffByUserId(staffUserId));
        _physicalExaminationQueryHandlerMock.Handle(patientId, encounterId, 1).ReturnsForAnyArgs(physicalExamination);
        _getPhysicalExamSuggestionsQueryMock.Handle(physicalExamination)
            .ReturnsForAnyArgs(Util.Common.GeneratePatientPhysicalExamResult(physicalExamination));
        _vitalSignQueryHandlerMock.Handle(patientId, encounterId)
            .ReturnsForAnyArgs(Util.Common.GetPatientVitalSigns(patientId));

        var handler = new NursesSummaryQueryHandler(_basedQueryMock, _abpSessionMock, _nurseCarreRepositoryMock,
            _physicalExaminationQueryHandlerMock, _vitalSignQueryHandlerMock, _getPhysicalExamSuggestionsQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(encounterId));
        //Assert
        result.Message.ShouldBe("Patient does not exist.");
    }
}
