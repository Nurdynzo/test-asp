using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityConsultingRoomsInput :  PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public long? FacilityIdFilter { get; set; }
    }
}
