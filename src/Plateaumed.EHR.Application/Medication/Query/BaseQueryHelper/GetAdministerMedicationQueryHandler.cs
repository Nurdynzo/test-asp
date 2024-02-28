using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
namespace Plateaumed.EHR.Medication.Query.BaseQueryHelper
{
    public class GetAdministerMedicationQueryHandler : IGetAdministerMedicationQueryHandler
    {
        private readonly IRepository<MedicationAdministrationActivity, long> _medicationAdministrationActivityRepository;
        public GetAdministerMedicationQueryHandler(IRepository<MedicationAdministrationActivity, long> medicationAdministrationActivityRepository)
        {
            _medicationAdministrationActivityRepository = medicationAdministrationActivityRepository;
        }
        public async Task<List<MedicationAdministrationActivityResponse>> Handle(long encounterId)
        {
            var medicationAdministrationActivities
                = await (from m in _medicationAdministrationActivityRepository.GetAll()
                         where m.PatientEncounterId == encounterId
                         orderby m.CreationTime descending
                         select new MedicationAdministrationActivityResponse
                         {
                             Id = m.Id,
                             PatientEncounterId = m.PatientEncounterId,
                             MedicationId = m.MedicationId,
                             IsAvailable = m.IsAvailable,
                             Direction = m.Direction,
                             Note = m.Note,
                             DoseUnit = m.DoseUnit,
                             DoseValue = m.DoseValue,
                             FrequencyUnit = m.FrequencyUnit,
                             FrequencyValue = m.FrequencyValue,
                             DurationUnit = m.DurationUnit,
                             DurationValue = m.DurationValue,
                             ProductName = m.ProductName,
                             CreatedDate = m.CreationTime

                         }).ToListAsync();
            return medicationAdministrationActivities;


        }
    }
}
