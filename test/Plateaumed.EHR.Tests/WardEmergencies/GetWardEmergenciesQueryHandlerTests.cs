using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.WardEmergencies;
using Plateaumed.EHR.WardEmergencies.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.WardEmergencies
{
    [Trait("Category", "Unit")]
    public class GetWardEmergenciesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnWardEmergencies()
        {
            // Arrange
            var repository = Substitute.For<IRepository<WardEmergency, long>>();
            repository.GetAll().Returns(new List<WardEmergency>
            {
                new() {Id = 1, Event = "Test Event 1"},
                new() {Id = 2, Event = "Test Event 2"}
            }.AsQueryable().BuildMock());
            var handler = new GetAllWardEmergenciesQueryHandler(repository);
            // Act
            var result = await handler.Handle();

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result.First().Id.ShouldBe(1);
            result.First().Event.ShouldBe("Test Event 1");
            result.Last().Id.ShouldBe(2);
            result.Last().Event.ShouldBe("Test Event 2");
        }
    }
}
