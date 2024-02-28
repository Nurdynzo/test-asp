using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class OperatingRoomDto
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<RoomAvailabilityDto> Availabilities { get; set; }
    }

}
