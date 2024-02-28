using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("DrugHistories")]
    public class DrugHistory : FullAuditedEntity<long>
    {
        public bool PatientOnMedication { get; set; }
        public long PatientId { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string MedicationName { get; set; }

        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Route { get; set; }
        public int Dose { get; set; }
        [StringLength(GeneralStringLengthConstant.ShortStringLength)]
        public string DoseUnit { get; set; }
        public int PrescriptionFrequency { get; set; }

        [StringLength(GeneralStringLengthConstant.ShortStringLength)]
        public string PrescriptionInterval { get; set; }
        public bool CompliantWithMedication { get; set; }
        public int UsageFrequency { get; set; }

        [StringLength(GeneralStringLengthConstant.ShortStringLength)]
        public string UsageInterval { get; set; }
        public bool IsMedicationStillBeingTaken { get; set; }
        public int WhenMedicationStopped { get; set; }

        [StringLength(GeneralStringLengthConstant.ShortStringLength)]
        public string StopInterval { get; set; }
        public string Note { get; set; }
    }
}
