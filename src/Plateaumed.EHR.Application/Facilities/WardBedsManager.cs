using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Facilities
{
    public class WardBedsManager : IWardBedsManager 
    {
        private readonly IRepository<WardBed, long> _repository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;

        public WardBedsManager(IRepository<WardBed, long> repository, IRepository<PatientEncounter, long> patientEncounterRepository)
        {
            _repository = repository;
            _patientEncounterRepository = patientEncounterRepository;
        }

        public async Task OccupyWardBed(long wardBedId, long encounterId)
        {
            ValidateInputs(wardBedId, encounterId);

            _ = await _patientEncounterRepository.GetAsync(encounterId) ??
                throw new UserFriendlyException($"Encounter with ID {encounterId} not found");

            var wardBed = await _repository.GetAsync(wardBedId) ?? throw new UserFriendlyException($"WardBed with ID {wardBedId} not found");

            if (wardBed.EncounterId is not null)
                throw new UserFriendlyException($"WardBed with ID {wardBedId} Is already occupied");

            if (wardBed is not null)
            {
                wardBed.EncounterId = encounterId;
                await _repository.UpdateAsync(wardBed);
            }            
        }

        public async Task DeOccupyWardBed(long? wardBedId)
        {
            if (wardBedId.HasValue)
            {
                var wardBed = await _repository.GetAsync(wardBedId.Value) ?? throw new UserFriendlyException($"WardBed with ID {wardBedId} not found");
                if (wardBed is not null)
                {
                    wardBed.EncounterId = null;
                    await _repository.UpdateAsync(wardBed);
                }
            }
        }      

        private static void ValidateInputs(long wardBedId, long encounterId)
        {
            if (wardBedId <= 0)
                throw new UserFriendlyException("Invalid WardBed Id");
            if (encounterId <= 0)
                throw new UserFriendlyException("Invalid EncounterId");           
        }
    }
}

