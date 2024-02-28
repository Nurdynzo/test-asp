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
    public class GetAllNursingOutcomesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldReturnValidNurseCareResponse()
        {
            // Arrange
            var repository = Substitute.For<IRepository<NursingDiagnosis, long>>();
            repository.GetAll().Returns(new List<NursingDiagnosis>
            {
                new()
                {
                    Id = 1,
                    Code = "Code 1",
                    Outcomes = new List<NursingOutcome>
                    {
                        new()
                        {
                            Id = 1,
                            Code = "Code 1",
                            Name = "Outcome 1"
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Code = "Code 2",
                    Outcomes = new List<NursingOutcome>
                    {
                        new()
                        {
                            Id = 2,
                            Code = "Code 2",
                            Name = "Outcome 2"
                        }
                    }
                }
            }.AsQueryable().BuildMock());
            var handler = new GetAllNursingOutcomesQueryHandler(repository);

            // Act
            var result = await handler.Handle( 1);

            // Assert
            Assert.Single(result);
        }
    }
}