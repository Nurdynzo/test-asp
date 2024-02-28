using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityOperatingRoomsForViewDto
    {
        public OperatingRoomResponseDto OperatingRoomResponseDto { get; set; }
        public string FacilityName { get; set; }
    }
}
