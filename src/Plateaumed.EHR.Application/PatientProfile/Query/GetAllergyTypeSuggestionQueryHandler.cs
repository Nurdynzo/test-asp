using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetAllergyTypeSuggestionQueryHandler : IGetAllergyTypeSuggestionQueryHandler
{
    private readonly IRepository<PatientAllergyTypeSuggestion,long> _patientAllergyTypeSuggestionRepository;

    public GetAllergyTypeSuggestionQueryHandler(IRepository<PatientAllergyTypeSuggestion, long> patientAllergyTypeSuggestionRepository)
    {
        _patientAllergyTypeSuggestionRepository = patientAllergyTypeSuggestionRepository;
    }

    public Task<List<GetAllergyTypeSuggestionQueryResponse>> Handle()
    {
        return _patientAllergyTypeSuggestionRepository.GetAll()
            .Select(x => 
                new GetAllergyTypeSuggestionQueryResponse()
            {
                Id = x.Id,
                Name = x.Name,
                SnomedId = x.SnomedId
            }).ToListAsync();
    }
}