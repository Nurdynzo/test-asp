using Plateaumed.EHR.Misc;

using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Organizations.Dtos
{
    public class OrganizationUnitTimeDto : NullableIdDto<long>
    {
        public DaysOfTheWeek DayOfTheWeek { get; set; }

        public DateTime? OpeningTime { get; set; }

        public DateTime? ClosingTime { get; set; }

        public bool IsActive { get; set; }

        public long? OrganizationUnitExtendedId { get; set; }
    }
}