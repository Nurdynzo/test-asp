using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Diagnoses.Abstraction;
namespace Plateaumed.EHR.Diagnoses.Handlers
{
    public class DeletePatientDiagnosisCommandHandler : IDeletePatientDiagnosisCommandHandler
    {
        private readonly IRepository<Diagnosis, long> _diagnosisRepository;
        public DeletePatientDiagnosisCommandHandler(IRepository<Diagnosis, long> diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }
        public async Task Handle(long diagnosisId)
        {
            var diagnosis = await _diagnosisRepository.GetAsync(diagnosisId)
                            ?? throw new UserFriendlyException($"Diagnosis with Id {diagnosisId} does not exist.");
            await _diagnosisRepository.DeleteAsync(diagnosis);
        }
    }
}
