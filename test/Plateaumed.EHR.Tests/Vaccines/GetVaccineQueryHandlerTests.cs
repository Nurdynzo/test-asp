using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Bogus;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.Vaccines.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Vaccines;

[Trait("Category", "Unit")]
public class GetVaccineQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task Handle_GivenUnknownId_ShouldThrow()
    {
        // Arrange
        var repository = Substitute.For<IRepository<Vaccine, long>>();
        var emptyResponse = new List<Vaccine>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var handler = new GetVaccineQueryHandler(repository, _objectMapper);
        // Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(new EntityDto<long>(1)));
        // Assert
       exception.Message.ShouldBe("Vaccine not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnAllVaccineNames()
    {
        // Arrange
        var repository = Substitute.For<IRepository<Vaccine, long>>();
        var testData = CreateTestData();
        repository.GetAll().Returns(testData);

        var handler = new GetVaccineQueryHandler(repository, _objectMapper);

        // Act
        var response = await handler.Handle(new EntityDto<long>(1));

        // Assert
        var vaccine = testData.First();
        response.Id.ShouldBe(vaccine.Id);
        response.Name.ShouldBe(vaccine.Name);
        response.FullName.ShouldBe(vaccine.FullName);
        response.Schedules.Count.ShouldBe(vaccine.Schedules.Count);
        response.Schedules.First().Id.ShouldBe(vaccine.Schedules.First().Id);
        response.Schedules.First().Doses.ShouldBe(vaccine.Schedules.First().Doses);
        response.Schedules.First().Dosage.ShouldBe(vaccine.Schedules.First().Dosage);
        response.Schedules.First().AgeMin.ShouldBe(vaccine.Schedules.First().AgeMin);
        response.Schedules.First().AgeMinUnit.ShouldBe(vaccine.Schedules.First().AgeMinUnit);
        response.Schedules.First().AgeMax.ShouldBe(vaccine.Schedules.First().AgeMax);
        response.Schedules.First().AgeMaxUnit.ShouldBe(vaccine.Schedules.First().AgeMaxUnit);
        response.Schedules.First().Notes.ShouldBe(vaccine.Schedules.First().Notes);
        response.Schedules.First().RouteOfAdministration.ShouldBe(vaccine.Schedules.First().RouteOfAdministration);
    }

    private static IQueryable<Vaccine> CreateTestData()
    {
        return new List<Vaccine>
            {
                new()
                {
                    Id = 1, 
                    Name = "V1", 
                    FullName = "Vaccine 1", 
                    Schedules = new List<VaccineSchedule>
                    {
                        CreateSchedule(),
                        CreateSchedule()
                    }
                }
            }
            .AsQueryable().BuildMock();
    }

    private static VaccineSchedule CreateSchedule()
    {
        var faker = new Faker<VaccineSchedule>();
        faker.RuleFor(x => x.Id, f => f.Random.Int());
        faker.RuleFor(x => x.Doses, f => f.Random.Int());
        faker.RuleFor(x => x.Dosage, f => f.Lorem.Sentence());
        faker.RuleFor(x => x.AgeMin, f => f.Random.Int());
        faker.RuleFor(x => x.AgeMinUnit, f => f.PickRandom<UnitOfTime>());
        faker.RuleFor(x => x.AgeMax, f => f.Random.Int());
        faker.RuleFor(x => x.AgeMaxUnit, f => f.PickRandom<UnitOfTime>());
        faker.RuleFor(x => x.Notes, f => f.Lorem.Sentence());
        faker.RuleFor(x => x.RouteOfAdministration, f => f.Lorem.Sentence());
        return faker.Generate();
    }
}