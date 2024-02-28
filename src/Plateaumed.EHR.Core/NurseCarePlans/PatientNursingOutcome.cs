using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("PatientNursingOutcomes")]
public class PatientNursingOutcome : Entity<long>
{
    public long NursingCareSummaryId { get; set; }

    [ForeignKey("NursingCareSummaryId")] public NursingCareSummary NursingCareSummary { get; set; }
    public long? NursingOutcomeId { get; set; }

    [ForeignKey("NursingOutcomeId")] public NursingOutcome NursingOutcome { get; set; }

    [StringLength(TenantConsts.MaxIndividualSpecializationLength,
        MinimumLength = TenantConsts.MinIndividualSpecializationLength)]
    public string NursingOutcomesText { get; set; }
}