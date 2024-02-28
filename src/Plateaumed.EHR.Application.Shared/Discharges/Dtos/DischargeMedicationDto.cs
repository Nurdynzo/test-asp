
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Discharges.Dtos
{
    public class DischargeMedicationDto
    {
        public long MedicationId { get; set; }
        public long PatientId { get; set; }
        public long DischargeId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSource { get; set; }
        public string DoseUnit { get; set; }
        public string Frequency { get; set; }
        public string Duration { get; set; }
        public string Direction { get; set; }
        public string Note { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
