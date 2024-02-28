using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Staff.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;


namespace Plateaumed.EHR.Staff
{
    [AbpAuthorize(AppPermissions.Pages_StaffCodeTemplates)]
    public class StaffCodeTemplatesAppService : EHRAppServiceBase, IStaffCodeTemplatesAppService
    {
        private readonly IRepository<StaffCodeTemplate, long> _staffCodeTemplateRepository;

        public StaffCodeTemplatesAppService(
            IRepository<StaffCodeTemplate, long> staffCodeTemplateRepository
        )
        {
            _staffCodeTemplateRepository = staffCodeTemplateRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_StaffCodeTemplates_Edit)]
        public async Task<GetStaffCodeTemplateForEditOutput> GetStaffCodeTemplateForEdit(
            EntityDto<long> input
        )
        {
            var staffCodeTemplate = await _staffCodeTemplateRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetStaffCodeTemplateForEditOutput
            {
                StaffCodeTemplate = ObjectMapper.Map<CreateOrEditStaffCodeTemplateDto>(staffCodeTemplate)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditStaffCodeTemplateDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_StaffCodeTemplates_Create)]
        protected virtual async Task Create(CreateOrEditStaffCodeTemplateDto input)
        {
            var staffCodeTemplate = ObjectMapper.Map<StaffCodeTemplate>(input);

            await _staffCodeTemplateRepository.InsertAsync(staffCodeTemplate);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_StaffCodeTemplates_Edit)]
        protected virtual async Task Update(CreateOrEditStaffCodeTemplateDto input)
        {
            var staffCodeTemplate = await _staffCodeTemplateRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, staffCodeTemplate);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_StaffCodeTemplates_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _staffCodeTemplateRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
