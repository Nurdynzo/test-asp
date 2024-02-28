using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies;

public interface ICreatePatientInterventionCommandHandler : ITransientDependency
{
    Task Handle(CreatePatientInterventionRequest request);
}