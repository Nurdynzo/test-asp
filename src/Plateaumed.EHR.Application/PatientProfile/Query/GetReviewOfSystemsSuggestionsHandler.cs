using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetReviewOfSystemsSuggestionsHandler : IGetReviewOfSystemsSuggestionsHandler
    {
        private readonly IRepository<ReviewOfSystemsSuggestion, long> _reviewOfSystemsSuggestionRepository;

        public GetReviewOfSystemsSuggestionsHandler(IRepository<ReviewOfSystemsSuggestion, long> reviewOfSystemsSuggestionRepositor)
        {
            _reviewOfSystemsSuggestionRepository = reviewOfSystemsSuggestionRepositor;
        }

        public async Task<List<ReviewOfSystemsSuggestionResponseDto>> Handle(SymptomsCategory category)
        {
            var response = await _reviewOfSystemsSuggestionRepository.GetAll()
                .Where(x => x.Category == category)
                .Select(i => new ReviewOfSystemsSuggestionResponseDto
                {
                    Name = i.Name,
                    SnomedId = i.SnomedId,
                    Id = i.Id
                }).ToListAsync();
            return response;
        }
    }
}
