using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Patients;

[Table("ChronicDiseases")]
public class ChronicDisease: FullAuditedEntity<long>
{
    [StringLength(ChronicDiseaseConst.MaxSuggestionLength)]
    public string Suggestion { get; set; }
    
    [StringLength(ChronicDiseaseConst.SynonymLength)]
    public string Synonym { get; set; }

    public long SnomedId { get; set; }
    
    public long? SynonymSnomedId { get; set; }

    public bool IsPrimaryFormat { get; set; }
    
    
}