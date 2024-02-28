using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class WardDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public long FacilityId { get; set; }

        public List<WardBedDto> WardBeds { get; set; }
    }
}
