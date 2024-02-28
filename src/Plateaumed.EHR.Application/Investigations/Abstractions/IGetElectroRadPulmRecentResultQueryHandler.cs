using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions
{
    public interface IGetElectroRadPulmRecentResultQueryHandler : ITransientDependency
    {
        Task<List<ElectroRadPulmInvestigationResultResponseDto>> Handle(GetInvestigationResultWithNameTypeFilterDto request);    
    }
}

