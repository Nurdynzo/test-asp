using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface ICreatePhysicalExerciseCommandHandler: ITransientDependency
{
    Task Handle(CreatePhysicalExerciseCommandRequest request);
}