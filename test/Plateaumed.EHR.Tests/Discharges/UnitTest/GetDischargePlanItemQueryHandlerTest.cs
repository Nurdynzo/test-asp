using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Query;
using Abp.UI;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;

namespace Plateaumed.EHR.Tests.Discharges.UnitTest;

[Trait("Category", "Unit")]
public class GetDischargePlanItemQueryHandlerTest
{
    IDischargeBaseQuery _baseQueryMock = Substitute.For<IDischargeBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    [Fact]
    public async Task Handle_Return_All_Discharge_PlanItem_Information()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetDischargePlanItem(1).Returns(Util.Common.GetDischargePlanItem(1,1).BuildMock());


        var handler = new GetDischargePlanItemsQueryHandler(_baseQueryMock);
        //act 
        var result = await handler.Handle(1);
        // assert
        result.Count.ShouldBeGreaterThan(0);

    }
    [Fact]
    public async Task Handle_When_DischargeId_Is_Zero_Return_No_Result()
    {
        //arrange 
        _baseQueryMock.GetDischargePlanItem(1).Returns(Util.Common.GetDischargePlanItem(1, 1));
        var handler = new GetDischargePlanItemsQueryHandler(_baseQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Discharge Id is required.");

    }




}