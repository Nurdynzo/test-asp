using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.AllInputs;

public class Discharge : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public long PatientId { get; set; }
    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }
    public long? AppointmentId { get; set; }
    [ForeignKey("AppointmentId")]
    public virtual PatientAppointment Appointment { get; set; }

    public bool IsFinalized { get; set; }
    public DischargeEntryType DischargeType { get; set; }
    public DischargeStatus status { get; set; }
    public bool IsBroughtInDead { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string TimeOfDeath { get; set; }
    public string TimeCPRCommenced { get; set; }
    public string TimeCPREnded { get; set; }
    [ForeignKey("CreatorUserId")]
    public virtual User CreatorUser { get; set; }
    public long? EncounterId { get; set; }
    [ForeignKey("EncounterId")]
    public PatientEncounter PatientEncounter { get; set; }
    public string PatientAssessment { get; set; }
    public string ActivitiesOfDailyLiving { get; set; }
    public string Drugs { get; set; }
}