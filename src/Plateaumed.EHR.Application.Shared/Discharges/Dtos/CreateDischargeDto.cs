using Plateaumed.EHR.DoctorDischarge;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Discharges.Dtos;

public class CreateNormalDischargeDto
{
    public long? Id { get; set; }
    public DischargeEntryType DischargeType { get; set; }
    public string Note { get; set; }
    public List<CreateDischargeMedicationDto> Prescriptions { get; set; }
    public List<CreateDischargePlanItemDto> PlanItems { get; set; }
    public long EncounterId { get; set; }
    public long? AppointmentId { get; set; }
}
public class CreateMarkAsDeceasedDischargeDto
{
    public long? Id { get; set; }
    public string Note { get; set; }
    public long EncounterId { get; set; }
    public bool IsBroughtInDead { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string TimeOfDeath { get; set; }
    public string TimeCPRCommenced { get; set; }
    public string TimeCPREnded { get; set; }
    public List<PatientCauseOfDeathDto> CausesOfDeath { get; set; }
}
public class CreateDischargeDto
{
    public long? Id { get; set; }
    public long PatientId { get; set; }
    public DischargeEntryType DischargeType { get; set; }
    public DischargeStatus Status { get; set; }
    public bool IsBroughtInDead { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string TimeOfDeath { get; set; }
    public string TimeCPRCommenced { get; set; }
    public string TimeCPREnded { get; set; }
    public List<PatientCauseOfDeathDto> CausesOfDeath { get; set; }
    public string Note { get; set; }
    public List<CreateDischargeMedicationDto> Prescriptions { get; set; }
    public List<CreateDischargePlanItemDto> PlanItems { get; set; }
    public long? AppointmentId { get; set; }
    public long EncounterId { get; set; }
}
