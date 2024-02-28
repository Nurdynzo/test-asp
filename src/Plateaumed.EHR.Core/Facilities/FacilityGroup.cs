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
    [Table("FacilityGroups")]
    [Audited]
    public class FacilityGroup : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [StringLength(
            FacilityGroupConsts.MaxNameLength,
            MinimumLength = FacilityGroupConsts.MinNameLength
        )]
        public virtual string Name { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxEmailAddressLength,
            MinimumLength = FacilityGroupConsts.MinEmailAddressLength
        )]
        public virtual string EmailAddress { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxPhoneNumberLength,
            MinimumLength = FacilityGroupConsts.MinPhoneNumberLength
        )]
        public virtual string PhoneNumber { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxWebsiteLength,
            MinimumLength = FacilityGroupConsts.MinWebsiteLength
        )]
        public virtual string Website { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxAddressLength,
            MinimumLength = FacilityGroupConsts.MinAddressLength
        )]
        public virtual string Address { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxCityLength,
            MinimumLength = FacilityGroupConsts.MinCityLength
        )]
        public virtual string City { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxStateLength,
            MinimumLength = FacilityGroupConsts.MinStateLength
        )]
        public virtual string State { get; set; }

        [StringLength(
            CountryConsts.MaxNameLength,
            MinimumLength = CountryConsts.MinNameLength
        )]
        public virtual string Country { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxPostCodeLength,
            MinimumLength = FacilityGroupConsts.MinPostCodeLength
        )]
        public virtual string PostCode { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxBankNameLength,
            MinimumLength = FacilityGroupConsts.MinBankNameLength
        )]
        public virtual string BankName { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxBankAccountHolderLength,
            MinimumLength = FacilityGroupConsts.MinBankAccountHolderLength
        )]
        public virtual string BankAccountHolder { get; set; }

        [StringLength(
            FacilityGroupConsts.MaxBankAccountNumberLength,
            MinimumLength = FacilityGroupConsts.MinBankAccountNumberLength
        )]
        public virtual string BankAccountNumber { get; set; }

        [ForeignKey("PatientCodeTemplateId")]
        public PatientCodeTemplate PatientCodeTemplateFk { get; set; }

        [ForeignKey("StaffCodeTemplateId")]
        public StaffCodeTemplate StaffCodeTemplateFk { get; set; }

        public ICollection<Facility> ChildFacilities { get; set; }

        public Guid? LogoId { get; set; }
    }
}
