using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetChronicDiseaseSuggestionQueryHandler : IGetChronicDiseaseSuggestionQueryHandler
{
    private readonly IRepository<ChronicDisease,long> _chronicDiseaseRepository;
    private readonly IObjectMapper _objectMapper;
    public GetChronicDiseaseSuggestionQueryHandler(
        IRepository<ChronicDisease, long> chronicDiseaseRepository,
        IObjectMapper objectMapper)
    {
        _chronicDiseaseRepository = chronicDiseaseRepository;
        _objectMapper = objectMapper;
    }

    public GetChronicDiseaseSuggestionQueryHandler(IRepository<ChronicDisease, long> chronicDiseaseRepository)
    {
        _chronicDiseaseRepository = chronicDiseaseRepository;
    }

    public async Task<List<GetChronicDiseaseSuggestionQueryResponse>> Handle()
    {
        return await _chronicDiseaseRepository
            .GetAll()
            .Select(x => _objectMapper.Map<GetChronicDiseaseSuggestionQueryResponse>(x)).ToListAsync();
    }
}