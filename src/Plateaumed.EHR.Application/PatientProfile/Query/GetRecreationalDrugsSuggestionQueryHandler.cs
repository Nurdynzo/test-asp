using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetRecreationalDrugsSuggestionQueryHandler : IGetRecreationalDrugsSuggestionQueryHandler
    {
        private readonly IRepository<RecreationalDrugSuggestion, long> _recreationalDrugsSuggestionRepository;

        public GetRecreationalDrugsSuggestionQueryHandler(IRepository<RecreationalDrugSuggestion, long> recreationalDrugsSuggestionRepository)
        {
            _recreationalDrugsSuggestionRepository = recreationalDrugsSuggestionRepository;
        }

        public async Task<List<GetRecreationalDrugsSuggesionResponseDto>> Handle()
        {
            return await _recreationalDrugsSuggestionRepository.GetAll()
                .Select(x => new GetRecreationalDrugsSuggesionResponseDto
                {
                    Name = x.Name,
                    SnomedId = x.SnomedId,
                    Id = x.Id
                }).ToListAsync();
        }
    }
}
