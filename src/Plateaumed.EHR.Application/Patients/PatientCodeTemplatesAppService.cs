using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Patients
{
    [AbpAuthorize(AppPermissions.Pages_PatientCodeTemplates)]
    public class PatientCodeTemplatesAppService : EHRAppServiceBase, IPatientCodeTemplatesAppService
    {
        private readonly IRepository<PatientCodeTemplate, long> _patientCodeTemplateRepository;
        private readonly IGetPatientCodeTemplateByFacilityIdQueryHandler _getPatientCodeTemplateByFacilityId;
        private readonly ICreateFacilityPatientCodeTemplateCommandHandler _createFacilityPatientCodeTemplateCommand;

        public PatientCodeTemplatesAppService(IRepository<PatientCodeTemplate, long> patientCodeTemplateRepository,
            IGetPatientCodeTemplateByFacilityIdQueryHandler getPatientCodeTemplateByFacilityId,
            ICreateFacilityPatientCodeTemplateCommandHandler createFacilityPatientCodeTemplateCommand)
        {
            _patientCodeTemplateRepository = patientCodeTemplateRepository;
            _getPatientCodeTemplateByFacilityId = getPatientCodeTemplateByFacilityId;
            _createFacilityPatientCodeTemplateCommand = createFacilityPatientCodeTemplateCommand;
        }

        [AbpAuthorize(AppPermissions.Pages_PatientCodeTemplates_Edit)]
        public async Task<GetPatientCodeTemplateForEditOutput> GetPatientCodeTemplateForEdit(EntityDto<long> input)
        {
            var patientCodeTemplate = await _patientCodeTemplateRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPatientCodeTemplateForEditOutput {
                PatientCodeTemplate = ObjectMapper.Map<CreateOrEditPatientCodeTemplateDto>(patientCodeTemplate)
                
            };
            return output;
        }

        public async Task<PatientCodeTemplateDto> GetFacilityPatientCodeTemplateByFacilityId(long facilityId)
        {
            return await _getPatientCodeTemplateByFacilityId.Handle(facilityId);
        }

        public async Task CreateOrEdit(CreateOrEditPatientCodeTemplateDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PatientCodeTemplates_Create)]
        protected virtual async Task Create(CreateOrEditPatientCodeTemplateDto input)
        {
            await _createFacilityPatientCodeTemplateCommand.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientCodeTemplates_Edit)]
        protected virtual async Task Update(CreateOrEditPatientCodeTemplateDto input)
        {
            var patientCodeTemplate = await _patientCodeTemplateRepository.FirstOrDefaultAsync((long)input.Id);

            ObjectMapper.Map(input, patientCodeTemplate);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_PatientCodeTemplates_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _patientCodeTemplateRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}