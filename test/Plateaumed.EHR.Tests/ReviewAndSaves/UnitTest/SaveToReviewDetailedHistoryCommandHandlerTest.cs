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

namespace Plateaumed.EHR.Tests.ReviewAndSaves.UnitTest;

[Trait("Category", "Unit")]
public class SaveToReviewDetailedHistoryCommandHandlerTest
{
    private readonly IRepository<PatientReviewDetailedHistory, long> _reviewDetailedHistoryRepositoryMock
        = Substitute.For<IRepository<PatientReviewDetailedHistory, long>>();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
    private readonly IEncounterManager _encounterManagerMock = Substitute.For<IEncounterManager>();
    private readonly IDoctorReviewAndSaveBaseQuery _patientEncounterQueryMock = Substitute.For<IDoctorReviewAndSaveBaseQuery>();
    private readonly IObjectMapper _objectMapperMock = Substitute.For<IObjectMapper>();
    [Fact]
    public async Task SaveToReviewDetailedHistoryCommandHandler_Handle_With_Successful_Response()
    {
        //arrange
        var patientId = 1;
        var encounterId = 1;
        var command = GetRequest(patientId, encounterId);
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
    public async Task SaveToReviewDetailedHistoryCommandHandler_Handle_ShouldCheckEncounterExists()
    {
        //arrange
        var patientId = 1;
        var encounterId = 1;
        var command = GetRequest(patientId, encounterId);
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
    public async Task SaveToReviewDetailedHistoryCommandHandler_Handle_With_No_PatientId_Failed_Response()
    {
        //arrange
        var encounterId = 1;
        var command = GetRequest(0, encounterId);
        MockDependencies(command, 0);
        command.Id = 1;
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("PatientId is required.");
    }


    [Fact]
    public async Task SaveToReviewDetailedHistoryCommandHandler_Handle_With_No_EncounterId_Failed_Response()
    {
        //arrange
        var patientId = 1;
        var command = GetRequest(patientId, 0);
        MockDependencies(command, patientId);
        command.Id = 1;
        //Act
        var handler = CreateHandler();
        var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(command));
        //Assert
        result.Message.ShouldBe("Encounter Id is required.");
    }


    private SaveToReviewDetailedHistoryCommandHandler CreateHandler()
    {
        return new SaveToReviewDetailedHistoryCommandHandler(_reviewDetailedHistoryRepositoryMock,
            _unitOfWork, _abpSessionMock, _encounterManagerMock, _patientEncounterQueryMock);
    }

    private void MockDependencies(SaveToReviewDetailedHistoryRequestDto request, long patientId)
    {
        var mappedModel = Util.Common.GetAllList(patientId, request.EncounterId).Where(s => s.Id == 1).FirstOrDefault();
        var patientEncounter = Util.Common.GetPatientEncounter(request.EncounterId,1,patientId);
        _reviewDetailedHistoryRepositoryMock.GetAllList().Returns(Util.Common.GetAllList(patientId, request.EncounterId).Where(s=>s.Id == 2).ToList());
        _reviewDetailedHistoryRepositoryMock.GetAsync(request.Id.GetValueOrDefault()).ReturnsForAnyArgs(mappedModel);
        _reviewDetailedHistoryRepositoryMock.UpdateAsync(mappedModel);
        _reviewDetailedHistoryRepositoryMock.InsertAndGetIdAsync(Arg.Any<PatientReviewDetailedHistory>())
            .ReturnsForAnyArgs(1);
        _unitOfWork.Current.SaveChangesAsync().Returns(Task.CompletedTask);
        _abpSessionMock.UserId.Returns(1);
        _abpSessionMock.TenantId.Returns(1);
        _encounterManagerMock.CheckEncounterExists(request.EncounterId).Returns(Task.CompletedTask);
        _patientEncounterQueryMock.GetPatientEncounter(request.EncounterId, 1).ReturnsForAnyArgs(patientEncounter);

        _objectMapperMock.Map<ReviewDetailedHistoryReturnDto>(Arg.Any<PatientReviewDetailedHistory>()).Returns(Util.Common.GetReviewDetailedHistoryReturnDto(request));
    }

    private static SaveToReviewDetailedHistoryRequestDto GetRequest(long patientId, long encounterId)
    {
        return new SaveToReviewDetailedHistoryRequestDto()
        {
            Id = 0,
            EncounterId = encounterId,
            Title = "Seen By Dr R",
            ShortDescription = string.Empty,
            FirstVisitNote = new EHR.ReviewAndSaves.Dtos.FirstVisitNoteDto(),
            IsAutoSaved = false
        };
    }

}
