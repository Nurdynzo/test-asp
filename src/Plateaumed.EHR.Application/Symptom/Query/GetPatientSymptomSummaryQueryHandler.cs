using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Symptom.Abstractions;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.Symptom.Query.BaseQueryHelper;

public class GetPatientSymptomSummaryQueryHandler : IGetPatientSymptomSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IRepository<User,long> _userRepository;

    public GetPatientSymptomSummaryQueryHandler(IBaseQuery baseQuery, IRepository<User, long> userRepository)
    {
        _baseQuery = baseQuery;
        _userRepository = userRepository;
    }
    
    public async Task<List<PatientSymptomSummaryForReturnDto>> Handle(long patientId, int? tenantId, long? encounterId = null)
    {
        var symptoms = await (from v in _baseQuery
                                  .GetPatientSymptomsBaseQuery(patientId, tenantId, encounterId)
                              join u in _userRepository.GetAll() on v.DeleterUserId equals u.Id into uJoin
                              from u in uJoin.DefaultIfEmpty()
                              orderby v.Id descending
                              select new PatientSymptomSummaryForReturnDto
                              {
                                  CreatorUserId = v.CreatorUserId ?? 0,
                                  CreationTime = v.CreationTime,
                                  DeletionTime = v.DeletionTime,
                                  Description = v.Description,
                                  Id = v.Id,
                                  IsDeleted = v.IsDeleted,
                                  SymptomEntryType = v.SymptomEntryType,
                                  SymptomEntryTypeName = v.SymptomEntryType.ToString(),
                                  OtherNote = v.OtherNote,
                                  DeletedUser = u != null ? u.DisplayName : string.Empty,
                                  EncounterId = v.EncounterId,
                                  JsonData = v.JsonData,
                              })
            .ToListAsync();

        foreach (var symptomSummaryForReturn in symptoms)
        {
            if (symptomSummaryForReturn.SymptomEntryType == SymptomEntryType.TypeNote)
                symptomSummaryForReturn.TypeNotes =
                    symptomSummaryForReturn.JsonData != null
                        ? JsonConvert.DeserializeObject<List<SymptomTypeNoteRequestDto>>(symptomSummaryForReturn.JsonData)
                        : new List<SymptomTypeNoteRequestDto>();
            
            else if (symptomSummaryForReturn.SymptomEntryType == SymptomEntryType.Suggestion)
                symptomSummaryForReturn.SuggestionSummary = symptomSummaryForReturn.JsonData != null ? SymptomSuggestionSummary(symptomSummaryForReturn,
                    JsonConvert.DeserializeObject<List<SymptomSuggestionQuestionDto>>(symptomSummaryForReturn.JsonData)): string.Empty;
        }

        return symptoms;
    }

    private string SymptomSuggestionSummary(PatientSymptomSummaryForReturnDto symptomSummary, List<SymptomSuggestionQuestionDto> suggestionQuestions)
    {
        if (suggestionQuestions.Count == 0)
            return string.Empty;

        var summary = $"{symptomSummary.Description}";
        var sites = new List<string>(); 
        var onset = new List<string>(); var cyclicality = new List<string>();  
        var frequency = new List<string>(); var howLongDidItLast = new List<string>();
        var severity = new List<string>();
        var characterPresent = new List<string>(); var characterAbsent = new List<string>();
        var radiationPresent = new List<string>(); var radiationAbsent = new List<string>();
        var associationPresent = new List<string>(); var associationAbsent = new List<string>();
        var exacerbating = new List<string>(); var relieving = new List<string>();

        // Loop through the suggestion question answers
        foreach (var suggestionQst in suggestionQuestions)
        {
            if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.Site)
            {
                // Get Site
                if(suggestionQst.SymptomSuggestionAnswer?.Description != string.Empty)
                    sites.Add(suggestionQst.SymptomSuggestionAnswer?.Description);
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.Onset)
            {
                // Get Onset
                if(!string.IsNullOrWhiteSpace(suggestionQst.SymptomSuggestionAnswer?.WhenOrHowLongAgo))
                    onset.Add(suggestionQst.SymptomSuggestionAnswer?.WhenOrHowLongAgo);
                
                if(suggestionQst.SymptomSuggestionAnswer?.Cyclicality != string.Empty)
                    cyclicality.Add(suggestionQst.SymptomSuggestionAnswer?.Cyclicality);
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.Character)
            {
                // Get Character
                if (suggestionQst.SymptomSuggestionAnswer is { IsAbsent: true })
                {
                    if(suggestionQst.SymptomSuggestionAnswer.Description != string.Empty)
                        characterAbsent.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(suggestionQst.SymptomSuggestionAnswer.Description))
                        characterPresent.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.Radiation)
            {
                // Get Radiation
                if (suggestionQst.SymptomSuggestionAnswer is { IsAbsent: true })
                {
                    if(suggestionQst.SymptomSuggestionAnswer.Description != string.Empty)
                        radiationAbsent.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(suggestionQst.SymptomSuggestionAnswer?.Description))
                        radiationPresent.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.Associations)
            {
                // Get Associations
                if (suggestionQst.SymptomSuggestionAnswer is { IsAbsent: true })
                {
                    if(suggestionQst.SymptomSuggestionAnswer.Description != string.Empty)
                        associationAbsent.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(suggestionQst.SymptomSuggestionAnswer.Description))
                        associationPresent.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.TimeCourse)
            {
                // Get TimeCourse
                if(suggestionQst.SymptomSuggestionAnswer?.Frequency != string.Empty)
                    frequency.Add(suggestionQst.SymptomSuggestionAnswer?.Frequency);
                
                if(suggestionQst.SymptomSuggestionAnswer?.HowLongDidItLast != string.Empty)
                    howLongDidItLast.Add(suggestionQst.SymptomSuggestionAnswer?.HowLongDidItLast);
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.ExacerbatingOrRelieving)
            {
                // Get ExacerbatingOrRelieving
                if (suggestionQst.SymptomSuggestionAnswer != null && suggestionQst.SymptomSuggestionAnswer.ExacerbatingOrRelievingType.Equals("Relieving", StringComparison.InvariantCultureIgnoreCase))
                {
                    if(suggestionQst.SymptomSuggestionAnswer.Description != string.Empty)
                        relieving.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
                else if (suggestionQst.SymptomSuggestionAnswer != null && suggestionQst.SymptomSuggestionAnswer.ExacerbatingOrRelievingType.Equals("Exacerbating", StringComparison.InvariantCultureIgnoreCase))
                {
                    if(suggestionQst.SymptomSuggestionAnswer.Description != string.Empty)
                        exacerbating.Add(suggestionQst.SymptomSuggestionAnswer.Description);
                }
            }
            else if (suggestionQst.SuggestionQuestionType == SuggestionQuestionType.Severity)
            {
                // Get Severity
                if(suggestionQst.SymptomSuggestionAnswer?.SeverityScale > 0)
                    severity.Add(suggestionQst.SymptomSuggestionAnswer.SeverityScale.ToString());
            }
        }

        if (sites.Count > 0)
        {
            summary += $" at {string.Join(", ", sites)} -";
        }
        if (onset.Count > 0)
        {
            var data1 = onset[0] == "null" || onset[0] == null ? " " : $" began {onset[0]} ago,";
            var data2 = cyclicality.Count <= 0 ? " " : $" {cyclicality[0]},";
            summary += $"{data1}{data2}";
        }
        if (frequency.Count > 0)
        {
            var data1 = frequency[0] == "null" || frequency[0] == null ? " " : $" present during the {frequency[0]}";
            var data2 = howLongDidItLast.Count <= 0 ? " " : $" and lasts for {howLongDidItLast[0]}"; 
            summary += $"{data1}{data2};";
        }
        if (severity.Count > 0)
        {
            var data1 = severity[0] == "null" || severity[0] == null ? " " : $" described as {severity[0]} on a 0 - 10 scale;";
            summary += $"{data1}";
        }
        if (characterPresent.Count > 0 || characterAbsent.Count > 0)
        {
            var data1 = characterPresent.Count <= 0 ? " " : $" characterized by {string.Join(", ", characterPresent)}";
            var data2 = characterAbsent.Count <= 0 ? " " : $", not described as {string.Join(", ", characterAbsent)}";
            summary += $"{data1}{data2};";
        } 
        if (radiationPresent.Count > 0 || radiationAbsent.Count > 0)
        {
            var data1 = radiationPresent.Count <= 0 ? " " : $" radiates to {string.Join(", ", radiationPresent)}";
            var data2 = radiationAbsent.Count <= 0 ? " " : $" / does not radiate to {string.Join(", ", radiationAbsent)}";
            summary += $"{data1}{data2};";
        }
        if (associationPresent.Count > 0 || associationAbsent.Count > 0)
        {
            var data1 = associationPresent.Count <= 0 ? " " : $" associated with {string.Join(", ", associationPresent)};";
            var data2 = associationAbsent.Count <= 0 ? " " : $" no history of {string.Join(", ", associationAbsent)};";
            summary += $"{data1}{data2}";
        }
        if (exacerbating.Count > 0 || relieving.Count > 0)
        {
            var data1 = exacerbating.Count <= 0 ? " " : $" is exacerbated by {string.Join(", ", exacerbating)}";
            var continuation = exacerbating.Count > 0 && relieving.Count > 0 ? " and" : "";
            var data2 = relieving.Count <= 0 ? " " : $" relieved by {string.Join(", ", relieving)}";
            summary += $"{data1}{continuation}{data2};";
        }

        // check if there is a note data
        if (symptomSummary.OtherNote is { Length: > 0 })
            summary += $" {symptomSummary.OtherNote}";

        // return the summary of the SOCRATES
        return summary;
    }
}
