using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Misc;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetPatientInvestigationsCommandHandlerTests
	{
        private readonly IRepository<InvestigationRequest, long> _investigationRequests = Substitute.For<IRepository<InvestigationRequest, long>>();
        private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();

        [Fact]
        public async Task HandleGivenInvestigationIdsShouldReturnInvestigationsAndStatus()
        {
            var Ids = new List<long> { 1, 2 };

            var request = new GetPatientInvestigationRequest { PatientId = 1, InvestigationIds = Ids};

            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());
            _abpSession.TenantId.Returns(1);

            var handler = new GetInvestigationForPatientCommandHandler(_investigationRequests,_abpSession);

            var result = await handler.GetInvestigationForPatient(request);

            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }

        [Fact]
        public async Task HandleGivenInvestigationIdsWithNoRequestedStatusShouldReturnCountZero()
        {
            var ids = new List<long> { 3,4 };

            var request = new GetPatientInvestigationRequest { PatientId = 1, InvestigationIds = ids };

            _investigationRequests.GetAll().Returns(GetInvestigationRequests().BuildMock());

            var handler = new GetInvestigationForPatientCommandHandler(_investigationRequests,_abpSession);

            var result = await handler.GetInvestigationForPatient(request);

            result.ShouldNotBeNull();
            result.Count.ShouldBe(0);
        }

        private static IQueryable<InvestigationRequest> GetInvestigationRequests()
        {
            return new List<InvestigationRequest>
            {
                new()
                {
                    InvestigationId = 1,
                    PatientId = 1,
                    InvestigationStatus = InvestigationStatus.Requested,
                    TenantId = 1
                },
                new()
                {
                    InvestigationId = 2,
                    PatientId = 1,
                    InvestigationStatus = InvestigationStatus.Invoiced,
                    TenantId = 1
                }
            }.AsQueryable();
        }

    }
}
