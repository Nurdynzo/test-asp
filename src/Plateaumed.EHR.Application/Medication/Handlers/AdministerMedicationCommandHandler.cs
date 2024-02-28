using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
namespace Plateaumed.EHR.Medication.Handlers
{
    public class AdministerMedicationCommandHandler : IAdministerMedicationCommandHandler
    {
        private readonly IRepository<MedicationAdministrationActivity, long> _medicationAdministrationActivityRepository;
        private readonly IEncounterManager _encounterManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        public AdministerMedicationCommandHandler(
            IRepository<MedicationAdministrationActivity, long> medicationAdministrationActivityRepository,
            IEncounterManager encounterManager,
            IObjectMapper objectMapper,
            IRepository<AllInputs.Medication, long> medicationRepository)
        {
            _medicationAdministrationActivityRepository = medicationAdministrationActivityRepository;
            _encounterManager = encounterManager;
            _objectMapper = objectMapper;
            _medicationRepository = medicationRepository;
        }
        public async Task Handle(MedicationAdministrationActivityRequest request)
        {
            var medicationAdministrationActivity = _objectMapper.Map<MedicationAdministrationActivity>(request);
            await ValidateRequest(request);
            await _medicationAdministrationActivityRepository.InsertAsync(medicationAdministrationActivity);

        }
        private async Task ValidateRequest(MedicationAdministrationActivityRequest request)
        {

            _ =  await _medicationRepository.GetAsync(request.MedicationId) ?? throw new UserFriendlyException($"Medication with id {request.MedicationId} doesn't exist.");
            await _encounterManager.CheckEncounterExists(request.PatientEncounterId);
        }
    }
}
