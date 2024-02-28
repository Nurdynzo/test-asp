using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityConsultingRoomsForViewDto
    {
        public ConsultingRoomResponseDto consultingRoomResponseDto { get; set; }
        public string FacilityName { get; set; }
    }
}
