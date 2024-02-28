using System;

namespace Plateaumed.EHR.Medication.Dtos;

public class PatientMedicationForReturnDto
{
    public long Id { get; set; } 
    public long PatientId { get; set; } 
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductSource { get; set; }
    public string DoseUnit { get; set; } 
    public string Frequency { get; set; }
    public string Duration { get; set; }
    public string Direction { get; set; }
    public string Note { get; set; } 
    public long? ProcedureId { get; set; }
    public string ProcedureEntryType { get; set; }
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }
    public bool IsAdministered { get; set; }
    public bool IsDiscontinued { get; set; }
    public string  DiscontinueUser { get; set; }
    
}
