using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilitiesAppService : IApplicationService
    {
        Task<PagedResultDto<GetFacilityForViewDto>> GetAll(GetAllFacilitiesInput input);

        Task<GetFacilityForEditOutput> GetFacilityForEdit(EntityDto<long> input);

        Task<List<GetUserFacilityViewDto>> GetUserFacility(string email);

        Task CreateOrEdit(CreateOrEditFacilityDto input);

        Task CreateOrEditMultipleFacilities(List<CreateOrEditFacilityDto> input);

        Task ActivateorDeactivateFacility(ActivateOrDeactivateFacility input);

        Task Delete(EntityDto<long> input);

        Task<PagedResultDto<FacilityFacilityGroupLookupTableDto>> GetAllFacilityGroupForLookupTable(
            GetAllForLookupTableInput input
        );

        Task<PagedResultDto<FacilityFacilityTypeLookupTableDto>> GetAllFacilityTypeForLookupTable(
            GetAllForLookupTableInput input
        );

        Task ActivateOrDeactivatePharmacy(ActivateOrDeactivatePharmacyRequest input);

        Task ActivateOrDeactivateLaboratory(ActivateOrDeactivateLaboratoryRequest input);
    }
}
