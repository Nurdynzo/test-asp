using System.Collections.Generic;

namespace Plateaumed.EHR.Medication.Dtos;

public class MedicationSuggestionForReturnDto
{
    public string[] Dose { get; set; }
    public string[] Unit { get; set; }
    public string[] Frequency { get; set; }
    public string[] Direction { get; set; }
    public string[] Duration { get; set; } 
}