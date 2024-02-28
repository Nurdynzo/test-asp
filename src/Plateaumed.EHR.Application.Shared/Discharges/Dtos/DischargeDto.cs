using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Medication.Dtos;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Discharges.Dtos;

public class NormalDischargeReturnDto
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public bool IsFinalized { get; set; }
    public long? EncounterId { get; set; }
    public long? AppointmentId { get; set; }
    public DischargeEntryType DischargeType { get; set; }
    public string DischargeTypeStr { get; set; }
    public DischargeStatus status { get; set; }
    public string StatusStr { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifieddAt { get; set; }
}
public class MarkAsDeceaseDischargeReturnDto
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public bool IsFinalized { get; set; }
    public long? EncounterId { get; set; }
    public bool IsBroughtInDead { get; set; }
    public DischargeEntryType DischargeType { get; set; }
    public string DischargeTypeStr { get; set; }
    public DischargeStatus status { get; set; }
    public string StatusStr { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifieddAt { get; set; }
}
public class DischargeDto
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public bool IsFinalized { get; set; }
    public DischargeEntryType DischargeType { get; set; }
    public string DischargeTypeStr { get; set; }
    public DischargeStatus status { get; set; }
    public string StatusStr { get; set; }
    public bool IsBroughtInDead { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string TimeOfDeath { get; set; }
    public string TimeCPRCommenced { get; set; }
    public string TimeCPREnded { get; set; }
    public List<PatientCauseOfDeathDto> CausesOfDeath { get; set; }
    public List<DischargeNoteDto> Note { get; set; }
    public long? AppointmentId { get; set; }
    public long? EncounterId { get; set; }
    public List<PatientMedicationForReturnDto> Prescriptions { get; set; }
    public List<DischargePlanItemDto> PlanItems { get; set; }
    public string PatientAssessment { get; set; }
    public string ActivitiesOfDailyLiving { get; set; }
    public string Drugs { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifieddAt { get; set; }
}