using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.IntakeOutputs.Handlers;
using Plateaumed.EHR.Investigations;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.IntakeOuputs.UnitTest;

[Trait("Category", "Unit")]
public class DeleteIntakeOutputCommandHandlerTest
{
    private readonly IRepository<AllInputs.IntakeOutputCharting, long> _intakeOutputRepositoryMock
        = Substitute.For<IRepository<AllInputs.IntakeOutputCharting, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IBaseQuery _baseQuery = Substitute.For<IBaseQuery>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();

    [Fact]
    public async Task DeleteIntakeOutputCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        MockDependencies(command);
        //Act
        var handler = new DeleteIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);//Act
        var result = await handler.Handle(command.Id.Value);
        //Assert
        result.ShouldBe(true);
    }

    [Fact]
    public async Task DeleteIntakeOutputCommandHandler_Handle_With_No_Id_Failed_Response()
    {
        //arrange
        var command = GetEditIntakeOutputRequest(1);
        command.PatientId = 0;
        MockDependencies(command);
        //Act
        var handler = new DeleteIntakeOutputCommandHandler(_intakeOutputRepositoryMock, _unitOfWork, _baseQuery, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(0));
        //Assert
        result.Message.ShouldBe("Id is required.");
    }
    private void MockDependencies(CreateIntakeOutputDto request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _baseQuery.GetIntakeOutputById((long)request.Id).ReturnsForAnyArgs(Util.Common.GetIntakeOutputById((long)request.Id));

        IntakeOutputCharting savedResult = null;
        _intakeOutputRepositoryMock.UpdateAsync(Arg.Do<IntakeOutputCharting>(result => savedResult = result));

        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }

    private static CreateIntakeOutputDto GetEditIntakeOutputRequest(long patientId)
    {
        return Util.Common.GetCreateIntakeOutputRequest(patientId).
            Where(s => s.Type == IntakeOutputs.ChartingType.INTAKE && s.Id == 1).FirstOrDefault();
    }

}
