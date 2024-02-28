using Plateaumed.EHR.Insurance;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientInsurers")]
    [Audited]
    public class PatientInsurer : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual InsuranceProviderType Type { get; set; }

        public virtual InsuranceBenefiaryType BenefiaryType { get; set; }

        [StringLength(
            PatientInsurerConsts.MaxCoverageLength,
            MinimumLength = PatientInsurerConsts.MinCoverageLength
        )]
        public virtual string Coverage { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        [StringLength(
            PatientInsurerConsts.MaxInsuranceCodeLength,
            MinimumLength = PatientInsurerConsts.MinInsuranceCodeLength
        )]
        public virtual string InsuranceCode { get; set; }

        public virtual long InsuranceProviderId { get; set; }

        [ForeignKey("InsuranceProviderId")]
        public InsuranceProvider InsuranceProviderFk { get; set; }

        public virtual long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
    }
}
