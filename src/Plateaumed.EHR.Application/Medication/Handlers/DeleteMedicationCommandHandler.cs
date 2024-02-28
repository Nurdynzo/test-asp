using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Medication.Abstractions;
namespace Plateaumed.EHR.Medication.Handlers
{
    public class DeleteMedicationCommandHandler : IDeleteMedicationCommandHandler
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        public DeleteMedicationCommandHandler(IRepository<AllInputs.Medication, long> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }
        public async Task Handle(long id)
        {
            var medication = await _medicationRepository.GetAsync(id) ?? throw new UserFriendlyException("Medication not found");
            await _medicationRepository.DeleteAsync(medication);
        }
    }
}
