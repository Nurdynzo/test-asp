using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class FacilityTypeDto : EntityDto<long>
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
