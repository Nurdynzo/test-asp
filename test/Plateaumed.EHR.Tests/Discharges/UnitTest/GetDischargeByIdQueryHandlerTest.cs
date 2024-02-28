using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Query;
using Abp.UI;
using Abp.Runtime.Session;
using System.Linq;

namespace Plateaumed.EHR.Tests.Discharges.UnitTest;

[Trait("Category", "Unit")]
public class GetDischargeByIdQueryHandlerTest
{
    IDischargeBaseQuery _baseQueryMock = Substitute.For<IDischargeBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    
    [Fact]
    public async Task Handle_When_DischargeId_Is_Zero_Return_No_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetDischargeInformation(1).ReturnsForAnyArgs(Util.Common.GetPatientDischargeInformation(1).FirstOrDefault(s=>s.Id == 1));
        _baseQueryMock.GetDischargeMedications(1).ReturnsForAnyArgs(Util.Common.GetDischargeMedications(1, 1));
        _baseQueryMock.GetDischargePlanItem(1).ReturnsForAnyArgs(Util.Common.GetDischargePlanItem(1, 1));

        var handler = new GetDischargeByIdQueryHandler(_baseQueryMock, _abpSessionMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Discharge Id is required.");

    }




}