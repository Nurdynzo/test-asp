using Abp.Dependency;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IDeleteRoomAvailabilityCommandHandler :  ITransientDependency
    {
        Task Handle(long roomAvailabilityId);
    }
}
