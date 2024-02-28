using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditRoomsDto : EntityDto<long?>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual long FacilityId { get; set; }
        public List<RoomAvailabilityDto> RoomAvailability { get; set; }
        public int? MinTimeInterval { get; set; }
        public RoomType Type { get; set; }
    }
}
