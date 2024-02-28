using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_Rooms)]
    public class RoomAppService : EHRAppServiceBase, IRoomAppService
    {
        private readonly IRepository<Rooms, long> _roomsRepository;
        private readonly IRepository<RoomAvailability, long> _roomAvailabilityRepository;
        private readonly ICreateRoomsCommandHandler _createRoomsCommandHandler;
        private readonly IGetConsultingRoomsQueryHandler _getConsultingRoomsQueryHandler;
        private readonly IGetOperatingRoomsQueryHandler _getOperatingRoomsQueryHandler;
        private readonly IUpdateRoomsCommandHandler _updateRoomsCommandHandler;
        private readonly IActivateOrDeactivateRoomCommandHandler _activateOrDeactivateRoom;
        private readonly ICreateRoomAvailabilityCommandHandler _createRoomAvailabilityCommandHandler;
        private readonly IUpdateRoomAvailabilityCommandHandler _updateRoomsAvailabilityCommandHandler;
        private readonly IDeleteRoomAvailabilityCommandHandler _deleteRoomAvailabilityCommandHandler;

        public RoomAppService(ICreateRoomsCommandHandler roomsCommandHandler, 
            IGetConsultingRoomsQueryHandler getConsultingRoomsQueryHandler,
            IGetOperatingRoomsQueryHandler getOperatingRoomsQueryHandler,
            IUpdateRoomsCommandHandler updateRoomsCommandHandler,
            IActivateOrDeactivateRoomCommandHandler activateOrDeactivateRoom,
            IRepository<Rooms, long> roomsRepository,
            IRepository<RoomAvailability, long> roomAvailabilityRepository,
            ICreateRoomAvailabilityCommandHandler createRoomAvailabilityCommand,
            IUpdateRoomAvailabilityCommandHandler updateRoomsAvailabilityCommandHandler,
            IDeleteRoomAvailabilityCommandHandler deleteRoomAvailabilityCommandHandler

            )
        {
            _createRoomsCommandHandler = roomsCommandHandler;
            _getConsultingRoomsQueryHandler = getConsultingRoomsQueryHandler;
            _getOperatingRoomsQueryHandler = getOperatingRoomsQueryHandler;
            _updateRoomsCommandHandler = updateRoomsCommandHandler;
            _activateOrDeactivateRoom = activateOrDeactivateRoom;
            _roomsRepository = roomsRepository;
            _roomAvailabilityRepository = roomAvailabilityRepository;
            _createRoomAvailabilityCommandHandler = createRoomAvailabilityCommand;
            _updateRoomsAvailabilityCommandHandler = updateRoomsAvailabilityCommandHandler;
            _deleteRoomAvailabilityCommandHandler = deleteRoomAvailabilityCommandHandler;
        }

        public async Task CreateOrEdit(CreateOrEditRoomsDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Rooms_Create)]
        protected async Task Create(CreateOrEditRoomsDto input)
        {
            await _createRoomsCommandHandler.Handle(input, AbpSession.TenantId, AbpSession.UserId);
        }

        [AbpAuthorize(AppPermissions.Pages_Rooms_Edit)]
        protected virtual async Task Update(CreateOrEditRoomsDto input)
        {
            await _updateRoomsCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Rooms_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _roomsRepository.DeleteAsync(input.Id);
        }

       [AbpAuthorize(AppPermissions.Pages_Rooms_Delete)]
        public async Task DeleteRoomAvailability(EntityDto<long> input)
        {
            await _deleteRoomAvailabilityCommandHandler.Handle(input.Id);
        }

        public async Task CreateOrEditRoomsAvailability(List<CreateOrEditRoomsAvailabilityDto> request)
        {
            foreach (CreateOrEditRoomsAvailabilityDto input in request)
            {
                if (!input.Id.HasValue)
                {
                    await CreateRoomAvailability(input);
                }
                else
                {
                    await UpdateRoomAvailability(input);
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Rooms_Create)]
        protected async Task CreateRoomAvailability(CreateOrEditRoomsAvailabilityDto input)
        {
            await _createRoomAvailabilityCommandHandler.Handle(input);
        }

        [AbpAuthorize(AppPermissions.Pages_Rooms_Edit)]
        protected async Task UpdateRoomAvailability(CreateOrEditRoomsAvailabilityDto input)
        {
            await _updateRoomsAvailabilityCommandHandler.Handle(input);
        }

        public async Task<PagedResultDto<GetFacilityConsultingRoomsForViewDto>> GetConsultingRooms(GetAllFacilityConsultingRoomsInput input)
        {
          return  await _getConsultingRoomsQueryHandler.Handle(input);
        }

        public async Task<PagedResultDto<GetFacilityOperatingRoomsForViewDto>> GetOperatingRooms(GetAllFacilityOperatingRoomsInput input)
        {
            return await _getOperatingRoomsQueryHandler.Handle(input, AbpSession);
        }

        [AbpAuthorize(AppPermissions.Pages_Rooms_Edit)]
        public virtual async Task ActivateOrDeactivateRoom(ActivateOrDeactivateRoom request)
        {
            await _activateOrDeactivateRoom.Handle(request);
        }

        public async Task<GetRoomForEditOutputDto> GetRoomForEditOutput(EntityDto<long> input)
        {
            var room = await _roomsRepository.FirstOrDefaultAsync(input.Id);
            if (room == null)
            {
                throw new UserFriendlyException("Room not found");
            }

            var availabilities = await _roomAvailabilityRepository
                .GetAll()
                .Where(avail => avail.RoomsId == input.Id)
                .ToListAsync();

            var roomDto = ObjectMapper.Map<CreateOrEditRoomsDto>(room);
            roomDto.RoomAvailability = ObjectMapper.Map<List<RoomAvailabilityDto>>(availabilities);

            var output = new GetRoomForEditOutputDto
            {
                RoomOutput = ObjectMapper.Map<CreateOrEditRoomsDto>(roomDto),
            };

            return output;
        }
    }
}
