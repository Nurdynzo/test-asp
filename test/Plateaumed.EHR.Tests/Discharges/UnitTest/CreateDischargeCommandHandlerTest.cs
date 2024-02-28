using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Discharges.Handlers;
using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.PatientAppointments.Abstractions;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Discharges.UnitTest;

[Trait("Category", "Unit")]
public class CreateDischargeCommandHandlerTest
{
    private readonly IRepository<Discharge, long> _dischargeRepositoryMock = Substitute.For<IRepository<Discharge, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IBaseQuery _appointmentBaseQueryMock = Substitute.For<IBaseQuery>();
    private readonly IEncounterManager _encounterManagerMock = Substitute.For<IEncounterManager>();
    private readonly IRepository<PatientCauseOfDeath, long> _causeOfDeathRepositoryMock = Substitute.For<IRepository<PatientCauseOfDeath, long>>();
    private readonly IRepository<DischargeNote, long> _noteRepositoryMock = Substitute.For<IRepository<DischargeNote, long>>();

    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetCreateDischargeRequest(1);
        command.Id = 1;
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var dischargeId = result?.Id ?? 0;
        //Assert
        dischargeId.ShouldBe(1);
    }
    
    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_With_No_DischargeType_Failed_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_DischargeType(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Discharge Type is required.");
    }

    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_With_No_Prescription_Failed_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_Prescription(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("At least one prescription data is required.");
    }
    
    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_With_No_PlanItems_Failed_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_PlanItems(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("At least one plan item is required.");
    }

    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_With_No_AppointmentId_Successful_Response()
    {
        //arrange
        var command = Util.Common.GetCreateDischargeRequest_No_AppointmentId(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var dischargeId = result?.Id ?? 0;
        //Assert
        dischargeId.ShouldBeGreaterThan(0);
    }

    //DAMA

    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_For_DAMA_With_Successful_Response()
    {
        //arrange
        var command = GetCreateDischargeRequestForDAMA(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var dischargeId = result?.Id ?? 0;
        //Assert
        dischargeId.ShouldBeGreaterThan(0);
    }
    //DECEASED

    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_For_BroughytInDead_True_With_Successful_Response()
    {
        //arrange
        var command = GetDischargeRequestForBroughtInDead_True(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var dischargeId = result?.Id ?? 0;
        //Assert
        dischargeId.ShouldBeGreaterThan(0);
    }


    [Fact]
    public async Task CreateDischargeCommandHandler_Handle_For_BroughytInDead_False_With_Successful_Response()
    {
        //arrange
        var command = GetDischargeRequestForBroughtInDead_False(1);
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var dischargeId = result?.Id ?? 0;
        //Assert
        dischargeId.ShouldBeGreaterThan(0);
    }


    [Fact]
    public async Task Handle_GivenEncounterId_ShouldCheckEncounterExists()
    {
        //Arrange
        var command = GetCreateDischargeRequest(1);
        command.EncounterId = 7;
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        _ = await handler.Handle(command);
        //Assert
        await _encounterManagerMock.Received().CheckEncounterExists(7);
    }

    private CreateDischargeCommandHandler CreateHandler()
    {
        return new CreateDischargeCommandHandler(_dischargeRepositoryMock, _unitOfWork, _appointmentBaseQueryMock, _encounterManagerMock,
            _causeOfDeathRepositoryMock, _noteRepositoryMock);
    }

    private void MockDependencies(CreateDischargeDto request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _dischargeRepositoryMock.InsertAsync(Arg.Any<Discharge>())
            .ReturnsForAnyArgs(Util.Common.GetNormalDischargeInstance(request, 1));
        _appointmentBaseQueryMock.GetAppointmentsBaseQuery().Returns(Util.Common.GetAppointmentsBaseQuery());
        _noteRepositoryMock.InsertAsync(Arg.Any<DischargeNote>())
            .ReturnsForAnyArgs(Util.Common.GetDischargeNote(1, 1));
        _causeOfDeathRepositoryMock.GetAll().Returns(Util.Common.GetCausesOfDealth(request.Id.Value).BuildMock());
        _noteRepositoryMock.GetAll().Returns(Util.Common.GetDischargeNote(request.Id.Value).BuildMock());

        _causeOfDeathRepositoryMock.InsertAsync(Arg.Any<PatientCauseOfDeath>())
            .ReturnsForAnyArgs(Util.Common.GetPatientCauseOfDeath(1, 1));
    }
    private void MockDependencies_MarkAsDeceased(CreateMarkAsDeceasedDischargeDto request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _dischargeRepositoryMock.InsertAsync(Arg.Any<Discharge>())
            .ReturnsForAnyArgs(Util.Common.GetDeceasedDischargeInstance(request, 1));
        _appointmentBaseQueryMock.GetAppointmentsBaseQuery().Returns(Util.Common.GetAppointmentsBaseQuery());
        _noteRepositoryMock.InsertAsync(Arg.Any<DischargeNote>())
            .ReturnsForAnyArgs(Util.Common.GetDischargeNote(1, 1));
        _causeOfDeathRepositoryMock.InsertRangeAsync(Arg.Any<List<PatientCauseOfDeath>>())
            .ReturnsForAnyArgs(Task.CompletedTask);

        _causeOfDeathRepositoryMock.GetAll().Returns(Util.Common.GetCausesOfDealth(request.Id.Value));
        _noteRepositoryMock.GetAll().Returns(Util.Common.GetDischargeNote(request.Id.Value));
    }

    private static CreateDischargeDto GetCreateDischargeRequest(long encounterId)
    {
        return Util.Common.GetCreateDischargeRequest(encounterId).FirstOrDefault(s => s.DischargeType == DischargeEntryType.NORMAL);
    }

    private static CreateDischargeDto GetCreateDischargeRequestForDAMA(long encounterId)
    {
        return Util.Common.GetCreateDischargeRequest(encounterId).FirstOrDefault(s => s.DischargeType == DischargeEntryType.DAMA);
    }

    private static CreateDischargeDto GetDischargeRequestForBroughtInDead_True(long encounterId)
    {
        return Util.Common.GetMarkAsDeceasedDischargeRequest(encounterId).FirstOrDefault(s => s.IsBroughtInDead == true);
    }

    private static CreateDischargeDto GetDischargeRequestForBroughtInDead_False(long encounterId)
    {
        return Util.Common.GetMarkAsDeceasedDischargeRequest(encounterId).FirstOrDefault(s => s.IsBroughtInDead == false);
    }

}