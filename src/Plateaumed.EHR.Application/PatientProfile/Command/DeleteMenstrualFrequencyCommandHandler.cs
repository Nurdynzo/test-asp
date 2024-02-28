using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Abstraction;
namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteMenstrualFrequencyCommandHandler : IDeleteMenstrualFrequencyCommandHandler
    {
        private readonly IRepository<PatientMensurationDuration, long> _mensurationDurationRepository;
        public DeleteMenstrualFrequencyCommandHandler(IRepository<PatientMensurationDuration, long> mensurationDurationRepository)
        {
            _mensurationDurationRepository = mensurationDurationRepository;
        }
        public async Task Handle(long id)
            => await _mensurationDurationRepository.DeleteAsync(id).ConfigureAwait(false);

    }
}
