using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class WardBedDto : EntityDto<long>
    {
        public string BedNumber { get; set; }

        public bool IsActive { get; set; }

        public long BedTypeId { get; set; }

        public long WardId { get; set; }

        public string BedTypeName { get; set; }

    }
}
