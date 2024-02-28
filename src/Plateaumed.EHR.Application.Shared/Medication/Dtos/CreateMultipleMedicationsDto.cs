using System.Collections.Generic;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.Medication.Dtos;

public class CreateMultipleMedicationsDto
{
    public long? ProcedureId { get; set; } = null;
    
    public ProcedureEntryType? ProcedureEntryType { get; set; }
    
    public List<CreateMedicationDto> Prescriptions { get; set; } 
    
    public long EncounterId { get; set; }
}

public class CreateMedicationDto
{ 
    public long PatientId { get; set; } 
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductSource { get; set; }
    public string DoseUnit { get; set; }
    public int? DoseValue { get; set; }
    public string Frequency { get; set; }
    public int? FrequencyValue { get; set; }
    public string Duration { get; set; }
    public int? DurationValue { get; set; }
    public string Direction { get; set; }
    public string Note { get; set; }
}
