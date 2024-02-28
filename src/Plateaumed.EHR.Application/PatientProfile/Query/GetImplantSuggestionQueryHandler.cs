using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetImplantSuggestionQueryHandler : IGetImplantSuggestionQueryHandler
    {
        private readonly IRepository<PatientImplantSuggestion, long> _implantSuggestionRepository;

        public GetImplantSuggestionQueryHandler(IRepository<PatientImplantSuggestion, long> implantSuggestionRepository)
        {
            _implantSuggestionRepository = implantSuggestionRepository;
        }

        public async Task<List<GetImplantSuggestionResponse>> Handle()
        {
            return await _implantSuggestionRepository.GetAll()
                .Select(x => new GetImplantSuggestionResponse
                {
                    Name = x.Name,
                    SnomedId = x.SnomedId,
                    Id = x.Id
                }).ToListAsync();
        }
    }
}
