using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilityDocumentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetFacilityDocumentForViewDto>> GetAll(GetAllFacilityDocumentsInput input);

        Task<List<GetFacilityDocumentForViewDto>> GetAllFacilityDocumentForView(EntityDto<long> input);

        Task<GetFacilityDocumentForEditOutput> GetFacilityDocumentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditFacilityDocumentDto input);

        Task Delete(EntityDto<long> input);

        Task RemoveDocumentFile(EntityDto<long> input);

        Task<PagedResultDto<FacilityDocumentFacilityLookupTableDto>> GetAllFacilitiesForLookupTable(GetAllForLookupTableInput input);
    }
}
