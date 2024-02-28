using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetContraceptionSuggestionQueryHandler : IGetContraceptionSuggestionQueryHandler
    {
        private readonly IRepository<TypeOfContraceptionSuggestion,long> _typeOfContraceptionSuggestionRepository;
        public GetContraceptionSuggestionQueryHandler(IRepository<TypeOfContraceptionSuggestion, long> typeOfContraceptionSuggestionRepository)
        {
            _typeOfContraceptionSuggestionRepository = typeOfContraceptionSuggestionRepository;
        }
        
        public  Task<List<GetContraceptionSuggestionQueryResponse>> Handle()
        {
            return _typeOfContraceptionSuggestionRepository
                .GetAll()
                .Select(x => new GetContraceptionSuggestionQueryResponse
                {
                    SnomedId = x.SnomedId,
                    Name = x.Name,
                    Id = x.Id
                })
                .ToListAsync();
        }

    }
}