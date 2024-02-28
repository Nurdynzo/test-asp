using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetProcedureSuggestionRequestHandler : IGetProcedureSuggestionRequestHandler
    {
        private readonly IRepository<ProcedureSuggestion, long> _procedureSuggestionRepository;

        public GetProcedureSuggestionRequestHandler(IRepository<ProcedureSuggestion, long> procedureSuggestionRepository)
        {
            _procedureSuggestionRepository = procedureSuggestionRepository;
        }

        public async Task<List<GetProcedureSuggestionResponseDto>> Handle()
        {
            return await _procedureSuggestionRepository.GetAll()
                .Select(x => new GetProcedureSuggestionResponseDto
                {
                    Name = x.Name,
                    SnomedId = x.SnomedId,
                    Id = x.Id
                }).ToListAsync();
        }
    }
}
