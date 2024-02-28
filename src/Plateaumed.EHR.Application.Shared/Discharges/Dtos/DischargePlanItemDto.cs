using System;
namespace Plateaumed.EHR.Discharges.Dtos
{
    public class DischargePlanItemDto
    {
        public long PlanItemId { get; set; }
        public long PatientId { get; set; }
        public long DischargeId { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
