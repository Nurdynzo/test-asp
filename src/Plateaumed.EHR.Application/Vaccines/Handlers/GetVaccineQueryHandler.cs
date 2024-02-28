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

public class GetVaccineQueryHandler : IGetVaccineQueryHandler
{
    private readonly IRepository<Vaccine, long> _vaccineRepository;
    private readonly IObjectMapper _objectMapper;

    public GetVaccineQueryHandler(IRepository<Vaccine, long> vaccineRepository, IObjectMapper objectMapper)
    {
        _vaccineRepository = vaccineRepository;
        _objectMapper = objectMapper;
    }

    public async Task<GetVaccineResponse> Handle(EntityDto<long> request)
    {
        var vaccine = await _vaccineRepository.GetAll().Include(x => x.Schedules)
            .FirstOrDefaultAsync(x => x.Id == request.Id) 
                      ?? throw new UserFriendlyException("Vaccine not found");

        return _objectMapper.Map<GetVaccineResponse>(vaccine);
    }
}