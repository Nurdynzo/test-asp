using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using Plateaumed.EHR.Investigations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Shouldly;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetAllInvestigationsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenFilter_ShouldReturnMatchingInvestigationsAndComponents()
        {
            // Arrange
            var repository = Substitute.For<IRepository<Investigation, long>>();
            repository.GetAll().Returns(GetData().BuildMock());

            var handler = new GetAllInvestigationsQueryHandler(repository);
            repository.GetAll().Returns(GetData().BuildMock());

            // Act
            var request = new GetAllInvestigationsRequest { Filter = "sod", Type = "Chemistry" };
            var response = await handler.Handle(request);

            // Assert
            response.Count.ShouldBe(4);
            response[0].Id.ShouldBe(1);
            response[0].Name.ShouldBe("Electrolytes, Urea & Creatinine");
            response[0].Specimen.ShouldBe("Whole blood");
            response[1].Id.ShouldBe(2);
            response[1].Name.ShouldBe("Sodium");
            response[1].Specimen.ShouldBe("CSF");
            response[2].Id.ShouldBe(5);
            response[2].Name.ShouldBe("Urine electrolytes");
            response[2].Specimen.ShouldBe("Urine");
            response[3].Id.ShouldBe(6);
            response[3].Name.ShouldBe("Sodium");
            response[3].Specimen.ShouldBe("Urine");
        }

        private IQueryable<Investigation> GetData()
        {
            return new List<Investigation>
            {
                new()
                {
                    Id = 1,
                    Type = "Chemistry",
                    Name = "Electrolytes, Urea & Creatinine",
                    Specimen = "Whole blood",
                    ShortName = "E/U/Cr",
                    SnomedId = "444164000",
                    Synonyms = "Measurement of urea, sodium, potassium, chloride, bicarbonate and creatinine",
                    Components = new List<Investigation>
                    {
                        new()
                        {
                            Id = 2,
                            Type = "Chemistry",
                            Name = "Sodium",
                            SnomedId = "39972003",
                            Specimen = "CSF",
                        },
                        new()
                        {
                            Id = 3,
                            Type = "Chemistry",
                            Name = "Potassium",
                            SnomedId = "59573005",
                            Specimen = "Whole blood",
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Type = "Chemistry",
                    Name = "Sodium",
                    SnomedId = "39972003",
                    Specimen = "CSF",
                },
                new()
                {
                    Id = 5,
                    Type = "Chemistry",
                    Name = "Urine electrolytes",
                    SnomedId = "14830009",
                    Specimen = "Urine",
                    Components = new List<Investigation>
                    {
                        new()
                        {
                            Id = 6,
                            Type = "Chemistry",
                            Name = "Sodium",
                            SnomedId = "104935006",
                            Specimen = "Urine",
                        }
                    }
                },
                new()
                {
                    Id = 6,
                    Type = "Chemistry",
                    Name = "Sodium",
                    SnomedId = "104935006",
                    Specimen = "Urine",
                }
            }.AsQueryable();
        }
    }
}