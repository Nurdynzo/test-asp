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
    public class GetAllNursingDiagnosisQueryHandlerTests
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
                    Id = 42,
                    Code = "Code 1",
                }
            }.AsQueryable().BuildMock());
            var handler = new GetAllNursingDiagnosisQueryHandler(repository);
            
            // Act
            var result = await handler.Handle();
            
            // Assert
            Assert.Single(result);
        }
    }
}