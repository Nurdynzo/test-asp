using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.WardEmergencies;
using Plateaumed.EHR.WardEmergencies.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.WardEmergencies
{
    [Trait("Category", "Unit")]
    public class GetNursingInterventionsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldReturnInterventions()
        {
            // Arrange
            const long wardEmergencyId = 42;
            var repository = Substitute.For<IRepository<WardEmergency, long>>();
            repository.GetAll().Returns(new List<WardEmergency>
            {
                new()
                {
                    Id = 42,
                    Event = "Event",
                    Interventions = new List<NursingIntervention>
                    {
                        new()
                        {
                            Id = 1,
                            Name = "Int1"
                        },
                        new()
                        {
                            Id = 2,
                            Name = "Int2"
                        }
                    }
                }
            }.AsQueryable().BuildMock());
            var handler = new GetNursingInterventionsQueryHandler(repository);
            // Act
            var result = await handler.Handle(wardEmergencyId);
            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}