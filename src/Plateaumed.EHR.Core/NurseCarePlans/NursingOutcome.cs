using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("NursingOutcomes")]
public class NursingOutcome: Entity<long>
{
    public string Name { get; set; }

    public string Code { get; set; }
    
    public long? DiagnosisId { get; set; }
    
    [ForeignKey("DiagnosisId")]
    public NursingDiagnosis Diagnosis { get; set; }
    
    public List<NursingCareIntervention> Interventions { get; set; }

}