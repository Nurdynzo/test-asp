using Abp.Auditing;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Facilities
{
    [Table("Rooms")]
    [Audited]
    
    public class Rooms : FullAuditedEntity<long>
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public virtual long FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility FacilityFk { get; set; }
        public bool IsActive { get; set; }
        public ICollection<RoomAvailability> Availabilities { get; set; }
        public int? MinTimeInterval { get; set; }

        public RoomType Type { get; set; }
    }
}
