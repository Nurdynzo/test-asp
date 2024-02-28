using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.IntakeOutputs;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.IntakeOutputs.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.IntakeOuputs.UnitTest;

[Trait("Category", "Unit")]
public class EditIntakeOutputCommandHandlerTest
{
    private readonly IRepository<AllInputs.IntakeOutputCharting, long> _intakeOutputRepositoryMock
        = Substitute.For<IRepository<AllInputs.IntakeOutputCharting, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IBaseQuery _baseQuery = Substitute.For<IBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    [Fact]
    public async Task EditIntakeOutputCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        MockDependencies(command);
        //Act
        var handler = new EditIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);//Act
        var result = await handler.Handle(command);
        var _id = result != null ? result.Id : 0;
        //Assert
        _id.ShouldBe(1);
    }

    [Fact]
    public async Task EditIntakeOutputCommandHandler_Handle_With_No_Type_Selection_Failed_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        command.Type = IntakeOutputs.ChartingType.UNKNOWN;
        MockDependencies(command);
        //Act
        var handler = new EditIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Invalid Type.");
    }

    [Fact]
    public async Task EditIntakeOutputCommandHandler_Handle_With_Wrong_PatientId_Failed_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        command.PatientId = 0;
        MockDependencies(command);
        //Act
        var handler = new EditIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("PatientId is required.");
    }

    [Fact]
    public async Task EditIntakeOutputCommandHandler_Handle_With_No_PatientId_Failed_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        command.PatientId = 0;
        MockDependencies(command);
        //Act
        var handler = new EditIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("PatientId is required.");
    }


    [Fact]
    public async Task EditIntakeOutputCommandHandler_Handle_With_No_SuggestionText_Failed_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        command.SuggestedText = string.Empty;
        MockDependencies(command);
        //Act
        var handler = new EditIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Please enter the type of fluid.");
    }

    [Fact]
    public async Task EditIntakeOutputCommandHandler_Handle_With_No_Volumn_Failed_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        command.VolumnInMls = 0;
        MockDependencies(command);
        //Act
        var handler = new EditIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Please enter the volume of the fluid.");
    }
    private void MockDependencies(CreateIntakeOutputDto request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        IntakeOutputCharting savedResult = null;
        _intakeOutputRepositoryMock.UpdateAsync(Arg.Do<IntakeOutputCharting>(result => savedResult = result));

        _baseQuery.GetIntakeOutputById(1).ReturnsForAnyArgs(Util.Common.GetIntakeOutputById(1));
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }

    private static CreateIntakeOutputDto GetEditIntakeOutputRequest(long patientId)
    {
        return Util.Common.GetCreateIntakeOutputRequest(patientId)
            .FirstOrDefault(s => s.Type == ChartingType.INTAKE && s.Id == 1);
    }

}