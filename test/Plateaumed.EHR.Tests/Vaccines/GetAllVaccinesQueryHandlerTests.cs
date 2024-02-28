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

namespace Plateaumed.EHR.Tests.Vaccines
{
    [Trait("Category", "Unit")]
    public class GetAllVaccinesQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_ShouldReturnAllVaccineNames()
        {
            // Arrange
            var repository = Substitute.For<IRepository<Vaccine, long>>();
            var testData = CreateTestData();
            repository.GetAll().Returns(testData);
            
            var handler = new GetAllVaccinesQueryHandler(repository, _objectMapper);

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

        private static IQueryable<Vaccine> CreateTestData()
        {
            return new List<Vaccine>
                {
                    new() { Id = 1, Name = "V1", FullName = "Vaccine 1" },
                    new() { Id = 2, Name = "V2", FullName = "Vaccine 2" },
                }
                .AsQueryable().BuildMock();
        }
    }
}