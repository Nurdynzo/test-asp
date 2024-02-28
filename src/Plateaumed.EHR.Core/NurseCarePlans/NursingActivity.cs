using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("NursingActivities")]
public class NursingActivity: Entity<long>
{
    public string Name { get; set; }

    public string Code { get; set; }
    
    public long? NursingCareInterventionsId { get; set; }
    
    [ForeignKey("NursingCareInterventionsId")]
    public NursingCareIntervention NursingCareIntervention { get; set; }

}