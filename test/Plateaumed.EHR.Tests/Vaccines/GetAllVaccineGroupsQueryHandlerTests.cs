using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.Vaccines.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Vaccines;

[Trait("Category", "Unit")]
public class GetAllVaccineGroupsQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task Handle_ShouldReturnAllVaccineNames()
    {
        // Arrange
        var repository = Substitute.For<IRepository<VaccineGroup, long>>();
        var testData = CreateTestData();
        repository.GetAll().Returns(testData);
            
        var handler = new GetAllVaccineGroupsQueryHandler(repository, _objectMapper);

        // Act
        var response = await handler.Handle();

        // Assert
        response.First().Id.ShouldBe(testData.First().Id);
        response.First().Name.ShouldBe(testData.First().Name);
        response.First().FullName.ShouldBe(testData.First().FullName);
        response.Last().Id.ShouldBe(testData.Last().Id);
        response.Last().Name.ShouldBe(testData.Last().Name);
        response.Last().FullName.ShouldBe(testData.Last().FullName);
    }

    private static IQueryable<VaccineGroup> CreateTestData()
    {
        return new List<VaccineGroup>
            {
                new() { Id = 1, Name = "VG1", FullName = "Vaccine Group 1" },
                new() { Id = 2, Name = "VG2", FullName = "Vaccine Group 2" },
            }
            .AsQueryable().BuildMock();
    }
}