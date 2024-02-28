using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class GetApgarScoringQueryHandler : IGetApgarScoringQueryHandler
{
    private readonly IRepository<ApgarScoring, long> _repository;
    private readonly IObjectMapper _objectMapper;

    public GetApgarScoringQueryHandler(IRepository<ApgarScoring, long> repository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _objectMapper = objectMapper;
    }

    public async Task<List<GetApgarScoringResponse>> Handle()
    {
        var apgarScorings = await _repository.GetAll()
            .Include(x => x.Ranges)
            .ToListAsync();
        return _objectMapper.Map<List<GetApgarScoringResponse>>(apgarScorings);
    }
}