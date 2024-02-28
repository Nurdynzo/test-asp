using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class DeleteInvestigationRequestCommandHandler : IDeleteInvestigationRequestCommandHandler
    {
        private readonly IRepository<InvestigationRequest, long> _repository;

        public DeleteInvestigationRequestCommandHandler(IRepository<InvestigationRequest, long> repository)
        {
            _repository = repository;
        }

        public async Task Handle(long requestId)
        {
            await _repository.DeleteAsync(requestId);
        }
    }
}