using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetAlcoholTypesSuggestionsRequestHandler : IGetAlcoholTypesSuggestionsRequestHandler
    {
        private readonly IRepository<AlcoholType, long> _alcoholTypesSuggestionRepository;

        public GetAlcoholTypesSuggestionsRequestHandler(IRepository<AlcoholType, long> alcoholTypesSuggestionRepository)
        {
            _alcoholTypesSuggestionRepository = alcoholTypesSuggestionRepository;
        }

        public async Task<List<GetAlcoholTypesResponseDto>> Handle()
        {
            return await _alcoholTypesSuggestionRepository.GetAll()
                .Select(x => new GetAlcoholTypesResponseDto
                {
                    Type = x.Type,
                    AlcoholUnit = x.AlcoholUnit,
                    Id = x.Id
                }).ToListAsync();
        }
    }
}
