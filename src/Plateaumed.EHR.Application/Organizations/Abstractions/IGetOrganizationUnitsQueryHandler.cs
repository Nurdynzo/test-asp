using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Organizations.Dto;

namespace Plateaumed.EHR.Organizations.Abstractions;

public interface IGetOrganizationUnitsQueryHandler : ITransientDependency
{
    Task<ListResultDto<OrganizationUnitDto>> Handle(GetOrganizationUnitsInput input, int tenantId);
}
