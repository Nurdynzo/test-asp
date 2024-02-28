using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.PatientProfile
{
    public class ReviewOfSystemsSuggestion : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public long SnomedId { get; set; }
        public SymptomsCategory Category { get; set; }
    }
}
