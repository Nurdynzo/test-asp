using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Symptom.Dtos;

public class CreateSymptomDto
{
    public string SymptomEntryType { get; set; }
    public long PatientId { get; set; }
    public long? Stamp { get; set; }
    public string SymptomSnowmedId { get; set; }
    public long EncounterId { get; set; }

    [StringLength(UserConsts.MaxDescriptionLength, MinimumLength = UserConsts.MinDescriptionLength)]
    public string Description { get; set; }
    public string OtherNote { get; set; } 
    public List<SymptomTypeNoteRequestDto> TypeNotes { get; set; }
    public List<SuggestionQuestionForCreationDto> Suggestions { get; set; }
}

public class SymptomTypeNoteRequestDto
{
    public string Type { get; set; }
    public string Note { get; set; }
}

public class SuggestionQuestionForCreationDto
{
    public string SuggestionQuestionType { get; set; }
    [Required] public SymptomSuggestionAnswerDto SymptomSuggestionAnswer { get; set; }
} 

public class SymptomSuggestionQuestionDto
{
    public SuggestionQuestionType SuggestionQuestionType { get; set; }
    public SymptomSuggestionAnswerDto SymptomSuggestionAnswer { get; set; }
} 

public class SymptomSuggestionAnswerDto
{
    public string SymptomSnowmedId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string WhenOrHowLongAgo { get; set; } = string.Empty;
    public string Cyclicality { get; set; } = string.Empty;
    public bool IsAbsent { get; set; } = false;
    public string Frequency { get; set; } = string.Empty;
    public string HowLongDidItLast { get; set; } = string.Empty;
    public string ExacerbatingOrRelievingType { get; set; } = string.Empty;
    public int SeverityScale { get; set; }
}