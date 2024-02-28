using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class DistrictDto : EntityDto<long>
    {
        public string Name {get; set;}
        public long RegionId {get; set;}
        
    }
}