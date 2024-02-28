namespace Plateaumed.EHR.PatientProfile.Dto;
public class GetChronicDiseaseSuggestionQueryResponse
{
    
    public string Suggestion { get; set; }
    
    public string Synonym { get; set; }

    public long SnomedId { get; set; }
    
    public long? SynonymSnomedId { get; set; }

    public bool IsPrimaryFormat { get; set; }
}