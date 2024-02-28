using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans.Handlers;

public class GetAllNursingActivitiesQueryHandler : IGetAllNursingActivitiesQueryHandler
{
    private readonly IRepository<NursingCareIntervention, long> _repository;

    public GetAllNursingActivitiesQueryHandler(IRepository<NursingCareIntervention, long> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetNurseCarePlansResponse>> Handle(List<long> careInterventionIds)
    {
        return await _repository.GetAll()
            .Include(x => x.Activities)
            .Where(x => careInterventionIds.Any( p => p == x.Id))
            .SelectMany(x => x.Activities).Select(x => new GetNurseCarePlansResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
    }
}