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
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Abp.Runtime.Session;
using Abp.ObjectMapping;
using Plateaumed.EHR.Facilities.Handler;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_Wards)]
    public class WardsAppService : EHRAppServiceBase, IWardsAppService
    {
        private readonly IRepository<Ward, long> _wardRepository;
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IRepository<BedType, long> _bedTypeRepository;

        private readonly IActivateOrDeactivateWardCommandHandler _activateWard;
        private readonly ICreateWardsCommandHandler _createWardsCommandHandler;
        private readonly IUpdateWardsCommandHandler _updateWardsCommand;

        public WardsAppService(
            IRepository<Ward, long> wardRepository,
            IRepository<Facility, long> facilityRepository,
            IRepository<BedType, long> bedTypeRepository,
            IActivateOrDeactivateWardCommandHandler activateWard,
            IObjectMapper objectMapper,
            IAbpSession abpSession,
            ICreateWardsCommandHandler createWardsCommandHandler,
            IUpdateWardsCommandHandler updateWardsCommand)
        {
            _wardRepository = wardRepository;
            _facilityRepository = facilityRepository;
            _bedTypeRepository = bedTypeRepository;
            _activateWard = activateWard;
            _createWardsCommandHandler = createWardsCommandHandler;
            _updateWardsCommand = updateWardsCommand;
        }

        public async Task<List<WardDto>> GetAllWards()
        {
            var wards = await _wardRepository.GetAllListAsync();
            return ObjectMapper.Map<List<WardDto>>(wards);
        }

        public async Task<PagedResultDto<GetWardForViewDto>> GetAll(GetAllWardsInput input)
        {
            var wards = _wardRepository
                .GetAll()
                .Include(e => e.FacilityFk)
                .Include(w => w.WardBeds)
                .ThenInclude(w => w.BedType)
                .WhereIf(input.FacilityIds?.Count > 0,
                    w => input.FacilityIds.Contains(w.FacilityId))
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e =>
                        e.Name.Contains(input.Filter)
                        || e.Description.Contains(input.Filter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.NameFilter),
                    e => e.Name.Contains(input.NameFilter)
                )
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.FacilityNameFilter),
                    e => e.FacilityFk != null && e.FacilityFk.Name == input.FacilityNameFilter
                )
                .OrderBy(input.Sorting ?? "id asc");

            var pagedWards = await wards.PageBy(input).ToListAsync();

            var results = pagedWards.Select(o => new GetWardForViewDto
            {
                Ward = ObjectMapper.Map<WardDto>(o),
                FacilityName = o.FacilityFk.Name,
                FacilityId = o.FacilityId
            })
                .ToList();

            return new PagedResultDto<GetWardForViewDto>(wards.Count(), results);
        }

        [AbpAuthorize(AppPermissions.Pages_Wards_Edit)]
        public async Task<GetWardForEditOutput> GetWardForEdit(EntityDto<long> input)
        {
            var ward = await _wardRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWardForEditOutput
            {
                Ward = ObjectMapper.Map<CreateOrEditWardDto>(ward)
            };

            var lookupFacility = await _facilityRepository.FirstOrDefaultAsync(output.Ward.FacilityId);
            output.FacilityName = lookupFacility?.Name;

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWardDto input)
        {
            var facility = await _facilityRepository.FirstOrDefaultAsync(input.FacilityId);
            if (facility == null)
            {
                throw new UserFriendlyException("Facility does not exist");
            }

            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Wards_Create)]
        protected virtual async Task Create(CreateOrEditWardDto input)
        {
            await _createWardsCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Wards_Edit)]
        public virtual async Task ActivateOrDeactivateWard(ActivateOrDeactivateWardRequest input)
        {
            await _activateWard.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Wards_Edit)]
        protected virtual async Task Update(CreateOrEditWardDto input)
        {
            await _updateWardsCommand.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Wards_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _wardRepository.DeleteAsync(input.Id);
        }
        
        
    }
}