using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.WardEmergencies;

[Table("WardEmergencies")]
public class WardEmergency: Entity<long>
{
    public string Event { get; set; }

    public string ShortName { get; set; }

    public string SnomedId { get; set; }

    public List<NursingIntervention> Interventions { get; set; }
}