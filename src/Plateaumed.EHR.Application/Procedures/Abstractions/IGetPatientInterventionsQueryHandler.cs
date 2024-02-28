using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions
{
    public interface IGetPatientInterventionsQueryHandler : ITransientDependency
    {
        Task<GetPatientInterventionsResponseDto> Handle(long patientId, long facilityId);
    }
}

