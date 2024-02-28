using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans.Handlers;

public class GetAllNursingDiagnosisQueryHandler : IGetAllNursingDiagnosisQueryHandler
{
    private readonly IRepository<NursingDiagnosis, long> _repository;

    public GetAllNursingDiagnosisQueryHandler(IRepository<NursingDiagnosis, long> repository)
    {
        _repository = repository;
    }


    public async Task<List<GetNurseCarePlansResponse>> Handle()
    {
        return await _repository.GetAll().Select(x => new GetNurseCarePlansResponse
        {
            Id = x.Id,
            Name = x.Name,
        }).ToListAsync();
    }
}