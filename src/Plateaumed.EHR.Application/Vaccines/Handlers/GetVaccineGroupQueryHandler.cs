using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers;

public class GetVaccineGroupQueryHandler : IGetVaccineGroupQueryHandler
{
    private readonly IRepository<VaccineGroup, long> _vaccineGroupRepository;
    private readonly IObjectMapper _objectMapper;

    public GetVaccineGroupQueryHandler(IRepository<VaccineGroup, long> vaccineGroupRepository, IObjectMapper objectMapper)
    {
        _vaccineGroupRepository = vaccineGroupRepository;
        _objectMapper = objectMapper;
    }

    public async Task<GetVaccineGroupResponse> Handle(EntityDto<long> request)
    {
        var vaccineGroup = await _vaccineGroupRepository.GetAll().Include(x => x.Vaccines)
                          .ThenInclude(x => x.Schedules)
                          .FirstOrDefaultAsync(x => x.Id == request.Id) 
                      ?? throw new UserFriendlyException("Vaccine group not found");
        
        return _objectMapper.Map<GetVaccineGroupResponse>(vaccineGroup);
    }
}