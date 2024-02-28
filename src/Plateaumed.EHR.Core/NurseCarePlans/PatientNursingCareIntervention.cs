using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("PatientNursingCareInterventions")]
public class PatientNursingCareIntervention : Entity<long>
{
    public long NursingCareSummaryId { get; set; }

    [ForeignKey("NursingCareSummaryId")] public NursingCareSummary NursingCareSummary { get; set; }
    
    public long? NursingCareInterventionsId { get; set; }

    [ForeignKey("NursingCareInterventionsId")]
    public NursingCareIntervention NursingCareIntervention { get; set; }

    [StringLength(TenantConsts.MaxIndividualSpecializationLength,
        MinimumLength = TenantConsts.MinIndividualSpecializationLength)]
    public string NursingCareInterventionsText { get; set; }
}