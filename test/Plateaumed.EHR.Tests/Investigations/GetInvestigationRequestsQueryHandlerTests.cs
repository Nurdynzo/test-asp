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
    public class GetInvestigationRequestsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenRequestExists_ShouldMapInvestigation()
        {
            // Arrange
            const long investigationId = 35;
            const long patientId = 123;
            const long investigationRequestId = 1337;

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();
            var request = new GetInvestigationRequestsRequest { PatientId = patientId, Type = "Haematology" };

            var investigationRequest = new InvestigationRequest
            {
                Id = investigationRequestId,
                InvestigationId = investigationId,
                PatientId = patientId,
                Urgent = true,
                Investigation = new Investigation
                {
                    Id = investigationId,
                    Name = "Inv",
                    Specimen = "Blood",
                    SpecificOrganism = "Lactobacillus",
                    Type = "Haematology"
                }
            };

            investigationRequestRepository.GetAll().Returns(new List<InvestigationRequest>{investigationRequest}.AsQueryable().BuildMock());

            var handler = CreateHandler(investigationRequestRepository);
            // Act
            var result = await handler.Handle(request);
            // Assert
            result[0].InvestigationId.ShouldBe(investigationId);
            result[0].Name.ShouldBe("Inv");
            result[0].Specimen.ShouldBe("Blood");
            result[0].SpecificOrganism.ShouldBe("Lactobacillus");
            result[0].Urgent.ShouldBe(true);
            result[0].WithContrast.ShouldBe(false);
        }

        private GetInvestigationRequestsQueryHandler CreateHandler(
            IRepository<InvestigationRequest, long> investigationRequestRepository)
        {
            return new GetInvestigationRequestsQueryHandler(investigationRequestRepository);
        }
    }
}

