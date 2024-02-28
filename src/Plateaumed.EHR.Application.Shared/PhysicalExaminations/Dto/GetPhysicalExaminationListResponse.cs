namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class GetPhysicalExaminationListResponse
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Header { get; set; }
    public string PresentTerms { get; set; }
    public string SnomedId { get; set; }
    public string AbsentTerms { get; set; }
    public string AbsentTermsSnomedId { get; set; }
    public bool HasQualifiers { get; set; }
}