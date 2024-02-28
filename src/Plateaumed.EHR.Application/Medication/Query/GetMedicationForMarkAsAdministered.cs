using System.Collections.Generic;
using Abp.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
namespace Plateaumed.EHR.Medication.Query
{
    public class GetMedicationForMarkAsAdministered : IGetMedicationForMarkAsAdministered
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        public GetMedicationForMarkAsAdministered(IRepository<AllInputs.Medication, long> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }
        public async Task<List<MedicationSummaryQueryResponse>> Handle(long patientId, long encounterId)
        {
            var query = await (from m in _medicationRepository.GetAll()
                               where m.PatientId == patientId &&
                                     m.EncounterId == encounterId &&
                                     !m.IsAdministered && !m.IsDiscontinued
                               orderby m.CreationTime descending
                               select new MedicationSummaryQueryResponse()
                               {
                                   Id = m.Id,
                                   ProductName = m.ProductName,
                                   Description = $"{m.DoseUnit}, {m.Frequency}, {m.Duration}, {m.Direction}",
                               }).ToListAsync();
            return query;
        }
    }
}
