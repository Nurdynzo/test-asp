using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface ICreatePatientPastMedicalHistoryHandler : ITransientDependency
{
    Task Handle(PatientPastMedicalConditionCommandRequest request);
}