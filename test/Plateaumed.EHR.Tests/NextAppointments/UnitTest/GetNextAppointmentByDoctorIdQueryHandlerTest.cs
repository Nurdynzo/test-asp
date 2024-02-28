using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using Xunit;
using Abp.UI;
using Abp.Runtime.Session;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.Tests.NextAppointments.Util;
using Plateaumed.EHR.NextAppointments.Query;

namespace Plateaumed.EHR.Tests.NextAppointments.UnitTest;

[Trait("Category", "Unit")]
public class GetNextAppointmentByDoctorIdQueryHandlerTest
{
    INextAppointmentBaseQuery _baseQueryMock = Substitute.For<INextAppointmentBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();


    [Fact]
    public async Task GetNextAppointmentByDoctorIdQueryHandler_Handle_With_No_DoctorId_Result()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);

        var handler = new GetDoctorAllNextAppointmentsQueryHandler(_baseQueryMock, _abpSessionMock);
        //Act
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Doctor user Id is required.");

    }

    [Fact]
    public async Task GetNextAppointmentByDoctorIdQueryHandler_Handle_With_Sucess_Response()
    {
        //arrange 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        var model = common.GetPatientAllNextAppointmentById(1);
        _baseQueryMock.GetNextAppointmentByDoctorId(4).ReturnsForAnyArgs(model);

        var handler = new GetDoctorAllNextAppointmentsQueryHandler(_baseQueryMock, _abpSessionMock);
        //Act
        var result = await handler.Handle(1);
        var resultId = result == null ? 0 : result.Count;
        //Assert
        resultId.ShouldBeGreaterThan(0);
    }

}
