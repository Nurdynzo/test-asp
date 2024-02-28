namespace Plateaumed.EHR.Medication.Dtos;

public class SearchMedicationForReturnDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public string GenericName { get; set; }
    public string ActiveIngredient { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public string DoseForm { get; set; }
    public string DoseStrength { get; set; }
    public string Source { get; set; }
    
}