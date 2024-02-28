using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.WardEmergencies;

[Table("NursingInterventions")]
public class NursingIntervention: Entity<long>
{
    public string Name { get; set; }

    public long WardEmergencyId { get; set; }

    [ForeignKey("WardEmergencyId")]
    public WardEmergency WardEmergency { get; set; }
}