using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Patients
{
    public interface IPatientInsurersAppService : IApplicationService
    {
        Task<PagedResultDto<GetPatientInsurerForViewDto>> GetAll(GetAllPatientInsurersInput input);

        Task<GetPatientInsurerForEditOutput> GetPatientInsurerForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditPatientInsurerDto input);

        Task Delete(EntityDto<long> input);

        Task<
            PagedResultDto<PatientInsurerInsuranceProviderLookupTableDto>
        > GetAllInsuranceProviderForLookupTable(GetAllForLookupTableInput input);
    }
}
