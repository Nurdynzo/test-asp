using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Handlers.Common;
namespace Plateaumed.EHR.Medication.Handlers
{
    public class MarkMedicationForAdministerCommandHandler : IMarkMedicationForAdministerCommandHandler
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        public MarkMedicationForAdministerCommandHandler(IRepository<AllInputs.Medication, long> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }
        public async Task Handle(List<long> medicationId)
        {
            await CommonMedicationUpdate.UpdateIsAdministerOrIsContinue(_medicationRepository, medicationId, isAdminister: true);
        }
    }
}
