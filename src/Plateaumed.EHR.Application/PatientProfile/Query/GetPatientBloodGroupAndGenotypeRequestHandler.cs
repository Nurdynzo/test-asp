using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientBloodGroupAndGenotypeRequestHandler : IGetPatientBloodGroupAndGenotypeRequestHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;

        public GetPatientBloodGroupAndGenotypeRequestHandler(IRepository<Patient, long> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<GetPatientBloodGroupAndGenotypeResponseDto> Handle(long id)
        {
            var patient = await _patientRepository.GetAll().SingleOrDefaultAsync(p => p.Id == id);
            return new GetPatientBloodGroupAndGenotypeResponseDto
            {
                PatientId = id,
                BloodGroup = patient.BloodGroup.ToString(),
                Genotype = patient.BloodGenotype.ToString(),
                GenotypeSource = patient.GenotypeSource.ToString(),
                BloodGroupSource = patient.BloodGroupSource.ToString(),
            };
        }
    }
}
