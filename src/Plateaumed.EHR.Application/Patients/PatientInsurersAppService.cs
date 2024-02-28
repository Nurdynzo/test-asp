using Plateaumed.EHR.Insurance;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Patients
{
    [AbpAuthorize(AppPermissions.Pages_PatientInsurers)]
    public class PatientInsurersAppService : EHRAppServiceBase, IPatientInsurersAppService
    {
        private readonly IRepository<PatientInsurer, long> _patientInsurerRepository;
        private readonly IRepository<InsuranceProvider, long> _lookup_insuranceProviderRepository;

        public PatientInsurersAppService(
            IRepository<PatientInsurer, long> patientInsurerRepository,
            IRepository<InsuranceProvider, long> lookup_insuranceProviderRepository
        )
        {
            _patientInsurerRepository = patientInsurerRepository;
            _lookup_insuranceProviderRepository = lookup_insuranceProviderRepository;
        }

        public async Task<PagedResultDto<GetPatientInsurerForViewDto>> GetAll(
            GetAllPatientInsurersInput input
        )
        {
            var filteredPatientInsurers = _patientInsurerRepository
                .GetAll()
                .Include(e => e.InsuranceProviderFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        false
                        || e.Coverage.Contains(input.Filter)
                        || e.InsuranceCode.Contains(input.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.InsuranceProviderNameFilter),
                    e =>
                        e.InsuranceProviderFk != null
                        && e.InsuranceProviderFk.Name == input.InsuranceProviderNameFilter
                );

            var pagedAndFilteredPatientInsurers = filteredPatientInsurers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var patientInsurers =
                from o in pagedAndFilteredPatientInsurers
                join o1 in _lookup_insuranceProviderRepository.GetAll()
                    on o.InsuranceProviderId equals o1.Id
                    into j1
                from s1 in j1.DefaultIfEmpty()
                select new
                {
                    Id = o.Id,
                    InsuranceProviderName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                };

            var totalCount = await filteredPatientInsurers.CountAsync();

            var dbList = await patientInsurers.ToListAsync();
            var results = new List<GetPatientInsurerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPatientInsurerForViewDto()
                {
                    PatientInsurer = new PatientInsurerDto { Id = o.Id, },
                    InsuranceProviderName = o.InsuranceProviderName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPatientInsurerForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientInsurers_Edit)]
        public async Task<GetPatientInsurerForEditOutput> GetPatientInsurerForEdit(
            EntityDto<long> input
        )
        {
            var patientInsurer = await _patientInsurerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPatientInsurerForEditOutput
            {
                PatientInsurer = ObjectMapper.Map<CreateOrEditPatientInsurerDto>(patientInsurer)
            };

            if (output.PatientInsurer.InsuranceProviderId != 0)
            {
                var _lookupInsuranceProvider =
                    await _lookup_insuranceProviderRepository.FirstOrDefaultAsync(
                        (long)output.PatientInsurer.InsuranceProviderId
                    );
                output.InsuranceProviderName = _lookupInsuranceProvider?.Name?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPatientInsurerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PatientInsurers_Create)]
        protected virtual async Task Create(CreateOrEditPatientInsurerDto input)
        {
            var patientInsurer = ObjectMapper.Map<PatientInsurer>(input);

            if (AbpSession.TenantId != null)
            {
                patientInsurer.TenantId = (int)AbpSession.TenantId;
            }

            await _patientInsurerRepository.InsertAsync(patientInsurer);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientInsurers_Edit)]
        protected virtual async Task Update(CreateOrEditPatientInsurerDto input)
        {
            var patientInsurer = await _patientInsurerRepository.FirstOrDefaultAsync(
                (long)input.Id
            );
            ObjectMapper.Map(input, patientInsurer);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientInsurers_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _patientInsurerRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_PatientInsurers)]
        public async Task<
            PagedResultDto<PatientInsurerInsuranceProviderLookupTableDto>
        > GetAllInsuranceProviderForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_insuranceProviderRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name != null && e.Name.Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var insuranceProviderList = await query.PageBy(input).ToListAsync();

            var lookupTableDtoList = new List<PatientInsurerInsuranceProviderLookupTableDto>();
            foreach (var insuranceProvider in insuranceProviderList)
            {
                lookupTableDtoList.Add(
                    new PatientInsurerInsuranceProviderLookupTableDto
                    {
                        Id = insuranceProvider.Id,
                        DisplayName = insuranceProvider.Name?.ToString()
                    }
                );
            }

            return new PagedResultDto<PatientInsurerInsuranceProviderLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}
