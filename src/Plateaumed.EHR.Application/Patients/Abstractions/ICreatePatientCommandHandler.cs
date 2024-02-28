using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Abstractions;

public interface ICreatePatientCommandHandler: ITransientDependency
{
    Task<CreateOrEditPatientDto> Handle(CreateOrEditPatientDto request, long facilityId);
}