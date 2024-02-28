using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations;

public interface IGetInvestigationResultsQueryHandler : ITransientDependency
{
    Task<List<GetInvestigationResultsResponse>> Handle(GetInvestigationResultsRequest request);
}