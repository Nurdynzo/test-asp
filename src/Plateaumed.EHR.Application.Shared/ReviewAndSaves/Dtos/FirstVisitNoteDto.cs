using System;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class FirstVisitNoteDto
    {
        public long PatientId { get; set; }
        public long EncounterId { get; set; }
        public DateTime DateGenerated { get; set; }
        public DoctorSummaryDto DoctorSummary { get; set; }
        public EncounterSummaryDto EncounterSummary { get; set; }
        public NurseSummaryDto NurseSummary { get; set; }
    }
}
