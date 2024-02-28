using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Misc.Dtos;
using Plateaumed.EHR.Dto;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Plateaumed.EHR.Storage;

namespace Plateaumed.EHR.Misc.Country
{
    [AbpAuthorize(AppPermissions.Pages_Districts)]
    public class DistrictAppService: EHRAppServiceBase, IDistrictAppService
    {
        private readonly IRepository<District, int> _districtRepository;

        public DistrictAppService(IRepository<District, int> districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public async Task<PagedResultDto<GetDistrictForViewDto>> GetAll(GetAllDistrictsInput input)
        {
            
                var filteredDistricts = _districtRepository
                .GetAllIncluding(r => r.RegionFk)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    c => c.Name.ToLower().Contains(input.Filter.ToLower())
                ).WhereIf(
                    input.RegionIdFilter > 0,
                    c => c.RegionFk.Id == input.RegionIdFilter
                );

            var pagedAndFilteredDistricts = filteredDistricts
                .OrderBy(input.Sorting ?? "name desc")
                .PageBy(input);

            var districts = await filteredDistricts.ToListAsync();

            var totalCount = await filteredDistricts.CountAsync();

            var results = ObjectMapper.Map<List<GetDistrictForViewDto>>(districts);

            return new PagedResultDto<GetDistrictForViewDto>(totalCount, results);
        }
        

        [AbpAuthorize(AppPermissions.Pages_Districts_Edit)]
        public async Task<GetDistrictForEditOutput> GetDistrictForEdit(EntityDto<int> input)
        {
            var district = await _districtRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDistrictForEditOutput
            {
                District = ObjectMapper.Map<CreateOrEditDistrictDto>(district)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDistrictDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Districts_Create)]
        protected virtual async Task Create(CreateOrEditDistrictDto input)
        {
            var district = ObjectMapper.Map<District>(input);

            await _districtRepository.InsertAsync(district);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Districts_Edit)]
        protected virtual async Task Update(CreateOrEditDistrictDto input)
        {
            var district = await _districtRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, district);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Countries_Delete)]
        public async Task Delete(EntityDto<int> input)
        {
            await _districtRepository.DeleteAsync(input.Id);
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetDistrictsForListDto>> GetDistricts(GetDistrictInput input)
        {
            var districts = await _districtRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    c => c.Name.ToLower().Contains(input.Filter.ToLower())
                )
                .Select(c => new GetDistrictsForListDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();


            return new ListResultDto<GetDistrictsForListDto>(districts);
        }

        
    }
}