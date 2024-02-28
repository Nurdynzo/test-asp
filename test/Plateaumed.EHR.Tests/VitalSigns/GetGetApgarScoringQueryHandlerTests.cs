using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns;

public class GetGetApgarScoringQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task GetAll_ShouldReturnApgarScores()
    {
        // Arrange
        var apgarScoring = GetApgarScoring();
        var repository = Substitute.For<IRepository<ApgarScoring, long>>();
        repository.GetAll().Returns(apgarScoring);

        var handler = new GetApgarScoringQueryHandler(repository, _objectMapper);
        // Act
        var result = await handler.Handle();

        // Assert
        result.Count.ShouldBe(1);
        result.First().Ranges.Count.ShouldBe(2);
        result.First().Ranges.First().Score.ShouldBe(0);
        result.First().Ranges.First().Result.ShouldBe("Pale");
        result.First().Ranges.Last().Score.ShouldBe(1);
        result.First().Ranges.Last().Result.ShouldBe("Pink");
    }

    private IQueryable<ApgarScoring> GetApgarScoring()
    {
        return new List<ApgarScoring>
        {
            new()
            {
                Name = "Appearance",
                Ranges = new List<ApgarScoringRange>
                {
                    new()
                    {
                        Score = 0,
                        Result = "Pale",
                    },
                    new()
                    {
                        Score = 1,
                        Result = "Pink",
                    }
                }
            }
        }.AsQueryable().BuildMock();
    }
}