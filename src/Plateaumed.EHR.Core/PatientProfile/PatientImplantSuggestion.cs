using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("ImplantSuggestions")]
    public class PatientImplantSuggestion : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public long? SnomedId { get; set; }
    }
}
