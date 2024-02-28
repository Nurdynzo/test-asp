using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Abstraction;
namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeletePatientMenstrualFlowCommandHandler : IDeletePatientMenstrualFlowCommandHandler
    {
        private readonly IRepository<PatientMenstrualFlow, long> _patientMenstrualFlowRepository;
        public DeletePatientMenstrualFlowCommandHandler(IRepository<PatientMenstrualFlow, long> patientMenstrualFlowRepository)
        {
            _patientMenstrualFlowRepository = patientMenstrualFlowRepository;
        }
        public async Task Handle(long id) =>
            await _patientMenstrualFlowRepository.DeleteAsync(id).ConfigureAwait(false);
    }
}
