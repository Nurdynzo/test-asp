using System.Threading.Tasks;
using Abp.Domain.Repositories;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Handlers;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class DeleteInvestigationRequestCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldDeleteInvestigationRequest()
        {
            // Arrange
            const long requestId = 43;
            var repository = Substitute.For<IRepository<InvestigationRequest, long>>();
            var handler = new DeleteInvestigationRequestCommandHandler(repository);
            // Act
            await handler.Handle(requestId);
            // Assert
            await repository.Received(1).DeleteAsync(requestId);
        }
    }
}
