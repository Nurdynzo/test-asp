using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_FacilityTypes)]
    public class FacilityTypesAppService : EHRAppServiceBase, IFacilityTypesAppService
    {
        private readonly IRepository<FacilityType, long> _facilityTypeRepository;

        public FacilityTypesAppService(IRepository<FacilityType, long> facilityTypeRepository)
        {
            _facilityTypeRepository = facilityTypeRepository;
        }

        public async Task<PagedResultDto<GetFacilityTypeForViewDto>> GetAll(
            GetAllFacilityTypesInput input
        )
        {
            var filteredFacilityTypes = _facilityTypeRepository
                .GetAll()
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.Name.Contains(input.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.Contains(input.NameFilter)
                );

            var pagedAndFilteredFacilityTypes = filteredFacilityTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredFacilityTypes.CountAsync();

            var facilityTypes = await pagedAndFilteredFacilityTypes.ToListAsync();

            var results = facilityTypes.Select(facilityType => new GetFacilityTypeForViewDto
            {
                FacilityType = ObjectMapper.Map<FacilityTypeDto>(facilityType)
            }).ToList();

            return new PagedResultDto<GetFacilityTypeForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityTypes_Edit)]
        public async Task<GetFacilityTypeForEditOutput> GetFacilityTypeForEdit(
            EntityDto<long> input
        )
        {
            var facilityType = await _facilityTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetFacilityTypeForEditOutput
            {
                FacilityType = ObjectMapper.Map<CreateOrEditFacilityTypeDto>(facilityType)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditFacilityTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_FacilityTypes_Create)]
        protected virtual async Task Create(CreateOrEditFacilityTypeDto input)
        {
            var facilityType = ObjectMapper.Map<FacilityType>(input);

            await _facilityTypeRepository.InsertAsync(facilityType);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityTypes_Edit)]
        protected virtual async Task Update(CreateOrEditFacilityTypeDto input)
        {
            var facilityType = await _facilityTypeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, facilityType);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityTypes_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _facilityTypeRepository.DeleteAsync(input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
