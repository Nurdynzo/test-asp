using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;
using Abp.Runtime.Session;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IGetOperatingRoomsQueryHandler :  ITransientDependency
    {
        Task<PagedResultDto<GetFacilityOperatingRoomsForViewDto>> Handle(GetAllFacilityOperatingRoomsInput input, IAbpSession abpSession);

        Task<List<OperatingRoomDto>> GetAllOperatingRooms(IAbpSession abpSession);
    }
}
