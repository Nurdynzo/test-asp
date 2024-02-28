using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilityStaffAppService : IApplicationService
    {
        Task<PagedResultDto<GetFacilityStaffForViewDto>> GetAll(GetAllFacilityStaffInput input);

        Task<GetFacilityStaffForEditOutput> GetFacilityStaffForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditFacilityStaffDto input);

        Task Delete(EntityDto<long> input);

        Task<PagedResultDto<FacilityStaffFacilityLookupTableDto>> GetAllFacilityForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<FacilityStaffStaffMemberLookupTableDto>> GetAllStaffMemberForLookupTable(GetAllForLookupTableInput input);

    }
}