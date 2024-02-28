using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies.Handlers;

public class GetNursingInterventionsQueryHandler : IGetNursingInterventionsQueryHandler
{
    private readonly IRepository<WardEmergency, long> _repository;

    public GetNursingInterventionsQueryHandler(IRepository<WardEmergency, long> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetNursingInterventionsResponse>> Handle(long wardEmergencyId)
    {
        return await _repository.GetAll()
            .Include(x => x.Interventions)
            .Where(x => x.Id == wardEmergencyId)
            .SelectMany(x => x.Interventions).Select(x => new GetNursingInterventionsResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
    }
}