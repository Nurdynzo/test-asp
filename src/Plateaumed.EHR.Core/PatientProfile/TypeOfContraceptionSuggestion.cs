using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("TypeOfContraceptionSuggestions")]
    public class TypeOfContraceptionSuggestion : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        
        public long? SnomedId { get; set; }
    }
}