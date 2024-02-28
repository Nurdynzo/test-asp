using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
namespace Plateaumed.EHR.WardEmergencies.Handlers
{
    public class DeleteInterventionCommandHandler : IDeleteInterventionCommandHandler
    {
        private readonly IRepository<PatientIntervention, long> _patientInterventionRepository;

        public DeleteInterventionCommandHandler(IRepository<PatientIntervention, long> patientInterventionRepository)
        {
            _patientInterventionRepository = patientInterventionRepository;
        }

        public async Task Handle(long interventionId)
        {
            var intervention = await _patientInterventionRepository.GetAsync(interventionId)
                ?? throw new UserFriendlyException($"Intervention with Id {interventionId} does not exist.");
            await _patientInterventionRepository.DeleteAsync(intervention);
        }
    }
}
