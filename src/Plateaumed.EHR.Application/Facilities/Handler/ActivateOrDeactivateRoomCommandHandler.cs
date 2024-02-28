using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class ActivateOrDeactivateRoomCommandHandler : IActivateOrDeactivateRoomCommandHandler
    {
        private readonly IRepository<Rooms, long> _roomsRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ActivateOrDeactivateRoomCommandHandler(
            IRepository<Rooms, long> roomsRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
           _roomsRepository = roomsRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public virtual async Task<Rooms> Handle(ActivateOrDeactivateRoom request)
        {
            var room = await _roomsRepository.FirstOrDefaultAsync((long)request.Id);
            if (room == null)
            {
                throw new UserFriendlyException("room cannot be found");
            }
            room.IsActive = request.IsActive;
            await _roomsRepository.UpdateAsync(room);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return room;
        }
    }
}
