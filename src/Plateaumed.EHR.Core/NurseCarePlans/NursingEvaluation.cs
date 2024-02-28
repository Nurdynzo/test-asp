using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("NursingEvaluations")]
public class NursingEvaluation: Entity<long>
{
    public string Name { get; set; }
    
    public string Code { get; set; }
    
}