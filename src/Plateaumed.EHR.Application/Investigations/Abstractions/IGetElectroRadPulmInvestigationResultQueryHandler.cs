using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions
{
    public interface IGetElectroRadPulmInvestigationResultQueryHandler: ITransientDependency
    {
        Task<ElectroRadPulmInvestigationResultResponseDto> Handle(long patientId, long investigationRequestId);
    }
}

