using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Facilities
{
    [Table("RoomAvailability")]
    [Audited]
    public class RoomAvailability : FullAuditedEntity<long>
    {
        public virtual long RoomsId { get; set; }

        [ForeignKey("RoomsId")]
        public Rooms Rooms { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }
    }
}
