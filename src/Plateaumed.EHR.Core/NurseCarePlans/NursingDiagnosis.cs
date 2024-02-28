using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("NursingDiagnosis")]
public class NursingDiagnosis: Entity<long>
{
    public string Name { get; set; }

    public string Code { get; set; }
    
    public List<NursingOutcome> Outcomes { get; set; }
}