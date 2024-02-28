using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Organizations.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Organizations
{
    public interface IOrganizationUnitTimesAppService : IApplicationService
    {
        Task<PagedResultDto<GetOrganizationUnitTimeForViewDto>> GetAll(GetAllOrganizationUnitTimesInput input);

        Task<GetOrganizationUnitTimeForEditOutput> GetOrganizationUnitTimeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditOrganizationUnitTimeDto input);

        Task Delete(EntityDto<long> input);

        Task<PagedResultDto<OrganizationUnitTimeOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input);

    }
}