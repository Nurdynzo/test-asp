using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.AllInputs;

public class GenericDrug : FullAuditedEntity<long>
{
    public string GenericSctName { get; set; }
    public string ActiveIngredients { get; set; }
    public string DoseForm { get; set; }
}