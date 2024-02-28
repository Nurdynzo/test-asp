using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilityInsurersAppService : IApplicationService
    {
        Task<PagedResultDto<GetFacilityInsurerForViewDto>> GetAll(GetAllFacilityInsurersInput input);

        Task<GetFacilityInsurersForEditOutput> GetFacilityInsurersForEdit(EntityDto<long> input);

        Task CreateOrEditMultiple(List<CreateOrEditFacilityInsurerDto> input);

        Task CreateOrEdit(CreateOrEditFacilityInsurerDto input);

        Task Delete(EntityDto<long> input);

        Task ActivateOrDeactivateFacilityInsurer(ActivateOrDeactivateFacilityInsurerRequest input);

        Task<PagedResultDto<FacilityInsurerFacilityGroupLookupTableDto>> GetAllFacilityGroupForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<FacilityInsurerFacilityLookupTableDto>> GetAllFacilityForLookupTable(GetAllForLookupTableInput input);
    }
}