using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions;

public interface IGetAllInvestigationsQueryHandler : ITransientDependency
{
    Task<List<GetAllInvestigationsResponse>> Handle(GetAllInvestigationsRequest request);
}