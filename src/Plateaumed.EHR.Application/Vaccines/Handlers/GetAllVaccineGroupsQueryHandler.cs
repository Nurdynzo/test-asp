using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers;

public class GetAllVaccineGroupsQueryHandler : IGetAllVaccineGroupsQueryHandler
{
    private readonly IRepository<VaccineGroup, long> _vaccineGroupRepository;
    private readonly IObjectMapper _objectMapper;

    public GetAllVaccineGroupsQueryHandler(IRepository<VaccineGroup, long> vaccineGroupRepository, IObjectMapper objectMapper)
    {
        _vaccineGroupRepository = vaccineGroupRepository;
        _objectMapper = objectMapper;
    }

    public async Task<List<GetAllVaccineGroupsResponse>> Handle()
    {
        return await _vaccineGroupRepository.GetAll()
            .Select(x => _objectMapper.Map<GetAllVaccineGroupsResponse>(x))
            .ToListAsync();
    }
}