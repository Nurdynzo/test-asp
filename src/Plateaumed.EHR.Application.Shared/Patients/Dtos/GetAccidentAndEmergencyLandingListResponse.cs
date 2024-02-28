using System;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Patients.Dtos;

public class GetAccidentAndEmergencyLandingListResponse
{
    public long PatientId { get; set; }
    public string FullName { get; set; }
    public long EncounterId { get; set; }
    public GenderType Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public List<string> Diagnosis { get; set; }
    public EncounterStatusType Status { get; set; }
    public List<AandEPatientVitalsDto> PatientVitals { get; set; }
    public string BedNumber { get; set; }
    public string ImageUrl { get; set; }    
    public AandEAttendingPhysician AttendingPhysician { get; set; }
    public string LastSeenBy { get; set; }
    public DateTime? LastSeenAt { get; set; }
}