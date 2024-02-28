using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Handlers;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Abp.ObjectMapping;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class CreateNursingRecordCommandHandlerTest
{
    private readonly IRepository<NursingRecord, long> _nursingRecordRepositoryMock
        = Substitute.For<IRepository<NursingRecord, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IEncounterManager _encounterManagerMock = Substitute.For<IEncounterManager>();
    private readonly IDoctorReviewAndSaveBaseQuery _patientEncounterQueryMock = Substitute.For<IDoctorReviewAndSaveBaseQuery>();
    [Fact]
    public async Task CreateNursingRecordCommandHandlerTest_Handle_With_Successful_Response()
    {
        //arrange
        var patientId = 1;
        var encounterId = 1;
        var command = GetRequest(encounterId);
        MockDependencies(command, patientId);
        command.Id = 1;
        //Act
        var handler = CreateHandler();
        var result = await handler.Handle(command);
        var _id = result != null ? result.Id : 0;
        //Assert
        _id.ShouldBe(1);
    }

    [Fact]
    public async Task CreateNursingRecordCommandHandlerTest_Handle_ShouldCheckEncounterExists()
    {
        //arrange
        var patientId = 1;
        var encounterId = 1;
        var command = GetRequest(encounterId);
        MockDependencies(command, patientId);
        command.Id = 1;
        command.EncounterId = 5;
        //Act
        var handler = CreateHandler();
        await handler.Handle(command);
        //Assert
        await _encounterManagerMock.Received(1).CheckEncounterExists(command.EncounterId);
    }


    [Fact]
    public async Task CreateNursingRecordCommandHandlerTest_Handle_With_No_EncounterId_Failed_Response()
    {
        //arrange
        var patientId = 1;
        var command = GetRequest(0);
        MockDependencies(command, patientId);
        command.Id = 1;
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Encounter Id is required.");
    }


    private CreateNursingRecordCommandHandler CreateHandler()
    {
        return new CreateNursingRecordCommandHandler(_nursingRecordRepositoryMock,
            _unitOfWork, _abpSessionMock, _encounterManagerMock, _patientEncounterQueryMock);
    }

    private void MockDependencies(NursingRecordDto request, long patientId)
    {
        var mappedModel = Util.Common.GetAllNursingRecord(request.EncounterId).Where(s => s.Id == 1).FirstOrDefault();
        var patientEncounter = Util.Common.GetPatientEncounter(request.EncounterId, 1, patientId);
        _nursingRecordRepositoryMock.GetAllList().Returns(Util.Common.GetAllNursingRecord(request.EncounterId).Where(s => s.Id == 2).ToList());
        _nursingRecordRepositoryMock.GetAsync(request.Id.GetValueOrDefault()).ReturnsForAnyArgs(mappedModel);
        _nursingRecordRepositoryMock.UpdateAsync(mappedModel);
        _nursingRecordRepositoryMock.InsertAndGetIdAsync(Arg.Any<NursingRecord>())
            .ReturnsForAnyArgs(1);
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _encounterManagerMock.CheckEncounterExists(request.EncounterId).Returns(Task.CompletedTask);
        _patientEncounterQueryMock.GetPatientEncounter(request.EncounterId, 1).ReturnsForAnyArgs(patientEncounter);
    }

    private static NursingRecordDto GetRequest(long encounterId)
    {
        return new NursingRecordDto()
        {
            Id = 0,
            EncounterId = encounterId,
            NursingNote = new NursingNoteDto(),
            IsAutoSaved = false
        };
    }

}