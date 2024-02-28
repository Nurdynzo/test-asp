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
    public class GetRadiologyAndPulmonaryInvestigationTypesHandlerTests
    {
        [Fact]
        public async Task HandleGivenValidDataShouldReturn()
        {
            var request = new GetElectroRadPulmInvestigationResultDto
            {
                Category = ""
            };
            var investigation = Substitute.For<IRepository<Investigation, long>>();
            investigation.GetAll().Returns(GetInvestigations().BuildMock());           

            var handler = new GetRadiologyAndPulmonaryInvestigationTypesHandler(investigation);

            var result = await handler.Handle(request);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        [Fact]
        public async Task HandleGivenValidDataWithTypeBodyPartForPulmAndRadShouldReturnSingleItem()
        {
            var request = new GetElectroRadPulmInvestigationResultDto
            {
                Category = "BodyParts"
            };

            var investigation = Substitute.For<IRepository<Investigation, long>>();
            investigation.GetAll().Returns(GetInvestigations().BuildMock());

            var handler = new GetRadiologyAndPulmonaryInvestigationTypesHandler(investigation);

            var result = await handler.Handle(request);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }


        [Fact]
        public async Task HandleGivenValidDataWithTypeViewForPulmAndRadShouldReturnSingleItem()
        {
            var request = new GetElectroRadPulmInvestigationResultDto
            {
                Category = "View"
            };

            var investigation = Substitute.For<IRepository<Investigation, long>>();
            investigation.GetAll().Returns(GetInvestigations().BuildMock());

            var handler = new GetRadiologyAndPulmonaryInvestigationTypesHandler(investigation);

            var result = await handler.Handle(request);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        private static IQueryable<Investigation> GetInvestigations()
        {
            return new List<Investigation>
            {
                new Investigation
                {
                    Id = 1,
                    Name = "Test",
                    Type="Radiology + Pulm",
                    RadiologyAndPulmonary = new List<RadiologyAndPulmonaryInvestigation>
                    {
                        new RadiologyAndPulmonaryInvestigation{ Category = "BodyParts", Id = 1, InvestigationId = 1, Name = "test"}
                    }
                },
                new Investigation
                {
                    Id = 2,
                    Name = "Test 2",
                    Type="Radiology + Pulm",
                    RadiologyAndPulmonary = new List<RadiologyAndPulmonaryInvestigation>
                    {
                        new RadiologyAndPulmonaryInvestigation{ Category = "View", Id = 2, InvestigationId = 2, Name = "test"}
                    }
                }
            }.AsQueryable();
        }
    }
}

