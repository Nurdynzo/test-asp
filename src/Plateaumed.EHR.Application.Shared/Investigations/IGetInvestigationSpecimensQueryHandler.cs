using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations;

public interface IGetInvestigationSpecimensQueryHandler : ITransientDependency
{
    Task<GetInvestigationSpecimensResponse> Handle(GetInvestigationSpecimensRequest request);
}