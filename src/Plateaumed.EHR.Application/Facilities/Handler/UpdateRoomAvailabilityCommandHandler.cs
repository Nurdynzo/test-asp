using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class UpdateRoomAvailabilityCommandHandler : IUpdateRoomAvailabilityCommandHandler
    {
        private readonly IRepository<Rooms, long> _roomsRepository;
        private readonly IRepository<RoomAvailability, long> _availabityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UpdateRoomAvailabilityCommandHandler(
            IRepository<Rooms, long> roomsRepository,
            IRepository<RoomAvailability, long> availabityRepository,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _roomsRepository = roomsRepository;
            _availabityRepository = availabityRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<RoomAvailability> Handle(CreateOrEditRoomsAvailabilityDto request)
        {
            var room = await _roomsRepository.FirstOrDefaultAsync(x => x.Id == request.RoomId);
            if (room == null)
            {
                throw new UserFriendlyException("Room cannot be found");
            }
            var updateRoomAvailibility = await _availabityRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (room == null)
            {
                throw new UserFriendlyException("Room Availability cannot be found");
            }
            updateRoomAvailibility.DayOfWeek = request.DayOfWeek;
            updateRoomAvailibility.StartTime = request.StartTime;
            updateRoomAvailibility.EndTime = request.EndTime;

            await _availabityRepository.UpdateAsync(updateRoomAvailibility);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            return updateRoomAvailibility;
        }
    }
}
