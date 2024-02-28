using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("RecreationalDrugsSuggestions")]
    public class RecreationalDrugSuggestion : FullAuditedEntity<long>
    {
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Name { get; set; }
        public long SnomedId { get; set; }
    }
}
