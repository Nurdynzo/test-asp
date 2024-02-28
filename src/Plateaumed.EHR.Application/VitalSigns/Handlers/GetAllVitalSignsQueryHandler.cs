using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class GetAllVitalSignsQueryHandler : IGetAllVitalSignsQueryHandler
{
    private readonly IRepository<VitalSign, long> _repository;
    private readonly IObjectMapper _objectMapper;

    public GetAllVitalSignsQueryHandler(IRepository<VitalSign, long> repository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _objectMapper = objectMapper;
    }

    public async Task<List<GetAllVitalSignsResponse>> Handle()
    {
        var vitalSigns = await _repository.GetAll()
            .Include(x => x.Ranges)
            .Include(x => x.Sites)
            .ToListAsync();
        return _objectMapper.Map<List<GetAllVitalSignsResponse>>(vitalSigns);
    }
}