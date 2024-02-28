using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using NSubstitute;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class LinkInvestigationToDiagnosisCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidValues_ShouldCreateInvestigation()
        {
            // Arrange
            var command = new LinkInvestigationToDiagnosisRequest
            {
                InvestigationRequestId = 1,
                DiagnosisId = 1
            };

            var investigationRequest = new InvestigationRequest();
            var diagnosis = new Diagnosis();

            var diagnosisRepository = Substitute.For<IRepository<Diagnosis, long>>();
            diagnosisRepository.GetAsync(command.DiagnosisId).Returns(Task.FromResult(diagnosis));

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();
            investigationRequestRepository.GetAsync(command.InvestigationRequestId).Returns(Task.FromResult(investigationRequest));
            InvestigationRequest savedResult = null;
            await investigationRequestRepository.UpdateAsync(Arg.Do<InvestigationRequest>(result => savedResult = result));

            var unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();

            var handler = new LinkInvestigationToDiagnosisCommandHandler(investigationRequestRepository, diagnosisRepository, unitOfWorkManager);
            // Act
            await handler.Handle(command);
            // Assert
            Assert.NotNull(savedResult);
            Assert.Equal(diagnosis, savedResult.Diagnosis);
            await unitOfWorkManager.Received(1).Current.SaveChangesAsync();
        }
    }
}
