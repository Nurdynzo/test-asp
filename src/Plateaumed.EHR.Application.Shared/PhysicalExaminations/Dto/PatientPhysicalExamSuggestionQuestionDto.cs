using System.Collections.Generic;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class PatientPhysicalExamSuggestionQuestionDto
{
    public string HeaderName { get; set; }
    public List<PatientPhysicalExamSuggestionAnswerDto> PatientPhysicalExamSuggestionAnswers { get; set; }
}