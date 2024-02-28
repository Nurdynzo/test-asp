using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies.Handlers;

public class GetAllWardEmergenciesQueryHandler : IGetAllWardEmergenciesQueryHandler
{
    private readonly IRepository<WardEmergency, long> _repository;

    public GetAllWardEmergenciesQueryHandler(IRepository<WardEmergency, long> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetWardEmergenciesResponse>> Handle()
    {
        return await _repository.GetAll().Select(x => new GetWardEmergenciesResponse
        {
            Id = x.Id,
            Event = x.Event,
            ShortName = x.ShortName
        }).ToListAsync();
    }
}