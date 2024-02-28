using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Plateaumed.EHR.Vaccines;

[Table("VaccineGroups")]
public class VaccineGroup : FullAuditedEntity<long>
{
    public string Name { get; set; }

    public string FullName { get; set; }

    public ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}