using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.NurseCarePlans.Handlers;
using Plateaumed.EHR.WardEmergencies;
using Plateaumed.EHR.WardEmergencies.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.NurseCarePlan
{
    [Trait("Category", "Unit")]
    public class GetAllNursingCareInterventionsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldReturnValidNurseCareResponse()
        {
            // Arrange
            var repository = Substitute.For<IRepository<NursingOutcome, long>>();
            repository.GetAll().Returns(new List<NursingOutcome>
            {
                new()
                {
                    Id = 1,
                    Code = "Code 1",
                    Interventions = new List<NursingCareIntervention>
                    {
                        new()
                        {
                            Id = 1,
                            Code = "Code 1",
                            Name = "Intervention 1"
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Code = "Code 2",
                    Interventions = new List<NursingCareIntervention>
                    {
                        new()
                        {
                            Id = 2,
                            Code = "Code 2",
                            Name = "Intervention 2"
                        }
                    }
                }
            }.AsQueryable().BuildMock());
            var handler = new GetAllNursingCareInterventionsQueryHandler(repository);

            // Act
            var result = await handler.Handle(new List<long> { 1, 2 });

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}