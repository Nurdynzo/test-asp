using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetCigaretteAndTobaccoSuggestionsRequestHandler : IGetCigaretteAndTobaccoSuggestionsRequestHandler
    {
        private readonly IRepository<TobaccoSuggestion, long> _tobaccoSugesstionRepository;

        public GetCigaretteAndTobaccoSuggestionsRequestHandler(IRepository<TobaccoSuggestion, long> tobaccoSugesstionRepository)
        {
            _tobaccoSugesstionRepository = tobaccoSugesstionRepository;
        }

        public async Task<List<GetTobaccoSuggestionResponseDto>> Handle()
        {
            return await _tobaccoSugesstionRepository.GetAll()
                .Select(x => new GetTobaccoSuggestionResponseDto
                {
                    ModeOfConsumption = x.ModeOfConsumption,
                    SnomedId = x.SnomedId,
                    Id = x.Id
                }).ToListAsync();
        }
    }
}
