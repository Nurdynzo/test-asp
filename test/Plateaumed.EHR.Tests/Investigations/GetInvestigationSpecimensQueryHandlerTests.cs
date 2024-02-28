using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetInvestigationSpecimensQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnUniqueFilteredInvestigationSpecimens()
        {
            // Arrange
            var request = new GetInvestigationSpecimensRequest
            {
                Type = "Haematology",
            };

            var repository = Substitute.For<IRepository<Investigation, long>>();
            repository.GetAll().Returns(new List<Investigation>
            {
                new()
                {
                    Type = "Haematology",
                    Specimen = "Blood",
                },
                new()
                {
                    Type = "Haematology",
                    Specimen = "Serum",
                },
                new()
                {
                    Type = "Chemistry",
                    Specimen = "Urine",
                },
                new()
                {
                    Type = "Haematology",
                    Specimen = "",
                },
                new()
                {
                    Type = "Microbiology",
                    Specimen = "Sputum",
                },
                new()
                {
                    Type = "Haematology",
                    Specimen = "Stool",
                },
                new()
                {
                    Type = "Haematology",
                    Specimen = "Blood",
                }
            }.AsQueryable().BuildMock());

            var handler = new GetInvestigationSpecimensQueryHandler(repository);
            // Act
            var result = await handler.Handle(request);

            // Assert
            result.Specimens.ShouldBeEquivalentTo(new List<string>
            {
                "Blood",
                "Serum",
                "Stool"
            });
        }
    }
}
