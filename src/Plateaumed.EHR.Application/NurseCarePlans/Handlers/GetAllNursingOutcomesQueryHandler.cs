using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans.Handlers;

public class GetAllNursingOutcomesQueryHandler : IGetAllNursingOutcomesQueryHandler
{
    private readonly IRepository<NursingDiagnosis, long> _repository;

    public GetAllNursingOutcomesQueryHandler(IRepository<NursingDiagnosis, long> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetNurseCarePlansResponse>> Handle(long diagnosisId)
    {
        return await _repository.GetAll()
            .Include(x => x.Outcomes)
            .Where(x => x.Id == diagnosisId)
            .SelectMany(x => x.Outcomes).Select(x => new GetNurseCarePlansResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
    }
}