using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;
namespace Plateaumed.EHR.Procedures
{
    public class SpecializedProcedureSafetyCheckList : FullAuditedEntity<long>
    {
        [Column(TypeName = "jsonb")]
        public List<SafetyCheckList> CheckLists { get; set; }
        public long ProcedureId { get; set; }
        [ForeignKey("ProcedureId")]
        public Procedure Procedure { get; set; }
        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

    }

}
