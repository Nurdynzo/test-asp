using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions
{
    public interface ICreatePatientStabilityStatusCommandHandler: ITransientDependency
    {
        Task Handle(PatientStabilityRequestDto request);    
    }
}

