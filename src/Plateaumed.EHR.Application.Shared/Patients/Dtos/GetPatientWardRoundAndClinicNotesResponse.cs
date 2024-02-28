using System;

namespace Plateaumed.EHR.Patients.Dtos;

public class GetPatientWardRoundAndClinicNotesResponse
{
    public long PatientId { get; set; }
    
    public string Clinic  { get; set; }
    
    public DateTime DateTime { get; set; }

    public string Notes { get; set; }

    public string Ward { get; set; }
}