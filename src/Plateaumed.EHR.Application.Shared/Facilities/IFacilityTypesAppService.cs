using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilityTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetFacilityTypeForViewDto>> GetAll(GetAllFacilityTypesInput input);

        Task<GetFacilityTypeForEditOutput> GetFacilityTypeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditFacilityTypeDto input);

        Task Delete(EntityDto<long> input);
    }
}
