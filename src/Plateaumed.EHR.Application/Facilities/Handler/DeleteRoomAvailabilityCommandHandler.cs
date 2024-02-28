using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class DeleteRoomAvailabilityCommandHandler : IDeleteRoomAvailabilityCommandHandler
    {
        private readonly IRepository<RoomAvailability, long> _availabilityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DeleteRoomAvailabilityCommandHandler(
            IRepository<RoomAvailability, long> availabilityRepository,
            IUnitOfWorkManager unitOfWorkManager) 
        {
            _availabilityRepository = availabilityRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task Handle(long roomAvailabilityId)
        {
            var roomAvailability = await _availabilityRepository.FirstOrDefaultAsync(x => x.Id == roomAvailabilityId);

            if (roomAvailability == null)
            {
                throw new UserFriendlyException("Room does not exists");
            }

            await _availabilityRepository.DeleteAsync(roomAvailability);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

    }
}
