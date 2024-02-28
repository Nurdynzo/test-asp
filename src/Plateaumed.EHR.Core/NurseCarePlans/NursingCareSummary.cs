using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.NurseCarePlans;

[Table("NursingCareSummaries")]
public class NursingCareSummary : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long PatientId { get; set; }

    [ForeignKey("PatientId")] public Patient Patient { get; set; }

    public long EncounterId { get; set; }

    [ForeignKey("EncounterId")] public PatientEncounter Encounter { get; set; }

    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    [CanBeNull]
    public string NursingDiagnosisText { get; set; }
    
    [StringLength(TenantConsts.MaxIndividualSpecializationLength, MinimumLength = TenantConsts.MinIndividualSpecializationLength)]
    [CanBeNull]
    public string NursingEvaluationText { get; set; }

    public long? NursingDiagnosisId { get; set; }
    public long? NursingEvaluationId { get; set; }

    public List<PatientNursingOutcome> NursingOutcomes { get; set; }
    public List<PatientNursingCareIntervention> NursingCareInterventions { get; set; }
    public List<PatientNursingActivity> NursingActivities { get; set; }
    
    [ForeignKey("NursingEvaluationId")]
    [CanBeNull]
    public NursingEvaluation NursingEvaluation { get; set; }
    
    [ForeignKey("NursingDiagnosisId")] [CanBeNull] public NursingDiagnosis NursingDiagnosis { get; set; }

}