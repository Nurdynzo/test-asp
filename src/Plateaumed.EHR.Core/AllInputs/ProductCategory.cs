using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.AllInputs;

public class ProductCategory : FullAuditedEntity<long>
{
    public string CategoryName { get; set; }
}