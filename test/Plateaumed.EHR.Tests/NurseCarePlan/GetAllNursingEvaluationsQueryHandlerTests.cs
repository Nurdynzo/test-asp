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
    public class GetAllNursingEvaluationsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldReturnValidNurseCareResponse()
        {
            // Arrange
            var repository = Substitute.For<IRepository<NursingEvaluation, long>>();
            repository.GetAll().Returns(new List<NursingEvaluation>
            {
                new()
                {
                    Id = 1,
                    Code = "Code 1",
                },
                new()
                {
                    Id = 2,
                    Code = "Code 2",
                }
            }.AsQueryable().BuildMock());
            var handler = new GetAllNursingEvaluationsQueryHandler(repository);

            // Act
            var result = await handler.Handle();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}