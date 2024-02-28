using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetAllergyExperiencedSuggestionQueryHandler : IGetAllergyExperiencedSuggestionQueryHandler
{
    private readonly IRepository<AllergyReactionExperiencedSuggestion, long>
        _allergyReactionExperiencedSuggestionRepository;

    public GetAllergyExperiencedSuggestionQueryHandler(IRepository<AllergyReactionExperiencedSuggestion, long> allergyReactionExperiencedSuggestionRepository)
    {
        _allergyReactionExperiencedSuggestionRepository = allergyReactionExperiencedSuggestionRepository;
    }

    public Task<List<GetAllergyExperiencedSuggestionQueryResponse>> Handle()
    {
        return _allergyReactionExperiencedSuggestionRepository.GetAll()
            .Select(x =>
                new GetAllergyExperiencedSuggestionQueryResponse()
                {
                    Id = x.Id,
                    Name = x.ReactionName,
                    SnomedId = x.SnomedId
                }).ToListAsync();

    }
}