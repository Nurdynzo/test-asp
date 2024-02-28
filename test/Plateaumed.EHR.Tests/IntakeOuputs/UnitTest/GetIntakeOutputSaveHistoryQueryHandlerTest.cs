using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Abp.Runtime.Session;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Query.BaseQueryHelper;
using Plateaumed.EHR.IntakeOutputs.Query;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Tests.IntakeOuputs.UnitTest;

[Trait("Category", "Unit")]
public class GetIntakeOutputSaveHistoryQueryHandlerTest
{
    IGetIntakeOutputSavedHistoryQueryHandler _historyQueryMock = Substitute.For<IGetIntakeOutputSavedHistoryQueryHandler>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    IBaseQuery _baseQueryMock = Substitute.For<IBaseQuery>();


    [Fact]
    public async Task GetIntakeOutputSaveHistoryQueryHandler_Handle_With_Invalid_PatientId_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetPatientIntakeOutputHistory(1).ReturnsForAnyArgs(Util.Common.GetSavedHistory(1));
        _historyQueryMock.Handle(1).ReturnsForAnyArgs(Util.Common.GetSavedHistory(1));

        var handler = new GetIntakeOutputSavedHistoryQueryHandler(_baseQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Patient Id is required.");

    }


    [Fact]
    public async Task GetIntakeOutputSaveHistoryQueryHandler_Handle_Successful_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetPatientIntakeOutputHistory(1).ReturnsForAnyArgs(Util.Common.GetSavedHistory(1));
        _historyQueryMock.Handle(1).ReturnsForAnyArgs(Util.Common.GetSavedHistory(1));

        var handler = new GetIntakeOutputSavedHistoryQueryHandler(_baseQueryMock);
        //Act
        var result = await handler.Handle(1);
        var resultId = result == null ? 0 : result.Count;
        //Assert
        resultId.ShouldBeGreaterThan(0);
    }

}