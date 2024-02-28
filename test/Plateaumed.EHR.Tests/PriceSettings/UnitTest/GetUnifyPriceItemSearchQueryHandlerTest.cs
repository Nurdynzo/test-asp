using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.PriceSettings.Query;
using Plateaumed.EHR.Snowstorm.Abstractions;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Symptom;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    public class GetUnifyPriceItemSearchQueryHandlerTest
    {
        private readonly IRepository<Ward, long> _wardRepositoryMock = Substitute.For<IRepository<Ward, long>>();
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepositoryMock = Substitute.For<IRepository<OrganizationUnitExtended, long>>();
        private readonly ISnowstormBaseQuery _snowstormBaseQueryMock = Substitute.For<ISnowstormBaseQuery>();
        private readonly IRepository<SnowmedSuggestion, long> _snomedSuggestionRepositoryMock = Substitute.For<IRepository<SnowmedSuggestion, long>>();

        [Fact]
        public async Task Handler_With_Given_Valid_Request_For_Consultation_Should_Returns_List_Of_PriceItemsSearchResponse()
        {
            //arrange
            var request = new PriceItemsSearchRequest
            {
                FacilityId = 1,
                PricingCategory = PricingCategory.Consultation,
                SearchTerm = "clinic"
            };
            BuildMockDependency();
            var handler = GetUnifyPriceItemSearchQueryHandler();
            //act

            var result = await handler.Handle(request);

            //assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result.First().Name.ShouldBe("test clinic");
        }
        [Fact]
        public async Task Handler_With_Given_Valid_Request_For_Ward_Should_Returns_List_Of_PriceItemsSearchResponse()
        {
            //arrange
            var request = new PriceItemsSearchRequest
            {
                FacilityId = 1,
                PricingCategory = PricingCategory.WardAdmission,
                SearchTerm = "ward"
            };
            BuildMockDependency();
            var handler = GetUnifyPriceItemSearchQueryHandler();
            //act

            var result = await handler.Handle(request);

            //assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result.First().Name.ShouldBe("test ward");
        }
        [Fact]
        public async Task Handler_With_Given_Valid_Request_For_Procedure_Should_Returns_List_Of_PriceItemsSearchResponse()
        {
            //arrange
            var request = new PriceItemsSearchRequest
            {
                FacilityId = 1,
                PricingCategory = PricingCategory.Procedure,
                SearchTerm = "test"
            };
            BuildMockDependency();
            var handler = GetUnifyPriceItemSearchQueryHandler();
            //act

            var result = await handler.Handle(request);

            //assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result.First().Name.ShouldBe("test1");
            result.First().ItemId.ShouldBe("12345");
        }
        [Fact]
        public async Task Handler_With_Given_Valid_Request_For_Others_Should_Returns_List_Of_PriceItemsSearchResponse()
        {
            //arrange
            var request = new PriceItemsSearchRequest
            {
                FacilityId = 1,
                PricingCategory = PricingCategory.Others,
                SearchTerm = "test"
            };
            BuildMockDependency();
            var handler = GetUnifyPriceItemSearchQueryHandler();
            //act

            var result = await handler.Handle(request);

            //assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result.First().Name.ShouldBe("test2");
            result.First().ItemId.ShouldBe("1234");
        }
        [Fact]
        public async Task Handler_With_Given_Valid_Request_For_Others_Should_Throw_Exception_When_SearchTerm_Is_Less_Than_3()
        {
            //arrange
            var request = new PriceItemsSearchRequest
            {
                FacilityId = 1,
                PricingCategory = PricingCategory.Others,
                SearchTerm = "te"
            };
            BuildMockDependency();
            var handler = GetUnifyPriceItemSearchQueryHandler();
            //act

            var result = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));

            //assert
            result.ShouldNotBeNull();
            result.Message.ShouldBe("Search term must be more than 3 characters");
        }
        private GetUnifyPriceItemSearchQueryHandler GetUnifyPriceItemSearchQueryHandler()
        {

            var handler = new GetUnifyPriceItemSearchQueryHandler(_wardRepositoryMock,
                _organizationUnitRepositoryMock,
                _snowstormBaseQueryMock,_snomedSuggestionRepositoryMock);
            return handler;
        }
        private void BuildMockDependency()
        {

            _snomedSuggestionRepositoryMock.GetAll().Returns(GetSnomedItems().BuildMock());
            _wardRepositoryMock.GetAll().Returns(GetWards().BuildMock());
            _organizationUnitRepositoryMock.GetAll().Returns(GetOrganizationUnits().BuildMock());
            _snowstormBaseQueryMock.GetTermBySemanticTags(Arg.Any<SnowstormRequestDto>()).Returns(GetSnowstormResponse());
        }
        private Task<(SnowstormResponse snowstormResponse, bool status)> GetSnowstormResponse()
        {
            var snowstormResponse = new SnowstormResponse
            {
                Items = new List<SnowItem>
                {
                    new()
                    {
                       Term = "test1",
                       Concept = new()
                       {
                           Id = "12345",
                       }
                    },
                    new()
                    {
                        Term = "test2",
                        Concept = new()
                        {
                            Id = "1234",
                        }
                    }

                }
            };
            return Task.FromResult((snowstormResponse, true));
        }
        private IQueryable<OrganizationUnitExtended> GetOrganizationUnits()
        {
            return new List<OrganizationUnitExtended>()
            {
                new ()
                {
                    Id = 1,
                    DisplayName = "test clinic",
                    Type = OrganizationUnitType.Clinic,
                    TenantId = 1,
                    FacilityId = 1
                }
            }.AsQueryable();


        }
        private IQueryable<Ward> GetWards()
        {
            return new List<Ward>()
            {
                new ()
                {
                    Id = 1,
                    Name = "test ward",
                    Description = "test",
                    FacilityId = 1,
                    TenantId = 1
                }
            }.AsQueryable();
        }
        private IQueryable<SnowmedSuggestion> GetSnomedItems()
        {
            return new List<SnowmedSuggestion>()
            {
                new ()
                {
                    Id = 1,
                    Name = "test1",
                    SnowmedId = "12345",
                    Type = AllInputType.Procedure
                },
                new ()
                {
                    Id = 2,
                    Name = "test2",
                    SnowmedId = "1234",
                    Type = AllInputType.PlanItems
                },
            }.AsQueryable();
        }
    }
}
