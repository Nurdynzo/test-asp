using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Misc.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Misc.Country
{
    [AbpAuthorize(AppPermissions.Pages_Countries)]
    public class CountriesAppService : EHRAppServiceBase, ICountriesAppService
    {
        private readonly IRepository<Country, int> _countryRepository;

        public CountriesAppService(IRepository<Country, int> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<PagedResultDto<GetCountryForViewDto>> GetAll(GetAllCountriesInput input)
        {
            
                var filteredCountries = _countryRepository
                .GetAllIncluding(r => r.Regions)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    c => c.Name.ToLower().Contains(input.Filter.ToLower())
                        || c.Nationality.ToLower().Contains(input.Filter.ToLower())
                        || c.Code.ToLower().Contains(input.Filter.ToLower())
                        || c.PhoneCode.ToLower().Contains(input.Filter.ToLower())
                );

            var pagedAndFilteredCountries = filteredCountries
                .OrderBy(input.Sorting ?? "name desc")
                .PageBy(input);

            var countries = await filteredCountries.ToListAsync();

            var totalCount = await filteredCountries.CountAsync();

            var results = ObjectMapper.Map<List<GetCountryForViewDto>>(countries);

            return new PagedResultDto<GetCountryForViewDto>(totalCount, results);
        }
        

        [AbpAuthorize(AppPermissions.Pages_Countries_Edit)]
        public async Task<GetCountryForEditOutput> GetCountryForEdit(EntityDto<int> input)
        {
            var country = await _countryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCountryForEditOutput
            {
                Country = ObjectMapper.Map<CreateOrEditCountryDto>(country)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCountryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Countries_Create)]
        protected virtual async Task Create(CreateOrEditCountryDto input)
        {
            var country = ObjectMapper.Map<Country>(input);

            await _countryRepository.InsertAsync(country);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Countries_Edit)]
        protected virtual async Task Update(CreateOrEditCountryDto input)
        {
            var country = await _countryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, country);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Countries_Delete)]
        public async Task Delete(EntityDto<int> input)
        {
            await _countryRepository.DeleteAsync(input.Id);
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetCountriesForListDto>> GetCountries(GetCountriesInput input)
        {
            var countries = await _countryRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    c => c.Name.ToLower().Contains(input.Filter.ToLower())
                        || c.Code.ToLower().Contains(input.Filter.ToLower())
                )
                .Select(c => new GetCountriesForListDto
                {
                    Id = c.Id,
                    CountryCode = c.Code,
                    CountryName = c.Name
                })
                .OrderBy(c => c.CountryName)
                .ToListAsync();


            return new ListResultDto<GetCountriesForListDto>(countries);
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetCountryPhoneCodesForListDto>> GetCountryPhoneCodes(GetCountryPhoneCodesInput input)
        {
            var countryPhoneCodes = await _countryRepository
               .GetAll()
               .WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                   c => c.PhoneCode.Contains(input.Filter.ToLower())
                   || c.Name.ToLower().Contains(input.Filter.ToLower())
                       || c.Code.ToLower().Contains(input.Filter.ToLower())
               )
               .Select(c => new GetCountryPhoneCodesForListDto
               {
                   CountryCode = c.Code,
                   PhoneCode = c.PhoneCode
               })
               .OrderBy(c => c.CountryCode)
               .ToListAsync();


            return new ListResultDto<GetCountryPhoneCodesForListDto>(countryPhoneCodes);
        }


        /// <summary>
        /// Get all nationlities or get nationalities based on filter input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<List<GetNationalitiesOutput>> GetNationalities(GetNationalitiesInput input)
        {
            var nationalities = await _countryRepository
               .GetAll()
               .WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                   c => c.Name.ToLower().Contains(input.Filter.ToLower())
                       || c.Code.ToLower().Contains(input.Filter.ToLower())
               )
               .Select(c => new GetNationalitiesOutput
               {
                   Id = c.Id,
                   Name = c.Nationality
               })
               .OrderBy(c => c.Name)
               .ToListAsync();

            return nationalities;
        }
    }
}
