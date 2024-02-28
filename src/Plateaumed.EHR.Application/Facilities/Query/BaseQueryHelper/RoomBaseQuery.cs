using Abp.Domain.Repositories;
using Plateaumed.EHR.Facilities.Abstractions;
using System.Linq;

namespace Plateaumed.EHR.Facilities.Query.BaseQueryHelper
{
    public class RoomBaseQuery : IBaseQuery
    {
        private readonly IRepository<Rooms, long> _roomsRepository;

        public RoomBaseQuery(IRepository<Rooms, long> roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public IQueryable<Rooms> GetAllActiveConsultingRooms()
        {
            return  _roomsRepository.GetAll().Where(r => r.Type == RoomType.ConsultingRoom);
        }

        public IQueryable<Rooms> GetAllActiveOperatingRooms(int? tenantId)
        {
            return _roomsRepository.GetAll().Where(r => r.Type == RoomType.OperatingRoom && r.TenantId == tenantId);
        }
    }
}
