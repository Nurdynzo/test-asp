using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientGynaecologicalProcedures")]
    public class PatientGynaecologicalProcedure : FullAuditedEntity<long> , IMustHaveTenant
    {
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Procedure { get; set; }

        public long? ProcedureSnomedId { get; set; }

        public int ProcedurePeriod { get; set; }

        public UnitOfTime ProcedurePeriodUnit { get; set; }

        public bool IsComplicationExperienced { get; set; }

        public string Notes { get; set; }
        
        public int TenantId { get; set; }
        
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}