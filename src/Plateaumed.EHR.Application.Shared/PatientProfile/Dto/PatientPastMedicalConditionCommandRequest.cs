using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.PatientProfile.Dto;
public class PatientPastMedicalConditionCommandRequest
{
    public string ChronicCondition { get; set; }

    public long SnomedId { get; set; }

    public int DiagnosisPeriod { get; set; }

    public UnitOfTime PeriodUnit { get; set; }

    public ConditionControl Control { get; set; }

    public bool IsOnMedication { get; set; }

    public string Notes { get; set; }

    public int NumberOfPreviousInfarctions { get; set; }

    public bool IsHistoryOfAngina { get; set; }

    public bool IsPreviousHistoryOfAngina { get; set; }

    public bool IsPreviousOfAngiogram { get; set; }

    public bool IsPreviousOfStenting { get; set; }

    public bool IsPreviousOfMultipleInfarction { get; set; }

    public bool IsStillIll { get; set; }

    public bool IsPrimaryTemplate { get; set; }

    [Required]
    public long PatientId { get; set; }

    public List<PatientPastMedicalConditionMedicationRequest> Medications { get; set; }
    public long? Id { get; set; }
}
