using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface IGetPatientPhysicalExerciseQueryHandler: ITransientDependency
{
    Task<GetPatientPhysicalExerciseQueryResponse> Handle(long patientId);
}