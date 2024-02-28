using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class RoomAvailabilityDto : EntityDto<long?>
    {
        public DayOfWeek? DayOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;
        public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;
    }
}
