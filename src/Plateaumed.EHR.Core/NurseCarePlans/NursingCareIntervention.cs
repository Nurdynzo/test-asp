using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("NursingCareIntervention")]
public class NursingCareIntervention: Entity<long>
{
    public string Name { get; set; }

    public string Code { get; set; }
    
    public long? NursingOutcomesId { get; set; }

    [ForeignKey("NursingOutcomesId")]
    public NursingOutcome NursingOutcome { get; set; }
    
    public List<NursingActivity> Activities { get; set; }
}