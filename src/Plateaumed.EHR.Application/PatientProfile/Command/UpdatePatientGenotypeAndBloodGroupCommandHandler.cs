using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Command;

public class UpdatePatientGenotypeAndBloodGroupCommandHandler : IUpdatePatientGenotypeAndBloodGroupCommandHandler
{
    private readonly IRepository<Patient,long> _patientRepository;

    public UpdatePatientGenotypeAndBloodGroupCommandHandler(IRepository<Patient, long> patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task Handle(UpdatePatientGenotypeAndBloodGroupCommandRequest request)
    {
        var existingPatient = await _patientRepository.GetAll().FirstOrDefaultAsync(v => v.Id == request.PatientId) ??
                              throw new UserFriendlyException("Patient not found");
        existingPatient.GenotypeSource = request.GenotypeSource;
        existingPatient.BloodGroupSource = request.BloodGroupSource;
        existingPatient.BloodGroup = request.BloodGroup;
        existingPatient.BloodGenotype = request.BloodGenotype;
        await _patientRepository.UpdateAsync(existingPatient);
    }
}