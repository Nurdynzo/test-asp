using Abp.Dependency;
using System.Linq;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IBaseQuery : ITransientDependency
    {
        IQueryable<Rooms> GetAllActiveConsultingRooms();
        IQueryable<Rooms> GetAllActiveOperatingRooms(int? tenantId);
    }
}
