using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPhysicalExaminationTypesQueryHandler : IGetPhysicalExaminationTypesQueryHandler
{
    private readonly IRepository<PhysicalExaminationType, long> _repository; 
    private readonly IObjectMapper _objectMapper;
    
    public GetPhysicalExaminationTypesQueryHandler(IRepository<PhysicalExaminationType, long> repository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<GetPhysicalExaminationTypeResponseDto>> Handle()
    {
        var tpyes = await _repository.GetAll().ToListAsync();
        return _objectMapper.Map<List<GetPhysicalExaminationTypeResponseDto>>(tpyes);
    }
}