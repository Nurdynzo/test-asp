using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditRoomsAvailabilityDto : EntityDto<long?>
    {
        public long RoomId { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;

        public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;
    }
}
