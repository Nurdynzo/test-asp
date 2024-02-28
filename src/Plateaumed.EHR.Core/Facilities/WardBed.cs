using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Facilities
{
    [Table("WardBeds")]
    [Audited]
    public class WardBed : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual string BedNumber { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long BedTypeId { get; set; }

        [ForeignKey("BedTypeId")]
        public BedType BedType { get; set; }

        public virtual long WardId { get; set; }

        [ForeignKey("WardId")]
        public Ward WardFk { get; set; }

        public long? EncounterId { get; set; }

        [ForeignKey("EncounterId")]
        public PatientEncounter PatientEncounter { get; set; } 
    }
}
