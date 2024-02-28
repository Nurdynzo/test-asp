using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Medication.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Medication.Abstractions;
namespace Plateaumed.EHR.Medication.Query
{
    public class GetViewMedicationQueryHandler : IGetViewMedicationQueryHandler
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        public GetViewMedicationQueryHandler(IRepository<AllInputs.Medication, long> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<List<MedicalViewResponse>> Handle(long patientId)
        {
            var query = (from medication in _medicationRepository.GetAll()
                         where medication.PatientId == patientId
                         orderby medication.CreationTime descending
                         select new MedicalViewResponse
                         {
                             Id = medication.Id,
                             ProductName = medication.ProductName,
                             Description = $"{medication.DoseUnit} {medication.Frequency} for {medication.Duration} to be taken {medication.Direction}"
                         });
            return await query.ToListAsync();
        }

    }
}
