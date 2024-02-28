using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.IntakeOutputs;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.IntakeOutputs.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.IntakeOuputs.UnitTest;

[Trait("Category", "Unit")]
public class CreateIntakeOutputCommandHandlerTest
{
    private readonly IRepository<AllInputs.IntakeOutputCharting, long> _intakeOutputRepositoryMock
        = Substitute.For<IRepository<AllInputs.IntakeOutputCharting, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IBaseQuery _baseQuery = Substitute.For<IBaseQuery>();
    private readonly IEncounterManager _encounterManager = Substitute.For<IEncounterManager>();

    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetCreateIntakeOutputRequest(1);
        MockDependencies(command);
        command.Id = 1;
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var _id = result != null ? result.Id : 0;
        //Assert
        _id.ShouldBe(1);
    }

    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_ShouldCheckEncounterExists()
    {
        //arrange
        var command = GetCreateIntakeOutputRequest(1);
        MockDependencies(command);
        command.Id = 1;
        command.EncounterId = 5;
        //Act
        var handler = CreateHandler();
        await handler.Handle(command);
        //Assert
        await _encounterManager.Received(1).CheckEncounterExists(command.EncounterId);
        await _intakeOutputRepositoryMock.Received(1)
            .InsertAndGetIdAsync(Arg.Is<AllInputs.IntakeOutputCharting>(x => x.EncounterId == command.EncounterId));
    }

    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_With_No_Type_Selection_Failed_Response()
    {
        //arrange
        var command = GetCreateIntakeOutputRequest(1);
        command.Type = IntakeOutputs.ChartingType.UNKNOWN;
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Invalid Type.");
    }


    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_With_No_PatientId_Failed_Response()
    {
        //arrange
        var command = GetCreateIntakeOutputRequest(1);
        command.PatientId = 0;
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("PatientId is required.");
    }


    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_With_No_SuggestionText_Failed_Response()
    {
        //arrange
        var command = GetCreateIntakeOutputRequest(1);
        command.SuggestedText = string.Empty;
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Please enter the type of fluid.");
    }

    [Fact]
    public async Task CreateIntakeOutputCommandHandler_Handle_With_No_Volumn_Failed_Response()
    {
        //arrange
        var command = GetCreateIntakeOutputRequest(1);
        command.VolumnInMls = 0;
        MockDependencies(command);
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Please enter the volume of the fluid.");
    }

    private CreateIntakeOutputCommandHandler CreateHandler()
    {
        return new CreateIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _abpSessionMock, _baseQuery, _encounterManager);
    }

    private void MockDependencies(CreateIntakeOutputDto request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _intakeOutputRepositoryMock.InsertAndGetIdAsync(Arg.Any<AllInputs.IntakeOutputCharting>())
            .ReturnsForAnyArgs(1);
        _baseQuery.GetIntakeOutputByText(request.PatientId, request.SuggestedText).Returns(Util.Common.GetIntakeOutputByText(1, ""));
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }

    private void MockDependencies_With_Existing_Text(CreateIntakeOutputDto request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _intakeOutputRepositoryMock.InsertAndGetIdAsync(Arg.Any<AllInputs.IntakeOutputCharting>())
            .ReturnsForAnyArgs(1);
        _baseQuery.GetIntakeOutputByText(request.PatientId, request.SuggestedText).Returns(Util.Common.GetIntakeOutputByText(1, "Anitmalaria"));
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }

    private static CreateIntakeOutputDto GetCreateIntakeOutputRequest(long patientId)
    {
        return Util.Common.GetCreateIntakeOutputRequest(patientId)
            .FirstOrDefault(s => s.Type == ChartingType.INTAKE && s.Id == 0);
    }

}
