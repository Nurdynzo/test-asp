using System;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using System.Collections.Generic;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Facilities
{
    [Table("Facilities")]
    [Audited]
    public class Facility : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(FacilityConsts.MaxNameLength, MinimumLength = FacilityConsts.MinNameLength)]
        public virtual string Name { get; set; }

        public virtual bool IsActive { get; set; }

        [StringLength(FacilityConsts.MaxEmailAddressLength, MinimumLength = FacilityConsts.MinEmailAddressLength)]
        public virtual string EmailAddress { get; set; }

        [StringLength(FacilityConsts.MaxPhoneNumberLength, MinimumLength = FacilityConsts.MinPhoneNumberLength)]
        public virtual string PhoneNumber { get; set; }

        [StringLength(FacilityConsts.MaxWebsiteLength, MinimumLength = FacilityConsts.MinWebsiteLength)]
        public virtual string Website { get; set; }

        [StringLength(FacilityConsts.MaxAddressLength, MinimumLength = FacilityConsts.MinAddressLength)]
        public virtual string Address { get; set; }

        [StringLength(FacilityConsts.MaxCityLength, MinimumLength = FacilityConsts.MinCityLength)]
        public virtual string City { get; set; }

        [StringLength(FacilityConsts.MaxStateLength, MinimumLength = FacilityConsts.MinStateLength)]
        public virtual string State { get; set; }

        [StringLength(CountryConsts.MaxNameLength, MinimumLength = CountryConsts.MinNameLength)]
        public string Country { get; set; }

        [StringLength(FacilityConsts.MaxPostCodeLength, MinimumLength = FacilityConsts.MinPostCodeLength)]
        public virtual string PostCode { get; set; }

        [StringLength(FacilityConsts.MaxBankNameLength, MinimumLength = FacilityConsts.MinBankNameLength)]
        public virtual string BankName { get; set; }

        [StringLength(FacilityConsts.MaxBankAccountHolderLength, MinimumLength = FacilityConsts.MinBankAccountHolderLength)]
        public virtual string BankAccountHolder { get; set; }

        [StringLength(FacilityConsts.MaxBankAccountNumberLength, MinimumLength = FacilityConsts.MinBankAccountNumberLength)]
        public virtual string BankAccountNumber { get; set; }

        public virtual bool? UseGroupAddress { get; set; }

        public virtual bool? UseGroupContacts { get; set; }

        public virtual bool? UseGroupBilling { get; set; }

        public virtual bool? HasPharmacy { get; set; }

        public virtual bool? HasLaboratory { get; set; }

        public virtual bool isDefault { get; set; }

        public virtual FacilityLevel? Level { get; set; }

        public virtual long GroupId { get; set; }

        [ForeignKey("GroupId")]
        public FacilityGroup GroupFk { get; set; }

        public virtual long TypeId { get; set; }

        [ForeignKey("TypeId")]
        public FacilityType TypeFk { get; set; }

        [ForeignKey("PatientCodeTemplateId")]
        public PatientCodeTemplate PatientCodeTemplate { get; set; }

        [ForeignKey("StaffCodeTemplateId")]
        public StaffCodeTemplate StaffCodeTemplateFk { get; set; }

        public Guid? LogoId{ get; set; }
        
        public virtual ICollection<Ward> Wards { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }

        public virtual ICollection<FacilityStaff> AssignedStaff { get; set; }
        
        public ICollection<PatientCodeMapping> PatientCodeMappings { get; set; }

        public virtual ICollection<FacilityBank> FacilityBanks { get; set; }

    }
}
