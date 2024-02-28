using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetVaccinationSuggestionsQueryHandler : IGetVaccinationSuggestionsQueryHandler
    {
        private readonly IRepository<VaccinationSuggestion, long> _vaccinationSuggestionsRepository;

        public GetVaccinationSuggestionsQueryHandler(IRepository<VaccinationSuggestion, long> vaccinationSuggestionRepository)
        {
            _vaccinationSuggestionsRepository = vaccinationSuggestionRepository;
        }

        public async Task<List<VaccinationSuggestionResponseDto>> Handle()
        {
            return await _vaccinationSuggestionsRepository.GetAll()
                .Select(x => new VaccinationSuggestionResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    SnomedId = x.SnomedId
                }).ToListAsync().ConfigureAwait(false);
        }
    }
}
