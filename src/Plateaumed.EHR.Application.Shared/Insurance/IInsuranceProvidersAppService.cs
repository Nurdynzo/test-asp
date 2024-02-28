using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Insurance.Dtos;
using Plateaumed.EHR.Dto;
using System.Collections.Generic;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Insurance
{
    public interface IInsuranceProvidersAppService : IApplicationService
    {
        Task<PagedResultDto<GetInsuranceProviderForViewDto>> GetAll(GetAllInsuranceProvidersInput input);

        Task<GetInsuranceProviderForEditOutput> GetInsuranceProviderForEdit(EntityDto<long> input);

        List<string> GetInsuranceProviderTypes();

        Task CreateOrEdit(CreateOrEditInsuranceProviderDto input);

        Task Delete(EntityDto<long> input);
    }
}