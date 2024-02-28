using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
namespace Plateaumed.EHR.PatientProfile
{
    [Table("PatientGynaecologicalIllnessSuggestions")]
    public class PatientGynaecologicalIllnessSuggestion : FullAuditedEntity<long>
    {
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Name { get; set; }

        public long? SnomedId { get; set; }
    }
}