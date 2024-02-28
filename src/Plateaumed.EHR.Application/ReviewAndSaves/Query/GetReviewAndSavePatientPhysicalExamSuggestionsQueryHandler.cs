using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class GetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler : IGetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler
{
    private readonly IGetPhysicalExaminationUploadedImagesQueryHandler _repositoryPhysicalExaminaionImageQuery;



    public GetReviewAndSavePatientPhysicalExamSuggestionsQueryHandler(
        IGetPhysicalExaminationUploadedImagesQueryHandler repositoryPhysicalExaminaionImageQuery)
    {
        _repositoryPhysicalExaminaionImageQuery = repositoryPhysicalExaminaionImageQuery;
    }

    public async Task<List<PatientPhysicalExaminationDto>> Handle(List<PatientPhysicalExaminationResponseDto> physicalExamination)
    {
        var patientPhysicalExaminationResult = new List<PatientPhysicalExaminationDto>();
        var physicalExaminationSuggestion = physicalExamination == null ? null : physicalExamination.Where(x => x.PhysicalExaminationEntryType == PhysicalExaminationEntryType.Suggestion).ToList();
        foreach (var item in physicalExaminationSuggestion)
        {
            var suggestionAnswers = item.Suggestions;
            var entryDate = item.CreationTime;
            var patientPhysicalExamImages = item.ImageUploaded ? await _repositoryPhysicalExaminaionImageQuery.Handle(item.Id) : null;
            var results = suggestionAnswers.Select(s =>
            {
                var answers = s.PatientPhysicalExamSuggestionAnswers.Select(a => a.Description).ToList();
                var suggestionQuestion = new PatientPhysicalExaminationDto()
                {
                    Header = s.HeaderName,
                    Answer = String.Join(",", answers),
                    ImageFiles = patientPhysicalExamImages,
                    CreatedAt = entryDate,
                    ImageUploaded = item.ImageUploaded
                };

                return suggestionQuestion;
            }).ToList();

            patientPhysicalExaminationResult.AddRange(results);
        }

        return patientPhysicalExaminationResult;
    }
}