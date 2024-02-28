using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Plateaumed.EHR.Medication.Handlers.Common
{
    public class CommonMedicationUpdate
    {
        public static async Task UpdateIsAdministerOrIsContinue(
            IRepository<AllInputs.Medication, long> medicationRepository,
            List<long> medicationIds,
            bool isAdminister = false,
            bool isDiscontinue = false,
            long? discontinueUserId=null)
        {
            var medications = await medicationRepository
                .GetAll()
                .Where(m => medicationIds.Contains(m.Id) && ((isAdminister && !m.IsAdministered) || (isDiscontinue && !m.IsDiscontinued)))
                .ToListAsync();

            foreach (var medication in medications)
            {
                medication.IsAdministered = isAdminister || medication.IsAdministered;
                medication.IsDiscontinued = isDiscontinue || medication.IsDiscontinued;
                medication.DiscontinueUserId = discontinueUserId;
                await medicationRepository.UpdateAsync(medication);
            }

        }

    }
}
