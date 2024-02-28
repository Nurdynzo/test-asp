using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_WardBeds)]
    public class WardBedsAppService : EHRAppServiceBase, IWardBedsAppService
    {
        private readonly IRepository<WardBed, long> _wardBedRepository;
        private readonly IRepository<BedType, long> _lookup_bedTypeRepository;
        private readonly IRepository<Ward, long> _lookup_wardRepository;
        private readonly ICreateWardBedsCommandHandler _createWardBedsCommandHandler;
        private readonly IUpdateWardBedsCommandHandler _updateWardBedsCommand;
        private readonly IGetWardBedCountQueryHandler _getWardBedCountQueryHandler;

        public WardBedsAppService(
            IRepository<WardBed, long> wardBedRepository,
            IRepository<BedType, long> lookup_bedTypeRepository,
            IRepository<Ward, long> lookup_wardRepository,
            ICreateWardBedsCommandHandler createWardBedsCommandHandler,
            IUpdateWardBedsCommandHandler updateWardBedsCommand,
            IGetWardBedCountQueryHandler getWardBedCountQueryHandler
        )
        {
            _wardBedRepository = wardBedRepository;
            _lookup_bedTypeRepository = lookup_bedTypeRepository;
            _lookup_wardRepository = lookup_wardRepository;
            _createWardBedsCommandHandler = createWardBedsCommandHandler;
            _updateWardBedsCommand = updateWardBedsCommand;
            _getWardBedCountQueryHandler = getWardBedCountQueryHandler;
        }

        public async Task<PagedResultDto<GetWardBedForViewDto>> GetAll(GetAllWardBedsInput input)
        {
            var filteredWardBeds = _wardBedRepository
                .GetAll()
                .Include(e => e.BedType)
                .Include(e => e.WardFk)              
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                .WhereIf(
                    input.IsActiveFilter.HasValue && input.IsActiveFilter > -1,
                    e =>
                        (input.IsActiveFilter == 1 && e.IsActive)
                        || (input.IsActiveFilter == 0 && !e.IsActive)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.BedTypeNameFilter),
                    e => e.BedType != null && e.BedType.Name == input.BedTypeNameFilter
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.WardNameFilter),
                    e => e.WardFk != null && e.WardFk.Name == input.WardNameFilter
                );


            var pagedAndFilteredWardBeds = filteredWardBeds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var totalCount = await filteredWardBeds.CountAsync();

            var results = await pagedAndFilteredWardBeds.Select(o => new GetWardBedForViewDto()
            {
                WardBed = new WardBedDto
                {
                    BedNumber = o.BedNumber,
                    IsActive = o.IsActive,
                    Id = o.Id,
                    BedTypeName = o.BedType.Name,
                    WardId = o.WardId,
                    BedTypeId = o.BedTypeId
                },
                BedTypeName = o.BedType.Name,
                WardName = o.WardFk.Name,
                Status = o.EncounterId.HasValue ? "Occupied" : "Available"
            }).ToListAsync();

        
            return new PagedResultDto<GetWardBedForViewDto>(totalCount, results);
        }

        [AbpAuthorize(AppPermissions.Pages_WardBeds_Edit)]
        public async Task<GetWardBedForEditOutput> GetWardBedForEdit(EntityDto<long> input)
        {
            var wardBed = await _wardBedRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWardBedForEditOutput
            {
                WardBed = ObjectMapper.Map<CreateOrEditWardBedDto>(wardBed)
            };

            if (output.WardBed.BedTypeId != null)
            {
                var _lookupBedType = await _lookup_bedTypeRepository.FirstOrDefaultAsync(
                    (long)output.WardBed.BedTypeId
                );
                output.BedTypeName = _lookupBedType?.Name;
            }

            if (output.WardBed.WardId != null)
            {
                var _lookupWard = await _lookup_wardRepository.FirstOrDefaultAsync(
                    (long)output.WardBed.WardId
                );
                output.WardName = _lookupWard?.Name;
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWardBedDto input)
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

        [AbpAuthorize(AppPermissions.Pages_WardBeds_Create)]
        protected virtual async Task Create(CreateOrEditWardBedDto input)
        {
            await _createWardBedsCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_WardBeds_Edit)]
        protected virtual async Task Update(CreateOrEditWardBedDto input)
        {
            await _updateWardBedsCommand.Handle(input);

        }


        [AbpAuthorize(AppPermissions.Pages_WardBeds_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _wardBedRepository.DeleteAsync(input.Id);
        }

        public async Task<List<GetWardBedCountResponse>> GetWardBedCount(GetWardBedCountRequest request)
        {
            return await _getWardBedCountQueryHandler.Handle(request);
        }
    }
}
