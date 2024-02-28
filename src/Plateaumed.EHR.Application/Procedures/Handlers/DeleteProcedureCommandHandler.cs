using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Procedures.Abstractions;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class DeleteProcedureCommandHandler : IDeleteProcedureCommandHandler
    {
        private readonly IRepository<Procedure, long> _procedureRepository;
        public DeleteProcedureCommandHandler(IRepository<Procedure, long> procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }
        public async Task Handle(long id)
        => await _procedureRepository.DeleteAsync(id);

    }
}
