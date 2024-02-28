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
    public class GetAllNursingActivitiesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldReturnValidNurseCareResponse()
        {
            // Arrange
            var repository = Substitute.For<IRepository<NursingCareIntervention, long>>();
            repository.GetAll().Returns(new List<NursingCareIntervention>
            {
                new()
                {
                    Id = 1,
                    Code = "Code 1",
                    Activities = new List<NursingActivity>
                    {
                        new()
                        {
                            Id = 1,
                            Code = "Code 1",
                            Name = "Activity 1"
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Code = "Code 2",
                    Activities = new List<NursingActivity>
                    {
                        new()
                        {
                            Id = 2,
                            Code = "Code 2",
                            Name = "Activity 2"
                        }
                    }
                }
            }.AsQueryable().BuildMock());
            var handler = new GetAllNursingActivitiesQueryHandler(repository);

            // Act
            var result = await handler.Handle(new List<long> { 1, 2 });

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}