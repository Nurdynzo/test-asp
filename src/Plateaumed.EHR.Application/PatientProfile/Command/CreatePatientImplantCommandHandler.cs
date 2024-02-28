using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreatePatientImplantCommandHandler : ICreatePatientImplantCommandHandler
    {
        private readonly IRepository<PatientImplant, long> _patientImplantRepository;
        private readonly IObjectMapper _mapper;
        private readonly IRepository<Patient, long> _patientRepository;
        public CreatePatientImplantCommandHandler(
            IRepository<PatientImplant, long> patientImplantRepository,
            IObjectMapper mapper,
            IRepository<Patient, long> patientRepository)
        {
            _patientImplantRepository = patientImplantRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreatePatientImplantCommandRequestDto request)
        {
            var patient = await _patientRepository.GetAll()
                .SingleOrDefaultAsync(x => x.Id == request.PatientId);
            if (patient is null) return;
            var patientImplant = _mapper.Map<PatientImplant>(request);
            await _patientImplantRepository.InsertAsync(patientImplant);
        }
    }
}
