using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns
{
    public class GetAllVitalSignsQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task GetAll_ShouldReturnAllVitalSigns()
        {
            // Arrange
            var vitalSigns = GetVitalSigns();
            var repository = Substitute.For<IRepository<VitalSign, long>>();
            repository.GetAll().Returns(vitalSigns);

            var handler = new GetAllVitalSignsQueryHandler(repository, _objectMapper);
            // Act
            var result = await handler.Handle();

            // Assert
            result.Count.ShouldBe(2);
            result.First().Sign.ShouldBe("Blood Pressure");
            result.First().Sites.Count.ShouldBe(2);
            result.First().Ranges.Count.ShouldBe(2);
            result.First().Sites.First().Site.ShouldBe("Left Arm");
            result.First().Sites.First().Default.ShouldBe(true);
            result.First().Ranges.First().Lower.ShouldBe(60);
            result.First().Ranges.First().Upper.ShouldBe(90);
            result.First().Ranges.First().Unit.ShouldBe("mmHg");
        }

        private static IQueryable<VitalSign> GetVitalSigns()
        {
            return new List<VitalSign>
            {
                new()
                {
                    Id = 1,
                    Sign = "Blood Pressure",
                    LeftRight = true,
                    Ranges = new List<MeasurementRange>
                    {
                        new()
                        {
                            Id = 1,
                            Lower = 60,
                            Upper = 90,
                            Unit = "mmHg"
                        },
                        new()
                        {
                            Id = 2,
                            Lower = 90,
                            Upper = 120,
                            Unit = "mmHg"
                        }
                    },
                    Sites = new List<MeasurementSite>
                    {
                        new()
                        {
                            Id = 1,
                            Site = "Left Arm",
                            Default = true
                        },
                        new()
                        {
                            Id = 2,
                            Site = "Right Arm",
                            Default = false
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Sign = "Height",
                    LeftRight = false,
                    Ranges = new List<MeasurementRange>
                    {
                        new()
                        {
                            Id = 3,
                            Lower = 60,
                            Upper = 100,
                            Unit = "cm"
                        }
                    },
                    Sites = new List<MeasurementSite>
                    {
                        new()
                        {
                            Id = 3,
                            Site = "Standing",
                            Default = true
                        },
                        new()
                        {
                            Id = 4,
                            Site = "Lying",
                            Default = false
                        }
                    }
                },
            }.AsQueryable().BuildMock(); 
        }
    }
}
