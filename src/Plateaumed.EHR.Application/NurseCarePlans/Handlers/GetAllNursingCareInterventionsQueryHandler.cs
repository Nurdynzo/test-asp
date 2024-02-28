using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans.Handlers;

public class GetAllNursingCareInterventionsQueryHandler : IGetAllNursingCareInterventionsQueryHandler
{
    private readonly IRepository<NursingOutcome, long> _repository;

    public GetAllNursingCareInterventionsQueryHandler(IRepository<NursingOutcome, long> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetNurseCarePlansResponse>> Handle(List<long> outcomesIds)
    {
        return await _repository.GetAll()
            .Include(x => x.Interventions)
            .Where(x => outcomesIds.Any( p => p == x.Id))
            .SelectMany(x => x.Interventions).Select(x => new GetNurseCarePlansResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
    }
}