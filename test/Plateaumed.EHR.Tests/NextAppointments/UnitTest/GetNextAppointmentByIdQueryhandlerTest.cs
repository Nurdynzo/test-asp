using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Abp.Runtime.Session;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.Tests.NextAppointments.Util;
using Plateaumed.EHR.NextAppointments.Query;
using System.Linq;

namespace Plateaumed.EHR.Tests.NextAppointments.UnitTest;

[Trait("Category", "Unit")]
public class GetNextAppointmentByIdQueryHandlerTest
{
    INextAppointmentBaseQuery _baseQueryMock = Substitute.For<INextAppointmentBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();


    [Fact]
    public async Task GetNextAppointmentByIdQueryHandler_Handle_With_No_AppointmentId_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);

        var handler = new GetNextAppointmentByIdQueryHandler(_baseQueryMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Next Appointment Id is required.");

    }

    [Fact]
    public async Task GetNextAppointmentByIdQueryHandler_Handle_With_Success_Response()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        var request = common.GetUnitOrClinicRequest(1);
        var model = common.GetPatientAllNextAppointmentById(1).Where(s => s.Id == 1).FirstOrDefault();
        _baseQueryMock.GetNextAppointmentById(1).ReturnsForAnyArgs(model);

        var handler = new GetNextAppointmentByIdQueryHandler(_baseQueryMock);
        //Act
        var result = await handler.Handle(1);
        var resultId = result == null ? 0 : result.Id;
        //Assert
        resultId.ShouldBeGreaterThan(0);
    }

}