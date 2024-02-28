using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_BedTypes)]
    public class BedTypesAppService : EHRAppServiceBase, IBedTypesAppService
    {
        private readonly IRepository<BedType, long> _bedTypeRepository;

        public BedTypesAppService(IRepository<BedType, long> bedTypeRepository)
        {
            _bedTypeRepository = bedTypeRepository;
        }

        public async Task<PagedResultDto<BedTypeDto>> GetAll(GetAllBedTypesInput input)
        {
            var filteredBedTypes = _bedTypeRepository
                .GetAll()
                .Where(e => e.FacilityId == input.FacilityId)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Name.Contains(input.Filter)
                );

            var pagedAndFilteredBedTypes = filteredBedTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredBedTypes.CountAsync();

            var dbList = await pagedAndFilteredBedTypes.ToListAsync();

            var results = dbList.Select(bedType =>  new BedTypeDto { Name = bedType.Name, Id = bedType.Id}).ToList();

            return new PagedResultDto<BedTypeDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_BedTypes_Edit)]
        public async Task<GetBedTypeForEditOutput> GetBedTypeForEdit(EntityDto<long> input)
        {
            var bedType = await _bedTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBedTypeForEditOutput
            {
                BedType = ObjectMapper.Map<CreateOrEditBedTypeDto>(bedType)
            };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditBedTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_BedTypes_Create)]
        protected virtual async Task Create(CreateOrEditBedTypeDto input)
        {
            var bedType = ObjectMapper.Map<BedType>(input);

            if (AbpSession.TenantId != null)
            {
                bedType.TenantId = (int)AbpSession.TenantId;
            }

            await _bedTypeRepository.InsertAsync(bedType);
        }

        [AbpAuthorize(AppPermissions.Pages_BedTypes_Edit)]
        protected virtual async Task Update(CreateOrEditBedTypeDto input)
        {
            var bedType = await _bedTypeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, bedType);
        }

        [AbpAuthorize(AppPermissions.Pages_BedTypes_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _bedTypeRepository.DeleteAsync(input.Id);
        }
    }
}
