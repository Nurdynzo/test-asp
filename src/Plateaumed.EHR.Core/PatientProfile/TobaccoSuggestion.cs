using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("TobaccoSuggestions")]
    public class TobaccoSuggestion : FullAuditedEntity<long>
    {
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string ModeOfConsumption { get; set; }
        public long SnomedId { get; set; }
    }
}
