using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Linq;

namespace Plateaumed.EHR.Facilities.Query
{
    public class GetConsultingRoomsQueryHandler : IGetConsultingRoomsQueryHandler
    {
        private readonly IBaseQuery _baseQuery;

        private readonly IObjectMapper _objectMapper;

        public GetConsultingRoomsQueryHandler(IBaseQuery baseQuery,
            IObjectMapper objectMapper)
        {
            _baseQuery = baseQuery;
            _objectMapper = objectMapper;
        }
        public async Task<PagedResultDto<GetFacilityConsultingRoomsForViewDto>> Handle(GetAllFacilityConsultingRoomsInput input)
        {
            var filterTerms = !string.IsNullOrWhiteSpace(input.Filter) ? input.Filter.ToLower().Split(" ") : null;
            var consultingRoomsQuery = _baseQuery
               .GetAllActiveConsultingRooms()
               .Include(room => room.FacilityFk)
               .WhereIf(input.FacilityIdFilter.HasValue, room => room.FacilityId == input.FacilityIdFilter);

            var totalCount = await consultingRoomsQuery.CountAsync();
            var pagedAndFiltereConsultingRooms =  consultingRoomsQuery
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var results = await pagedAndFiltereConsultingRooms
                 .Select(room => new GetFacilityConsultingRoomsForViewDto
                 {
                     consultingRoomResponseDto = new ConsultingRoomResponseDto
                     {
                         ConsultingRooms = new List<ConsultingRoomDto>
                         {
                            new ConsultingRoomDto
                            {
                                RoomId = room.Id,
                                RoomName = room.Name,
                                IsActive = room.IsActive,
                            }
                         }
                     },
                     FacilityName = room.FacilityFk.Name
                 })
                 .ToListAsync();
            return new PagedResultDto<GetFacilityConsultingRoomsForViewDto>(totalCount, results);
        }
    }
}
