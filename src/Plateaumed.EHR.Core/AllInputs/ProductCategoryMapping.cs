using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.AllInputs;

public class ProductCategoryMapping : FullAuditedEntity<long>
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
}