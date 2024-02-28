using System;
using System.Collections.Generic;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Patients.Dtos;

public class GetInpatientLandingListResponse
{
    public long PatientId { get; set; }
    public string FullName { get; set; }
    public long EncounterId { get; set; }
    public GenderType Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public List<string> Diagnosis { get; set; }
    public EncounterStatusType Status { get; set; }
    public List<PatientVitalsDto> PatientVitals { get; set; }
    public string BedNumber { get; set; }
    public string ImageUrl { get; set; }
    public AttendingPhysician AttendingPhysician { get; set; }
    public string LastSeenBy { get; set; }
    public DateTime? LastSeenAt { get; set; }
    public List<MedicationsListDto> PatientMedications { get; set; }
    public List<SelectedProceduresDto> Interventions { get; set; }
    public string StabilityStatus { get; set; }
    public MoneyDto PaidAmount { get; set; }
    public MoneyDto OutstandingAmount { get; set; }
    public MoneyDto TotalAmount { get; set; }
}