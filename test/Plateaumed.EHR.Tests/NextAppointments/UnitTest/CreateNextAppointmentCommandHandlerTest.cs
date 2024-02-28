using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.NextAppointments.Abstractions;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.NextAppointments.Handlers;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Tests.NextAppointments.Util;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.NextAppointments.UnitTest;

[Trait("Category", "Unit")]
public class CreateNextAppointmentCommandHandlerTest
{
    private readonly ICreateAppointmentCommandHandler _createAppointmentCommandHandler = Substitute.For<ICreateAppointmentCommandHandler>();
    private readonly IRepository<PatientEncounter, long> _encounterRepositoryMock = Substitute.For<IRepository<PatientEncounter, long>>();
    private readonly INextAppointmentBaseQuery _baseQueryMock = Substitute.For<INextAppointmentBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    [Fact]
    public async Task CreateNextAppointmentCommandHandler_Handle_With_Successful_Response()
    {

        //arrange
        var loginUserId = 5;
        var command = GetCreateNextAppointmentRequest(1);
        MockDependencies(command, loginUserId);
        command.Id = 1;
        //Act
        var handler = new CreateNextAppointmentCommandHandler(_encounterRepositoryMock, _createAppointmentCommandHandler, _baseQueryMock, _abpSessionMock);
        var result = await handler.Handle(command, loginUserId);
        var id = result != null ? result.Id : 0;
        //Assert
        id.ShouldBe(1);
    }
    
    [Fact]
    public async Task CreateNextAppointmentCommandHandler_Handle_With_No_PatientId_Response()
    {
        //arrange
        var loginUserId = 5;
        var command = GetCreateNextAppointmentRequest(1);
        MockDependencies(command, loginUserId);
        command.PatientId = 0;
        //Act
        var handler = new CreateNextAppointmentCommandHandler(_encounterRepositoryMock, _createAppointmentCommandHandler, _baseQueryMock, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, loginUserId));
        //Assert
        result.Message.ShouldBe("PatientId is required.");
    }


    [Fact]
    public async Task CreateNextAppointmentCommandHandler_Handle_With_No_UnitId_Response()
    {
        //arrange
        var loginUserId = 5;
        var command = GetCreateNextAppointmentRequest(1);
        MockDependencies(command, loginUserId);
        command.UnitId = 0;
        //Act
        var handler = new CreateNextAppointmentCommandHandler(_encounterRepositoryMock, _createAppointmentCommandHandler, _baseQueryMock, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, loginUserId));
        //Assert
        result.Message.ShouldBe("Please supply a valid unit");
    }

    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_Where_SeenIn_Value_Is_Zero_Response()
    {
        //arrange
        var loginUserId = 5;
        var command = GetCreateNextAppointmentRequest(1);
        MockDependencies(command, loginUserId);
        command.SeenIn = 0;
        //Act
        var handler = new CreateNextAppointmentCommandHandler(_encounterRepositoryMock, _createAppointmentCommandHandler, _baseQueryMock, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, loginUserId));
        //Assert
        result.Message.ShouldBe("Please enter a number (day) indicating when the appointment will hold.");
    }

    private void MockDependencies(CreateNextAppointmentDto request, long loginUserId)
    {
        var requestDto = ConvertToPatientAppointmentDto(request);
        _encounterRepositoryMock.GetAll().Returns(common.GetPatientEncounter(1, requestDto.PatientId));
        requestDto.Id = 1;
        _createAppointmentCommandHandler.Handle(requestDto, 1).ReturnsForAnyArgs(requestDto);
        _baseQueryMock.GetOperationUnitTime(request.UnitId).Returns(Util.common.GetOperationUnitTime());
        _baseQueryMock.GetStaffFacilities(loginUserId, 2).Returns(Util.common.GetStaffFacilities(loginUserId, 2)); 
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }
    private static CreateNextAppointmentDto GetCreateNextAppointmentRequest(long patientId)
    {
        return common.GetCreateNextAppointment(patientId).
            Where(s => s.Id == 0).FirstOrDefault();
    }
    private static CreateOrEditPatientAppointmentDto ConvertToPatientAppointmentDto(CreateNextAppointmentDto requestDto)
    {
        return new CreateOrEditPatientAppointmentDto()
        {
            Id = requestDto.Id.GetValueOrDefault(),
            Title = $"Patient Next Appointment",
            Duration = 10,
            StartTime = requestDto.AppointmentDate.GetValueOrDefault(),
            IsRepeat = false,
            Notes = "",
            RepeatType = AppointmentRepeatType.Custom,
            Status = AppointmentStatusType.Rescheduled,
            Type = AppointmentType.Consultation,
            PatientId = requestDto.PatientId,
            AttendingPhysicianId = 4,
            AttendingClinicId = requestDto.UnitId,
            ReferringClinicId = 17,
        };
    }

}
