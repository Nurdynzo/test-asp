using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Runtime.Session;

namespace Plateaumed.EHR.Facilities.Query
{
    public class GetOperatingRoomsQueryHandler : IGetOperatingRoomsQueryHandler
    {
        private readonly IBaseQuery _baseQuery;
        
        
        public GetOperatingRoomsQueryHandler(IBaseQuery baseQuery)
        {
            _baseQuery = baseQuery;
        }
        
        public async Task<PagedResultDto<GetFacilityOperatingRoomsForViewDto>> Handle(GetAllFacilityOperatingRoomsInput input, IAbpSession abpSession)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;
            var operatingRoomsQuery = _baseQuery
                .GetAllActiveOperatingRooms(abpSession.TenantId)
                .Include(room => room.FacilityFk)
                .WhereIf(input.FacilityIdFilter.HasValue, room => room.FacilityId == input.FacilityIdFilter);

            var totalCount = await operatingRoomsQuery.CountAsync();
            var pagedAndFilteredOperatingRooms = operatingRoomsQuery
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var results = await pagedAndFilteredOperatingRooms
                .Select(room => new GetFacilityOperatingRoomsForViewDto
                {
                    OperatingRoomResponseDto = new OperatingRoomResponseDto
                    {
                        operatingRoomDtos = new List<OperatingRoomDto>
                        {
                            new OperatingRoomDto
                            {
                                RoomId = room.Id,
                                RoomName = room.Name,
                                IsActive = room.IsActive,
                                Availabilities = room.Availabilities.Select(availability => new RoomAvailabilityDto
                                {
                                    DayOfWeek = availability.DayOfWeek.Value,
                                    StartTime = availability.StartTime,
                                    EndTime = availability.EndTime,
                                    Id = availability.Id,
                                }).ToList()
                            }
                        }
                    },
                    FacilityName = room.FacilityFk.Name
                })
                .ToListAsync();
            return new PagedResultDto<GetFacilityOperatingRoomsForViewDto>(totalCount, results);
        }
        
        public async Task<List<OperatingRoomDto>> GetAllOperatingRooms(IAbpSession abpSession)
        {
            var operatingRoomsQuery = await _baseQuery.GetAllActiveOperatingRooms(abpSession.TenantId)
                .Select(v => new OperatingRoomDto
                {
                    RoomId = v.Id,
                    RoomName = v.Name,
                    IsActive = v.IsActive
                }).ToListAsync();

            return operatingRoomsQuery;
        }
        
    }
}
