using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class UpdateRoomsCommandHandler : IUpdateRoomsCommandHandler
    {
        private readonly IRepository<Rooms, long> _roomsRepository;
        private readonly IRepository<RoomAvailability, long> _roomAvailabilityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUpdateRoomAvailabilityCommandHandler _updateRoomAvailabilityCommandHandler;
        private readonly ICreateRoomAvailabilityCommandHandler _createRoomAvailabilityCommandHandler;

        public UpdateRoomsCommandHandler(IRepository<Rooms, long> roomsRepository, 
            IRepository<RoomAvailability, long> roomAvailabilityRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IUpdateRoomAvailabilityCommandHandler updateRoomAvailabilityCommandHandler,
            ICreateRoomAvailabilityCommandHandler createRoomAvailabilityCommandHandler) 
        {
            _roomsRepository = roomsRepository;
            _roomAvailabilityRepository = roomAvailabilityRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _updateRoomAvailabilityCommandHandler = updateRoomAvailabilityCommandHandler;
            _createRoomAvailabilityCommandHandler = createRoomAvailabilityCommandHandler;
    }

        public async Task<Rooms> Handle(CreateOrEditRoomsDto updateRoomsDto)
        {
            var updateRoom = await _roomsRepository.FirstOrDefaultAsync(updateRoomsDto.Id.GetValueOrDefault());

            if (updateRoom == null)
            {
                throw new UserFriendlyException("Room not found");
            }

            if (updateRoomsDto.Type != RoomType.OperatingRoom && updateRoomsDto.Type != RoomType.ConsultingRoom)
            {
                throw new UserFriendlyException("Invalid Room Type");
            }

            updateRoom.Name = updateRoomsDto.Name;
            updateRoom.IsActive = updateRoomsDto.IsActive;
            updateRoom.MinTimeInterval = updateRoomsDto.MinTimeInterval;

            var savedRoomAvailability = new List<RoomAvailability>();

            if (updateRoomsDto.Type == RoomType.OperatingRoom && updateRoomsDto.RoomAvailability != null)
            {
                foreach (var availabilityDto in updateRoomsDto.RoomAvailability)
                {
                    var existingRoomAvailability = _roomAvailabilityRepository.FirstOrDefault(x => x.Id == availabilityDto.Id);

                    if (existingRoomAvailability != null)
                    {
                        var roomAvailabilityDto = GetRoomAvailabilityPayload(availabilityDto, updateRoom.Id);
                        roomAvailabilityDto.Id = existingRoomAvailability.Id;
                        var roomAvailability = await _updateRoomAvailabilityCommandHandler.Handle(roomAvailabilityDto);
                        savedRoomAvailability.Add(roomAvailability);
                    }
                    else if (!availabilityDto.Id.HasValue)
                    {
                        var roomAvailabilityDto = GetRoomAvailabilityPayload(availabilityDto, updateRoom.Id);
                        var roomAvailability = await _createRoomAvailabilityCommandHandler.Handle(roomAvailabilityDto);
                        savedRoomAvailability.Add(roomAvailability);
                    }
                    else
                    {
                        throw new UserFriendlyException("Room Availability not found");
                    }
                }
            }

            await _roomsRepository.UpdateAsync(updateRoom);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            updateRoom.Availabilities = savedRoomAvailability;

            return updateRoom;
        }

        private static CreateOrEditRoomsAvailabilityDto GetRoomAvailabilityPayload(RoomAvailabilityDto availabilityDto, long roomId)
        {
            var roomAvailability = new CreateOrEditRoomsAvailabilityDto
            {
                RoomId = roomId,
                DayOfWeek = availabilityDto.DayOfWeek,
                StartTime = availabilityDto.StartTime,
                EndTime = availabilityDto.EndTime
            };
            return roomAvailability;
        }
    }
}
