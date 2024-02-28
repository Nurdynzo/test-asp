using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientMensurationDurations")]
    public class PatientMensurationDuration : FullAuditedEntity<long> , IMustHaveTenant
    {
        public DateTime LastDayOfPeriod { get; set; }
        
        public int AveragePeriodDuration { get; set; }
        
        public bool IsPeriodPredictable { get; set; }
        
        public int AverageCycleLength { get; set; }
        
        public UnitOfTime AverageCycleLengthUnit { get; set; }
        
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string RequestedTest { get; set; }

        public string Notes { get; set; }
        
        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}