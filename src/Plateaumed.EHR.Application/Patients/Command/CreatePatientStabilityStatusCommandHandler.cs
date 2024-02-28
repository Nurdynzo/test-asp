using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Command
{
    public class CreatePatientStabilityStatusCommandHandler: ICreatePatientStabilityStatusCommandHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IRepository<PatientStability, long> _patientStability;
        private readonly IAbpSession _abpSession;

        public CreatePatientStabilityStatusCommandHandler(IRepository<Patient, long> patientRepository,
            IRepository<PatientEncounter, long> patientEncounterRepository,
            IRepository<PatientStability, long> patientStability,
            IAbpSession abpSession
            )
        {
            _patientRepository = patientRepository;
            _patientEncounterRepository = patientEncounterRepository;
            _patientStability = patientStability;
            _abpSession = abpSession;
        }

        public async Task Handle(PatientStabilityRequestDto request)
        {
            var tenantId = _abpSession.TenantId ?? throw new UserFriendlyException("User Not logged In");
            await ValidateInputs(request);
            await _patientStability.InsertAsync(new PatientStability
            {
                PatientId = request.PatientId,
                EncounterId = request.EncounterId,
                Status = request.Status,
                TenantId = tenantId
            });
        }

        private async Task ValidateInputs(PatientStabilityRequestDto request)
        {          
            _ = await _patientRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.PatientId) ?? throw new UserFriendlyException("Patient not found");
            _ = await _patientEncounterRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.EncounterId) ?? throw new UserFriendlyException("Encounter not found");
        }
    }
}

