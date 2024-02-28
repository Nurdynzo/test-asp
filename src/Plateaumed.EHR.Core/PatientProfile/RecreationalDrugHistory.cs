using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("RecreationalDrugHistories")]
    public class RecreationalDrugHistory : FullAuditedEntity<long>
    {
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }
        public bool PatientDoesNotTakeRecreationalDrugs { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string DrugUsed { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Route { get; set; }
        public bool StillUsingDrugs { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Note { get; set; }
    }
}