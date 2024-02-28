using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Feeding.Dtos;
using Plateaumed.EHR.Feeding.Handlers;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Feeding;

[Trait("Category", "Unit")]
public class CreateFeedingCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

    [Fact]
    public async Task Handle_GivenCorrectRequest()
    {
        // Arrange
        var feedingRepository = Substitute.For<IRepository<AllInputs.Feeding, long>>();
        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        var emptyResponse = new List<AllInputs.Feeding>().AsQueryable().BuildMock();
        var suggestions = new Patient()
        {
            DateOfBirth = DateTime.Now
        };

        feedingRepository.GetAll().Returns(emptyResponse);
        patientRepository.GetAsync(5).Returns(suggestions);
        var handler = new CreateFeedingCommandHandler(feedingRepository, patientRepository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());

        // Act
        await handler.Handle(new CreateFeedingDto()
        {
            PatientId = 5,
            EncounterId = 9
        });

        // Assert
        await feedingRepository.Received(1).InsertAsync(Arg.Is<AllInputs.Feeding>(x => x.EncounterId == 9));
    }

    [Fact]
    public async Task Handle_ShouldCheckEncounterExists()
    {
        // Arrange
        var feedingRepository = Substitute.For<IRepository<AllInputs.Feeding, long>>();
        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        patientRepository.GetAsync(5).Returns(new Patient { DateOfBirth = DateTime.Now });

        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = new CreateFeedingCommandHandler(feedingRepository, patientRepository, _unitOfWork, _objectMapper, encounterManager);

        // Act
        await handler.Handle(new CreateFeedingDto()
        {
            PatientId = 5,
            EncounterId = 9
        });

        // Assert
        await encounterManager.Received(1).CheckEncounterExists(9);
    }

    [Fact]
    public async Task Handle_GivenPatientIsOlderThan2Years()
    {
        // Arrange
        var feedingRepository = Substitute.For<IRepository<AllInputs.Feeding, long>>();
        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        var emptyResponse = new List<AllInputs.Feeding>().AsQueryable().BuildMock();
        var suggestions = new Patient()
        {
            DateOfBirth = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 5))
        };

        feedingRepository.GetAll().Returns(emptyResponse);
        patientRepository.GetAsync(5).Returns(suggestions);
        var handler = new CreateFeedingCommandHandler(feedingRepository, patientRepository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());

        // Act
        // Assert
        var exception = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(
            new CreateFeedingDto()
            {
                PatientId = 5
            })
        );
        exception.Message.ShouldBe("Patient must not be older then 2 years!");
    }
}