using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientCodeTemplatesAppService : IApplicationService
    {
        Task<GetPatientCodeTemplateForEditOutput> GetPatientCodeTemplateForEdit(
            EntityDto<long> input
        );

        Task<PatientCodeTemplateDto> GetFacilityPatientCodeTemplateByFacilityId(long facilityId);

        Task CreateOrEdit(CreateOrEditPatientCodeTemplateDto input);

        Task Delete(EntityDto<long> input);
    }
}
