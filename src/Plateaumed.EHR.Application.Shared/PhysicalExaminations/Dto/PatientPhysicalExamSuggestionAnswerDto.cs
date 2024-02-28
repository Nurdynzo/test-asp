using System.Collections.Generic;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class PatientPhysicalExamSuggestionAnswerDto
{
    public string SnowmedId { get; set; }
    public string Description { get; set; }
    public bool IsAbsent { get; set; }
    public List<PatientPhysicalExamSuggestionQualifierDto> Sites { get; set; }
    public List<PatientPhysicalExamSuggestionQualifierDto> Planes { get; set; }
    public List<PatientPhysicalExamSuggestionQualifierDto> Qualifiers { get; set; }
}