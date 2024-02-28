using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface ICreateRoomsCommandHandler : ITransientDependency
    {
        Task<Rooms> Handle(CreateOrEditRoomsDto createRoomsDto, int? tenantId, long? userId);
    }
}
