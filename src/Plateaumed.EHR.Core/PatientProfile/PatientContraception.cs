using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientContraception")]
    public class PatientContraception: FullAuditedEntity<long>, IMustHaveTenant
    {
        public bool IsContraceptionEverUsed { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string TypeOfContraceptionUsed { get; set; }

        public long? ContraceptionSnomedId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool IsContraceptionInUsed { get; set; }

        public bool IsComplicationExperienced { get; set; }

        public string Notes { get; set; }

        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}