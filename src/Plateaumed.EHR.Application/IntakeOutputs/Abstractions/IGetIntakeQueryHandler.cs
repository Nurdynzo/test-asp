using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.IntakeOutputs.Dtos;

namespace Plateaumed.EHR.IntakeOutputs.Abstractions;

public interface IGetIntakeQueryHandler : ITransientDependency
{
    Task<PatientIntakeOutputDto> Handle(long patientId);
}
