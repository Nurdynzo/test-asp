using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Discharges.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Discharges.UnitTest;

[Trait("Category", "Unit")]
public class CreateDischargedPlanItemsHandlerTest
{
    private readonly IRepository<AllInputs.DischargePlanItem, long> _dischargePlanItemRepositoryMock
        = Substitute.For<IRepository<AllInputs.DischargePlanItem, long>>();
    private readonly EHR.PlanItems.Abstractions.IBaseQuery _planItemsQueryHandlerMock = Substitute.For<EHR.PlanItems.Abstractions.IBaseQuery>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    [Fact]
    public async Task CreateDischargedPlanItemsHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetDischargedPlanItemsRequest(1);
        MockDependencies(command);
        //Act
        var handler = new CreateDischargedPlanItemsHandler(_unitOfWork, _dischargePlanItemRepositoryMock, _planItemsQueryHandlerMock, _abpSessionMock);
        var result = await handler.Handle(command, 1, 1);
        var isSuccess = result == null ? false : result.Count > 0 ? true : false;
        //Assert
        isSuccess.ShouldBe(true);
    }


    [Fact]
    public async Task CreateDischargedPlanItemsHandler_Handle_With_No_PatientId_Failed_Response()
    {
        //arrange
        var command = GetDischargedPlanItemsRequest(1);
        MockDependencies(command);
        //Act
        var handler = new CreateDischargedPlanItemsHandler(_unitOfWork, _dischargePlanItemRepositoryMock, _planItemsQueryHandlerMock, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, 1, 0));
        //Assert
        result.Message.ShouldBe("PatientId Id is required.");
    }

    [Fact]
    public async Task CreateDischargedPlanItemsHandler_Handle_With_No_DischargeId_Failed_Response()
    {
        //arrange
        var command = GetDischargedPlanItemsRequest(1);
        MockDependencies(command);
        //Act
        var handler = new CreateDischargedPlanItemsHandler(_unitOfWork, _dischargePlanItemRepositoryMock, _planItemsQueryHandlerMock, _abpSessionMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, 0, 1));
        //Assert
        result.Message.ShouldBe("Discharge Id is required.");
    }


    [Fact]
    public async Task CreateDischargedPlanItemsHandler_Handle_With_No_PlanItems_Failed_Response()
    {
        //arrange
        var command = GetDischargedPlanItemsRequest(1);
        MockDependencies(command);
        command = null;
        //Act
        var handler = new CreateDischargedPlanItemsHandler(_unitOfWork, _dischargePlanItemRepositoryMock, _planItemsQueryHandlerMock, _abpSessionMock);
        var result = await handler.Handle(command, 1, 1);
        //Assert
        result.ShouldBeNull();
    }


    private void MockDependencies(List<CreateDischargePlanItemDto> request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _planItemsQueryHandlerMock.GetPatientPlanItemsBaseQuery(1, 1).ReturnsForAnyArgs(Util.Common.GetPatientPlanItems(1,1,false));
        _dischargePlanItemRepositoryMock.InsertAsync(Arg.Any<AllInputs.DischargePlanItem>())
            .ReturnsForAnyArgs(Util.Common.GetDischargePlanItemInstance(request, 1,1));
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }
    private static List<CreateDischargePlanItemDto> GetDischargedPlanItemsRequest(long patientId)
    {
        return Util.Common.GetDischargedPlanItemsRequest(patientId).ToList();
    }

}