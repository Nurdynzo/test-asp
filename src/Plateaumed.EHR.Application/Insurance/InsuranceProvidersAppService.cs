using Plateaumed.EHR.Insurance;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Insurance.Dtos;
using Plateaumed.EHR.Dto;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;
using System.Reflection;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Insurance
{
    [AbpAuthorize(AppPermissions.Pages_InsuranceProviders)]
    public class InsuranceProvidersAppService : EHRAppServiceBase, IInsuranceProvidersAppService
    {
        private readonly IRepository<InsuranceProvider, long> _insuranceProviderRepository;

        public InsuranceProvidersAppService(
            IRepository<InsuranceProvider, long> insuranceProviderRepository
            )
        {
            _insuranceProviderRepository = insuranceProviderRepository;
        }

        public async Task<PagedResultDto<GetInsuranceProviderForViewDto>> GetAll(GetAllInsuranceProvidersInput input)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;

            var filteredInsuranceProviders = _insuranceProviderRepository.GetAll()
                .IgnoreQueryFilters()
                .Include(i => i.CountryFk)
                .WhereIf(filterTerms != null, i => filterTerms.All(term => i.Name.ToLower().Contains(term)))
                .WhereIf(input.TypeFilter.HasValue, i => i.Type == input.TypeFilter);

            var pagedAndFilteredInsuranceProviders = filteredInsuranceProviders
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredInsuranceProviders.CountAsync();

            var insuranceProviders = await pagedAndFilteredInsuranceProviders.ToListAsync();

            var results = insuranceProviders.Select(insuranceProvider => new GetInsuranceProviderForViewDto
            {
                InsuranceProvider = ObjectMapper.Map<InsuranceProviderDto>(insuranceProvider),
                Country = insuranceProvider.CountryFk?.Name
            }).ToList();

            return new PagedResultDto<GetInsuranceProviderForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_InsuranceProviders_Edit)]
        public List<string> GetInsuranceProviderTypes()
        {
            var providerTypes = Enum
                  .GetValues<InsuranceProviderType>()
                  .AsEnumerable()
                  .Select(r => r.ToString())
                  .ToList();

            return providerTypes;
        }

        [AbpAuthorize(AppPermissions.Pages_InsuranceProviders_Edit)]
        public async Task<GetInsuranceProviderForEditOutput> GetInsuranceProviderForEdit(EntityDto<long> input)
        {
            var insuranceProvider = await _insuranceProviderRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInsuranceProviderForEditOutput
            {
                InsuranceProvider = ObjectMapper.Map<CreateOrEditInsuranceProviderDto>(insuranceProvider)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInsuranceProviderDto input)
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

        [AbpAuthorize(AppPermissions.Pages_InsuranceProviders_Create)]
        protected virtual async Task Create(CreateOrEditInsuranceProviderDto input)
        {
            var insuranceProvider = ObjectMapper.Map<InsuranceProvider>(input);

            if (AbpSession.TenantId != null)
            {
                insuranceProvider.TenantId = (int)AbpSession.TenantId;
            }

            await _insuranceProviderRepository.InsertAsync(insuranceProvider);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_InsuranceProviders_Edit)]
        protected virtual async Task Update(CreateOrEditInsuranceProviderDto input)
        {
            var insuranceProvider = await _insuranceProviderRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, insuranceProvider);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_InsuranceProviders_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _insuranceProviderRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAllowAnonymous]
        public async Task<PagedResultDto<InsuranceProviderForLookupTableDto>>
            GetAllInsuranceProvidersForLookupTable(GetAllInsuranceProvidersForLookupTableInput input)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;

            var query = _insuranceProviderRepository.GetAll()
                .WhereIf(filterTerms != null, i => filterTerms.All(term => i.Name.ToLower().Contains(term)))
                .WhereIf(input.TypeFilter.HasValue, i => i.Type == input.TypeFilter)
                .WhereIf(input.CountryIdFilter.HasValue, i => i.CountryId == input.CountryIdFilter);

            var totalCount = await query.CountAsync();

            var insuranceProviderList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = insuranceProviderList.Select(insuranceProvider => new InsuranceProviderForLookupTableDto {
                Id = insuranceProvider.Id,
                DisplayName = insuranceProvider.Name?.ToString()
            }).ToList();

            return new PagedResultDto<InsuranceProviderForLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}