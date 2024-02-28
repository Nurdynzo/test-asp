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
    [AbpAuthorize(AppPermissions.Pages_Regions)]
    public class RegionsAppService : EHRAppServiceBase,IRegionsAppService
    {
        private readonly IRepository<Region, int> _regionRepository;

        public RegionsAppService(IRepository<Region, int> regionRepository)
        {
            _regionRepository = regionRepository;
        }
        
        public async Task<PagedResultDto<GetRegionForViewDto>> GetAll(GetAllRegionsInput input){
            var filteredRegions = _regionRepository
                .GetAllIncluding(r => r.Districts,r => r.CountryFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    c => c.Name.ToLower().Contains(input.Filter.ToLower())
                        || c.ShortName.ToLower().Contains(input.Filter.ToLower())
                ).WhereIf(
                    !string.IsNullOrWhiteSpace(input.CountryCodeFilter),
                    c => c.CountryFk.Code.ToLower() == input.CountryCodeFilter.ToLower());

            var pagedAndfilteredRegions = filteredRegions
                .OrderBy(input.Sorting ?? "name desc")
                .PageBy(input);

            var regions = await filteredRegions.ToListAsync();

            var totalCount = await filteredRegions.CountAsync();

            var results = ObjectMapper.Map<List<GetRegionForViewDto>>(regions);

            return new PagedResultDto<GetRegionForViewDto>(totalCount, results);
        }
        
        [AbpAllowAnonymous]   
        public async Task<ListResultDto<GetRegionsForListDto>> GetRegions(GetRegionsInput input){
            var regions = await _regionRepository
                .GetAllIncluding(r => r.CountryFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    c => c.Name.ToLower().Contains(input.Filter.ToLower())
                        || c.ShortName.ToLower().Contains(input.Filter.ToLower())
                ).WhereIf(
                    !string.IsNullOrWhiteSpace(input.CountryCodeFilter),
                    c => c.CountryFk.Code.ToLower() == input.CountryCodeFilter.ToLower())
                .Select(c => new GetRegionsForListDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ShortName = c.ShortName,
                    CountryCode = c.CountryFk.Code
                })
                .OrderBy(c => c.Name)
                .ToListAsync();


            return new ListResultDto<GetRegionsForListDto>(regions);
        }
        
        /// <summary>
        /// Get regions by passing countryId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetRegionByCountryIdOutput>> GetRegionsByCountryId(int input){
            var regions = await _regionRepository
                .GetAllIncluding(r => r.CountryFk)
                .Where(region => region.CountryId == input)
                .Select(c => new GetRegionByCountryIdOutput
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .OrderBy(c => c.Name)
                .ToListAsync();


            return regions;
        }
        
        
        public async Task CreateOrEdit(CreateOrEditRegionDto input){
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
            return;
        }
        

        [AbpAuthorize(AppPermissions.Pages_Regions_Edit)]
        public async Task<GetRegionForEditOutput> GetRegionForEdit(EntityDto<int> input){
            var region = await _regionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRegionForEditOutput
            {
                Region = ObjectMapper.Map<CreateOrEditRegionDto>(region)
            };

            return output;
        }
        

        [AbpAuthorize(AppPermissions.Pages_Regions_Create)]
        protected virtual async Task Create(CreateOrEditRegionDto input){
            var region = ObjectMapper.Map<Region>(input);

            await _regionRepository.InsertAsync(region);

            await CurrentUnitOfWork.SaveChangesAsync();
        }


        [AbpAuthorize(AppPermissions.Pages_Regions_Edit)]
        protected virtual async Task Update(CreateOrEditRegionDto input){
            var region = await _regionRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, region);

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        

        [AbpAuthorize(AppPermissions.Pages_Regions_Delete)]
        public async Task Delete(EntityDto<int> input){
            await _regionRepository.DeleteAsync(input.Id);
        }
    }
}