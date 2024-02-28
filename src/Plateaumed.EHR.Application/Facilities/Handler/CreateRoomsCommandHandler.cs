using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNetCore.Server.IIS.Core;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class CreateRoomsCommandHandler : ICreateRoomsCommandHandler
    {
        private readonly IRepository<Rooms, long> _roomsRepository;
        private readonly IRepository<RoomAvailability, long> _roomavailabityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IObjectMapper _objectMapper;

        public CreateRoomsCommandHandler(IRepository<Rooms, long> roomsRepository, IRepository<RoomAvailability, long> roomavailabityRepository,
            IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
        {
            _roomsRepository = roomsRepository;
            _roomavailabityRepository = roomavailabityRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
        }
        public async Task<Rooms> Handle(CreateOrEditRoomsDto createRoomsDto, int? tenantId, long? userId)
        {
            if (createRoomsDto.Type != RoomType.OperatingRoom
                && createRoomsDto.Type != RoomType.ConsultingRoom)
            {
                throw new UserFriendlyException("Invalid Room Type");
            }

            var room = _objectMapper.Map<Rooms>(createRoomsDto);
            room.TenantId = tenantId;
            room.IsActive = true;
            room.CreatorUserId = userId;
            room.CreationTime = DateTime.UtcNow;


            var roomType = (RoomType)Enum.Parse(typeof(RoomType), createRoomsDto.Type.ToString(), true);
            var roomId = await _roomsRepository.InsertAndGetIdAsync(room);

            if (roomType == RoomType.OperatingRoom)
            {
                room.MinTimeInterval = createRoomsDto.MinTimeInterval;

                if (createRoomsDto.RoomAvailability != null)
                {
                    foreach (var availability in createRoomsDto.RoomAvailability)
                    {
                        var roomAvailability = new RoomAvailability
                        {
                            RoomsId = roomId,
                            DayOfWeek = availability.DayOfWeek,
                            StartTime = availability.StartTime,
                            EndTime = availability.EndTime
                        };
                        await _roomavailabityRepository.InsertAsync(roomAvailability);

                    }

                }
            }
            await _unitOfWorkManager.Current.SaveChangesAsync();
            return room;
        }
    }
}
