using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetPatientStabilityStatusQueryHandler : IGetPatientStabilityStatusQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientStability, long> _patientStability;
        private readonly IRepository<PatientEncounter, long> _patientEncounter;

        public GetPatientStabilityStatusQueryHandler(IRepository<Patient, long> patientRepository,
            IRepository<PatientStability, long> patientStability,
            IRepository<PatientEncounter, long> patientEncounter)
        {
            _patientRepository = patientRepository;
            _patientStability = patientStability;
            _patientEncounter = patientEncounter;
        }

        public async Task<List<PatientStabilityResponseDto>> Handle(long patientId, long encounterId) =>
            await GetStabilityStatus(patientId, encounterId);        

        private async Task<List<PatientStabilityResponseDto>> GetStabilityStatus(long patientId, long encounterId)
        {
            await ValidateInputs(patientId, encounterId);

            return await _patientStability.GetAll()
                .Where(x => x.PatientId == patientId && x.EncounterId == encounterId)
                .OrderByDescending(x=>x.CreationTime)
                .Select(x => new PatientStabilityResponseDto
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    Status = x.Status,
                    CreatorUserId = x.CreatorUserId.Value,
                    CreationTime = x.CreationTime
                }).ToListAsync();            
        }       

        private async Task ValidateInputs(long patientId, long encounterId)
        {
            _ = await _patientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new UserFriendlyException("Patient not found");
            _ = await _patientEncounter.GetAll().FirstOrDefaultAsync(x => x.Id == encounterId) ?? throw new UserFriendlyException("Encounter not found");
        }
    }
}
