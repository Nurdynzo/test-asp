using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Patients.Dtos;

public class GetPatientPastMedicalCondition
{
    public long Id { get; set; }
    public string ChronicCondition { get; set; }
    public long SnomedId { get; set; }
    public string DiagnosisPeriod { get; set; }
    public string PeriodUnit { get; set; }
    public string Control { get; set; }
    public bool IsOnMedication { get; set; }
    public string Notes { get; set; }
    public int NumberOfPreviousInfarctions { get; set; }
    public bool IsHistoryOfAngina { get; set; }
    public bool IsPreviousHistoryOfAngina { get; set; }
    public bool IsPreviousOfAngiogram { get; set; }
    public bool IsPreviousOfStenting { get; set; }
    public bool IsPreviousOfMultipleInfarction { get; set; }
    public bool IsStillIll { get; set; }
    public long PatientId { get; set; }
    public bool IsPrimaryTemplate { get; set; }
    public DateTime LastEnteredByDate { get; set; }
    public string LastEnteredBy { get; set; }
    public List<PatientPastMedicalConditionMedicationResponse> Medications { get; set; }
}
