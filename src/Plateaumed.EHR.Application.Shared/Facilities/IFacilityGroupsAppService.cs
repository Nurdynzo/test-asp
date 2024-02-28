using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilityGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<GetFacilityGroupForViewDto>> GetAll(GetAllFacilityGroupsInput input);

        Task<GetFacilityGroupForEditOutput> GetFacilityGroupForEdit(EntityDto<long> input);

        Task<GetFacilityGroupForEditOutput> GetFacilityGroupDetails();

        Task<GetFacilityGroupBankDetailsForEditOutput> GetFacilityGroupBankDetails();

        Task<GetFacilityGroupPatientDetailsForEditOutput> GetFacilityGroupPatientCodeTemplateDetails();

        Task CreateOrEdit(CreateOrEditFacilityGroupDto input);

        Task CreateOrEditBankDetails(CreateOrEditFacilityGroupBankRequest input);

        Task CreateOrEditPatientCodeTemplateDetails(CreateOrEditFacilityGroupPatientCodeTemplateDto input);

        Task Delete(EntityDto<long> input);
    }
}
