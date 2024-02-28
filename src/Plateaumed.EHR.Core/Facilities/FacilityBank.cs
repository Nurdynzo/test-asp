using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Facilities
{
    [Table("FacilityBanks")]
    [Audited]
    public class FacilityBank : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [StringLength(FacilityConsts.MaxBankNameLength, MinimumLength = FacilityConsts.MinBankNameLength)]
        public virtual string BankName { get; set; }

        [StringLength(FacilityConsts.MaxBankAccountHolderLength, MinimumLength = FacilityConsts.MinBankAccountHolderLength)]
        public virtual string BankAccountHolder { get; set; }

        [StringLength(FacilityConsts.MaxBankAccountNumberLength, MinimumLength = FacilityConsts.MinBankAccountNumberLength)]
        public virtual string BankAccountNumber { get; set; }

        public virtual bool IsDefault { get; set; }

        public virtual bool IsActive { get; set; }

        public long FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }
    }
}
