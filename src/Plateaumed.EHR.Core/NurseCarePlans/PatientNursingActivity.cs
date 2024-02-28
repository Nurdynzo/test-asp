using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using JetBrains.Annotations;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("PatientNursingActivities")]
public class PatientNursingActivity : Entity<long>
{
    public long NursingCareSummaryId { get; set; }

    [ForeignKey("NursingCareSummaryId")] public NursingCareSummary NursingCareSummary { get; set; }
    public long? NursingActivitiesId { get; set; }

    [ForeignKey("NursingActivitiesId")] public NursingActivity NursingActivity { get; set; }

    [StringLength(TenantConsts.MaxIndividualSpecializationLength,
        MinimumLength = TenantConsts.MinIndividualSpecializationLength)]
    [CanBeNull]
    public string NursingActivitiesText { get; set; }
}