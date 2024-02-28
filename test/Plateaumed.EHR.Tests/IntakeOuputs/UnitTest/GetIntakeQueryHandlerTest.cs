using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Abp.Runtime.Session;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Query.BaseQueryHelper;

namespace Plateaumed.EHR.Tests.IntakeOuputs.UnitTest;

[Trait("Category", "Unit")]
public class GetIntakeQueryHandlerTest
{
    IBaseQuery _baseQueryMock = Substitute.For<IBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();


    [Fact]
    public async Task GetIntakeQueryHandlerTest_Handle_With_Invalid_PatientId_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetIntakesSuggestions(1).ReturnsForAnyArgs(Util.Common.GetPatientIntakes(1,1));

        var handler = new GetIntakeQueryHandler(_baseQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Patient Id is required.");

    }


    [Fact]
    public async Task GetIntakeQueryHandlerTest_Handle_Successful_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetIntakesSuggestions(1).ReturnsForAnyArgs(Util.Common.GetPatientIntakes(1, 1));

        var handler = new GetIntakeQueryHandler(_baseQueryMock);
        //Act
        var result = await handler.Handle(1);
        var resultId = result == null ? 0 : result.PatientId;
        //Assert
        resultId.ShouldBeGreaterThan(0);
    }

}