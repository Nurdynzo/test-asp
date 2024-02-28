using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class UpdateWardBedsCommandHandler : IUpdateWardBedsCommandHandler
    {
        private readonly IRepository<WardBed, long> _wardBedRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;

        public UpdateWardBedsCommandHandler(
            IRepository<WardBed, long> wardBedRepository,
            IObjectMapper objectMapper,
            IAbpSession abpSession)
        {
            _wardBedRepository = wardBedRepository;
            _objectMapper = objectMapper;
            _abpSession = abpSession;
        }
        public async Task Handle(CreateOrEditWardBedDto request)
        {
            var wardbed = await _wardBedRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (wardbed == null)
                throw new UserFriendlyException("Ward bed does not exist");
            

            var wardBeds = await GetWardBedsByTypeId(wardbed.BedTypeId);

            if (wardBeds.Count > request.Count)
                await DeleteExcessWardBeds(wardBeds, request.Count);
            
            else if (wardBeds.Count < request.Count)
                await InsertAdditionalWardBeds(wardBeds, request);

            else
            {
                UpdateWard(wardbed, request);
            }
        }


        private async Task<List<WardBed>> GetWardBedsByTypeId(long? bedTypeId)
        {
            return await _wardBedRepository.GetAll()
                .Where(wb => wb.BedTypeId == bedTypeId)
                .OrderBy(wb => wb.BedNumber)
                .ToListAsync();
        }

        private async Task DeleteExcessWardBeds(List<WardBed> wardBeds, int requestedCount)
        {
            var wardBedsToDelete = wardBeds.Skip(requestedCount).ToList();
            foreach (var wardBedToDelete in wardBedsToDelete)
            {
                await _wardBedRepository.DeleteAsync(wardBedToDelete);
            }
        }

        private async Task InsertAdditionalWardBeds(List<WardBed> wardBeds, CreateOrEditWardBedDto request)
        {
            var lastBedNumber = wardBeds.Any() ? wardBeds.Max(wb => wb.BedNumber) : "0";
            for (int i = wardBeds.Count + 1; i <= request.Count; i++)
            {
                var wardBedRequest = MapWardBed(request);
                var wardBed = _objectMapper.Map<WardBed>(wardBedRequest);
                wardBed.BedNumber = $"{request.BedTypeName} {i}";

                if (_abpSession.TenantId != null)
                {
                    wardBed.TenantId = (int)_abpSession.TenantId;
                }

                await _wardBedRepository.InsertAsync(wardBed);
            }
        }

        private async void UpdateWard(WardBed wardBed, CreateOrEditWardBedDto request)
        {
            wardBed.WardId = (long)request.WardId;
            wardBed.IsActive = request.IsActive;
            await _wardBedRepository.UpdateAsync(wardBed);
        }

        private static CreateOrEditWardBedDto MapWardBed(CreateOrEditWardBedDto input)
        {
            var request = new CreateOrEditWardBedDto
            {
                 Id = null,
                 BedTypeId = input.BedTypeId,
                 BedTypeName = input.BedTypeName,
                 Count = input.Count,
                 IsActive = input.IsActive,
                 WardId = input.WardId,
                   
            };

            return request;
        }
    }
}
