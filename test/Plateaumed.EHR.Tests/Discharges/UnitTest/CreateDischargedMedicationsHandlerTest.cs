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
using Plateaumed.EHR.Medication.Abstractions;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Discharges.UnitTest;

[Trait("Category", "Unit")]
public class CreateDischargedMedicationsHandlerTest
{
    private readonly IRepository<AllInputs.DischargeMedication, long> _dischargeMedicationRepositoryMock
        = Substitute.For<IRepository<AllInputs.DischargeMedication, long>>();
    private readonly IGetPatientMedicationQueryHandler _medicationQueryHandlerMock = Substitute.For<IGetPatientMedicationQueryHandler>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    [Fact]
    public async Task CreateDischargedMedicationsHandler_Handle_With_Successful_Response()
    {
        //arrange
        var command = GetDischargeMedicationRequest(1);
        //command = command.Where(s => s.PatientId == 1).ToList();
        MockDependencies(command);
        //Act
        var handler = new CreateDischargedMedicationsHandler(_unitOfWork, _medicationQueryHandlerMock, _abpSessionMock, _dischargeMedicationRepositoryMock);
        var result = await handler.Handle(command, 1, 1);
        var isSuccess = result == null ? false : result.Count > 0 ? true : false;
        //Assert
        isSuccess.ShouldBe(true);
    }

    [Fact]
    public async Task CreateDischargedMedicationsHandler_Handle_With_No_PatientId_Failed_Response()
    {
        //arrange
        var command = GetDischargeMedicationRequest(1);
        MockDependencies(command);
        //Act
        var handler = new CreateDischargedMedicationsHandler(_unitOfWork, _medicationQueryHandlerMock, _abpSessionMock, _dischargeMedicationRepositoryMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, 1,0));
        //Assert
        result.Message.ShouldBe("PatientId Id is required.");
    }

    [Fact]
    public async Task CreateDischargedMedicationsHandler_Handle_With_No_DischargeId_Failed_Response()
    {
        //arrange
        var command = GetDischargeMedicationRequest(1);
        MockDependencies(command);
        //Act
        var handler = new CreateDischargedMedicationsHandler(_unitOfWork, _medicationQueryHandlerMock, _abpSessionMock, _dischargeMedicationRepositoryMock);
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command, 0, 1));
        //Assert
        result.Message.ShouldBe("Discharge Id is required.");
    }


    [Fact]
    public async Task CreateDischargedMedicationsHandler_Handle_With_No_Medication_Failed_Response()
    {
        //arrange
        var command = GetDischargeMedicationRequest(1);
        MockDependencies(command);
        command = null;
        //Act
        var handler = new CreateDischargedMedicationsHandler(_unitOfWork, _medicationQueryHandlerMock, _abpSessionMock, _dischargeMedicationRepositoryMock);
        var result = await handler.Handle(command, 1, 1);
        //Assert
        result.ShouldBeNull();
    }

    private void MockDependencies(List<CreateDischargeMedicationDto> request)
    {
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _medicationQueryHandlerMock.Handle(1,1).ReturnsForAnyArgs(Util.Common.GetMedications(1));
        _dischargeMedicationRepositoryMock.InsertAsync(Arg.Any<AllInputs.DischargeMedication>())
            .ReturnsForAnyArgs(Util.Common.GetListDischargeMedicationInstance(request,1, 1,1).FirstOrDefault());
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
    }
    private static List<CreateDischargeMedicationDto> GetDischargeMedicationRequest(long patientId)
    {
        return Util.Common.GetDischargeMedicationRequest(patientId).ToList();
    }

}