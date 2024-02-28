using System.Collections.Generic;
using JetBrains.Annotations;

namespace Plateaumed.EHR.NurseCarePlans.Dto;

public class CreateNurseCarePlanRequest
{
    public long PatientId { get; set; }
    public long EncounterId { get; set; }
    public long? DiagnosisId { get; set; }
    public List<long> OutcomesId { get; set; }
    public List<long> InterventionsId { get; set; }
    public List<long> ActivitiesId { get; set; }
    public long? EvaluationId { get; set; }
    
    [CanBeNull] public string DiagnosisText { get; set; }
    public List<string> OutcomesText { get; set; }
    public List<string> InterventionsText { get; set; }
    public List<string> ActivitiesText { get; set; }
    [CanBeNull] public string EvaluationText { get; set; }
}