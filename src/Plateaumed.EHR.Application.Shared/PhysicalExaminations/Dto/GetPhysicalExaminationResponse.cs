using System.Collections.Generic;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class GetPhysicalExaminationResponse
{
    public string Type { get; set; }
    public string Header { get; set; }
    public string PresentTerms { get; set; }
    public string SnomedId { get; set; }
    public string AbsentTerms { get; set; }
    public string AbsentTermsSnomedId { get; set; }
    public bool Site { get; set; }
    public bool Plane { get; set; }
    public List<ExaminationQualifierDto> Qualifiers { get; set; }
    public List<string> PlaneTypes { get; set; }
    public string Unit { get; set; }
}