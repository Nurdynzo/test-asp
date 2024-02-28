using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Misc.Dtos
{
    public class RegionDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string ShortName {get; set;}
        public string CountryCode {get; set;}

    }
}