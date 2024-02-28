namespace Plateaumed.EHR.NurseHistory.Dtos;

public class CreateNurseHistoryDto
{ 
    public long PatientId { get; set; } 
    
    public string Note { get; set; }
}