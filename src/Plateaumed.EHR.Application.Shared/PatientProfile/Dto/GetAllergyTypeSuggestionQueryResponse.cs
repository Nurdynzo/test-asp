namespace Plateaumed.EHR.PatientProfile.Dto;

public class GetAllergyTypeSuggestionQueryResponse
{
    public long Id { get; set; }

    public string Name { get; set; }

    public long? SnomedId { get; set; }
}