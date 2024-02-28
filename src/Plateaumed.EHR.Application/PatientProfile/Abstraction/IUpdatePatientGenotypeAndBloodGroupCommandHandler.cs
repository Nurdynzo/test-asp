using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface IUpdatePatientGenotypeAndBloodGroupCommandHandler : ITransientDependency
{
    Task Handle(UpdatePatientGenotypeAndBloodGroupCommandRequest request);
}