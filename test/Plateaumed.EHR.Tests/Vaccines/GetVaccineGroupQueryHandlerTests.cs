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
public class GetVaccineGroupQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task Handle_GivenUnknownId_ShouldThrow()
    {
        // Arrange
        var repository = Substitute.For<IRepository<VaccineGroup, long>>();
        var emptyResponse = new List<VaccineGroup>().AsQueryable().BuildMock();

        repository.GetAll().Returns(emptyResponse);

        var handler = new GetVaccineGroupQueryHandler(repository, _objectMapper);
        // Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(new EntityDto<long>(1)));
        // Assert
        exception.Message.ShouldBe("Vaccine group not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnAllVaccinesInGroup()
    {
        // Arrange
        var repository = Substitute.For<IRepository<VaccineGroup, long>>();
        var testData = CreateTestData();
        repository.GetAll().Returns(testData);

        var handler = new GetVaccineGroupQueryHandler(repository, _objectMapper);

        // Act
        var response = await handler.Handle(new EntityDto<long>(1));

        // Assert
        var vaccineGroup = testData.First();
        var vaccine1 = vaccineGroup.Vaccines.First();
        var vaccine2 = vaccineGroup.Vaccines.Last();

        response.Id.ShouldBe(vaccineGroup.Id);
        response.Name.ShouldBe(vaccineGroup.Name);
        response.FullName.ShouldBe(vaccineGroup.FullName);

        response.Vaccines.First().Schedules.First().Doses.ShouldBe(vaccine1.Schedules.First().Doses);
        response.Vaccines.First().Schedules.First().Dosage.ShouldBe(vaccine1.Schedules.First().Dosage);
        response.Vaccines.First().Schedules.First().AgeMin.ShouldBe(vaccine1.Schedules.First().AgeMin);
        response.Vaccines.First().Schedules.First().AgeMinUnit.ShouldBe(vaccine1.Schedules.First().AgeMinUnit);
        response.Vaccines.Last().Schedules.First().AgeMax.ShouldBe(vaccine2.Schedules.First().AgeMax);
        response.Vaccines.Last().Schedules.First().AgeMaxUnit.ShouldBe(vaccine2.Schedules.First().AgeMaxUnit);
        response.Vaccines.Last().Schedules.First().Notes.ShouldBe(vaccine2.Schedules.First().Notes);
        response.Vaccines.Last().Schedules.First().RouteOfAdministration.ShouldBe(vaccine2.Schedules.First().RouteOfAdministration);
    }

    private static IQueryable<VaccineGroup> CreateTestData()
    {
        return new List<VaccineGroup>
            {
                new()
                {
                    Id = 1, 
                    Name = "VG1", 
                    FullName = "Vaccine Group 1",
                    Vaccines = new List<Vaccine>
                    {
                        new()
                        {
                            Id = 1, 
                            Name = "V1", 
                            FullName = "Vaccine 1", 
                            Schedules = new List<VaccineSchedule>
                            {
                                CreateSchedule()
                            }
                        },
                        new()
                        {
                            Id = 2, 
                            Name = "V2", 
                            FullName = "Vaccine 2", 
                            Schedules = new List<VaccineSchedule>
                            {
                                CreateSchedule()
                            }
                        }
                    }
                    
                }
            }
            .AsQueryable().BuildMock();
    }

    private static VaccineSchedule CreateSchedule()
    {
        var faker = new Faker<VaccineSchedule>();
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