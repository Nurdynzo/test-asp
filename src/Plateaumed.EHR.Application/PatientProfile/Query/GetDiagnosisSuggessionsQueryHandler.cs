using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetDiagnosisSuggessionsQueryHandler : IGetDiagnosisSuggessionsQueryHandler
    {
        private readonly IRepository<DiagnosisSuggestion, long> _diagnosisSuggessionRepository;

        public GetDiagnosisSuggessionsQueryHandler(IRepository<DiagnosisSuggestion, long> diagnosisSuggessionRepository)
        {
            _diagnosisSuggessionRepository = diagnosisSuggessionRepository;
        }

        public async Task<List<GetDiagnosisSuggestionResponseDto>> Handle()
        {
            return await _diagnosisSuggessionRepository.GetAll()
                .Select(x => new GetDiagnosisSuggestionResponseDto
                {
                    Name = x.Name,
                    SnomedId = x.SnomedId,
                    Id = x.Id
                }).ToListAsync();
        }
    }
}
