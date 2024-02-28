using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Facilities.Handler;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    [Trait( "Category", "Unit")]
    public class GetWardBedCountQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenBedTypesWithoutWardBeds_ShouldReturnGroupedByType()
        {
            // Arrange

            var bedTypeRepository = Substitute.For<IRepository<BedType, long>>();
            bedTypeRepository.GetAll().Returns(GetBedTypes());
            var wardBedRepository = Substitute.For<IRepository<WardBed, long>>();
            wardBedRepository.GetAll().Returns(GetWardBeds());
            
            var request = new GetWardBedCountRequest();
            var handler = new GetWardBedCountQueryHandler(bedTypeRepository, wardBedRepository);

            // Act
            var result = await handler.Handle(request);

            // Assert
            result.Count.ShouldBe(3);
            result[0].BedTypeId.ShouldBe(1);
            result[0].BedTypeName.ShouldBe("Bed");
            result[0].Count.ShouldBe(2);
            result[0].WardBeds.Count.ShouldBe(2);
            result[0].WardBeds[0].BedNumber.ShouldBe("Bed 1");
            result[0].WardBeds[1].BedNumber.ShouldBe("Bed 2");
            result[1].BedTypeId.ShouldBe(2);
            result[1].BedTypeName.ShouldBe("Cot");
            result[1].Count.ShouldBe(1);
            result[1].WardBeds.Count.ShouldBe(1);
            result[1].WardBeds[0].BedNumber.ShouldBe("Cot 1");
            result[2].BedTypeId.ShouldBe(3);
            result[2].BedTypeName.ShouldBe("Incubator");
            result[2].Count.ShouldBe(0);
            result[2].WardBeds.Count.ShouldBe(0);
        }

        private static IQueryable<WardBed> GetWardBeds()
        {
            return new List<WardBed>
            {
                new () { Id = 1, BedTypeId = 1, BedNumber = "Bed 1" },
                new () { Id = 2, BedTypeId = 1, BedNumber = "Bed 2" },
                new () { Id = 3, BedTypeId = 2, BedNumber = "Cot 1" }

            }.AsQueryable().BuildMock();
        }

        private static IQueryable<BedType> GetBedTypes()
        {
            return new List<BedType>
                {
                    new() { Id = 1, Name = "Bed" },
                    new() { Id = 2, Name = "Cot" },
                    new() { Id = 3, Name = "Incubator" }
                }
                .AsQueryable().BuildMock();
        }
    }
}
