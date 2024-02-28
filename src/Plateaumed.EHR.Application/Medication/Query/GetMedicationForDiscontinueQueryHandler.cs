using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Medication.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Medication.Abstractions;
namespace Plateaumed.EHR.Medication.Query
{
    public class GetMedicationForDiscontinueQueryHandler : IGetMedicationForDiscontinueQueryHandler
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        public GetMedicationForDiscontinueQueryHandler(IRepository<AllInputs.Medication, long> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }
        public async Task<List<MedicationSummaryQueryResponse>> Handle(long patientId, long encounterId)
        {
            var query = await (from m in _medicationRepository.GetAll()
                               where m.PatientId == patientId &&
                                     m.EncounterId == encounterId &&
                                     !m.IsDiscontinued
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
