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
public class GetPatientDischargesQueryHandlerTest
{
    IDischargeBaseQuery _baseQueryMock = Substitute.For<IDischargeBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    [Fact]
    public async Task Handle_When_Filter_With_Wrong_PatientId_Return_No_Information()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _baseQueryMock.GetPatientDischarges(4).Returns(Util.Common.GetPatientDischargeInformation(4).ToList());
        _baseQueryMock.GetDischargeMedications(1).ReturnsForAnyArgs(Util.Common.GetDischargeMedications(1, 1));
        _baseQueryMock.GetDischargePlanItem(1).ReturnsForAnyArgs(Util.Common.GetDischargePlanItem(1, 1));
        var handler = new GetPatientDischargeQueryHandler(_baseQueryMock);
        //act 

        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));

        //Assert
        result.Message.ShouldBe("Patient Id is required.");

    }




}