using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities
{
    public interface IRoomAppService : IApplicationService
    {
        Task CreateOrEdit(CreateOrEditRoomsDto input);
        Task CreateOrEditRoomsAvailability(List<CreateOrEditRoomsAvailabilityDto> request);
        Task Delete(EntityDto<long> input);
        Task DeleteRoomAvailability(EntityDto<long> input);
        Task<PagedResultDto<GetFacilityConsultingRoomsForViewDto>> GetConsultingRooms(GetAllFacilityConsultingRoomsInput input);
        Task<PagedResultDto<GetFacilityOperatingRoomsForViewDto>> GetOperatingRooms(GetAllFacilityOperatingRoomsInput input);
        Task ActivateOrDeactivateRoom(ActivateOrDeactivateRoom request);
    }
}
