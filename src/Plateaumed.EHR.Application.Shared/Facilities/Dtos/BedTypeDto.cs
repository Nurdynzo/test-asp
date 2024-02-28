using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class BedTypeDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
