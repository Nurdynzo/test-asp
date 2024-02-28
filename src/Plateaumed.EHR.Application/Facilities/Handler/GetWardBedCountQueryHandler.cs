using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Dtos;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class GetWardBedCountQueryHandler : IGetWardBedCountQueryHandler
    {
        private readonly IRepository<BedType, long> _bedTypeRepository;
        private readonly IRepository<WardBed, long> _wardBedRepository;

        public GetWardBedCountQueryHandler(IRepository<BedType, long> bedTypeRepository, IRepository<WardBed, long> wardBedRepository)
        {
            _bedTypeRepository = bedTypeRepository;
            _wardBedRepository = wardBedRepository;
        }

        public async Task<List<GetWardBedCountResponse>> Handle(GetWardBedCountRequest request)
        {
            var bedTypes = _bedTypeRepository.GetAll()
                .WhereIf(request.FacilityId.HasValue, x => x.FacilityId == request.FacilityId);

            var filteredWardBeds = _wardBedRepository.GetAll()
                .WhereIf(request.WardId.HasValue, x => x.WardId == request.WardId);

            var grouping = await (from bedType in bedTypes
                join wardBed in filteredWardBeds 
                    on bedType.Id equals wardBed.BedTypeId into wardBeds
                from wardBed in wardBeds.DefaultIfEmpty()
                group wardBed by new { bedType.Id, bedType.Name }).ToListAsync();

            return grouping.Select(x => new GetWardBedCountResponse
            {
                BedTypeId = x.Key.Id,
                BedTypeName = x.Key.Name,

                Count = x.Count(wb => wb != null),
                WardBeds = x.Where(wb => wb != null).Select(wb => new WardBedDto
                {
                    Id = wb.Id,
                    BedNumber = wb.BedNumber,
                    WardId = wb.WardId,
                    BedTypeId = wb.BedTypeId,
                    BedTypeName = x.Key.Name
                }).ToList()
            }).ToList();
        }
    }
}
