using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class CreateRoomAvailabilityCommandHandler : ICreateRoomAvailabilityCommandHandler
    {
        private readonly IRepository<Rooms, long> _roomsRepository;
        private readonly IRepository<RoomAvailability, long> _roomAvailabityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IObjectMapper _objectMapper;
        public CreateRoomAvailabilityCommandHandler(
            IRepository<Rooms, long> roomsRepository, 
            IRepository<RoomAvailability, long> roomAvailabityRepository,
            IUnitOfWorkManager unitOfWorkManager, 
            IObjectMapper objectMapper)
        {
            _roomsRepository = roomsRepository;
            _roomAvailabityRepository = roomAvailabityRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
        }
        public async Task<RoomAvailability> Handle(CreateOrEditRoomsAvailabilityDto request)
        {          
            var room = await _roomsRepository.FirstOrDefaultAsync(x => x.Id == request.RoomId);

            if (room == null)
            {
                throw new UserFriendlyException("Room cannot be found");
            }
            var roomAvailabilty = _objectMapper.Map<RoomAvailability>(request);
            roomAvailabilty.RoomsId = request.RoomId;

            await _roomAvailabityRepository.InsertAsync(roomAvailabilty);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return roomAvailabilty;
        }
    }
}
