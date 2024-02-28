using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPhysicalExaminationQueryHandler : IGetPhysicalExaminationQueryHandler
{
    private readonly IRepository<PhysicalExamination, long> _repository;
    private readonly IRepository<ExaminationQualifier, long> _qualifierRepository;
    private readonly IObjectMapper _objectMapper;

    public GetPhysicalExaminationQueryHandler(IRepository<PhysicalExamination, long> repository,
        IRepository<ExaminationQualifier, long> qualifierRepository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _qualifierRepository = qualifierRepository;
        _objectMapper = objectMapper;
    }

    public async Task<GetPhysicalExaminationResponse> Handle(long id)
    {
        var physicalExamination = await _repository.GetAsync(id) ??
                                  throw new UserFriendlyException("Physical examination not found");

        var response = _objectMapper.Map<GetPhysicalExaminationResponse>(physicalExamination);

        if (!string.IsNullOrEmpty(physicalExamination.Qualifiers))
        {
            response.Qualifiers = await _qualifierRepository.GetAll()
                .Where(x => x.SubQualifier == physicalExamination.Qualifiers)
                .Select(x => _objectMapper.Map<ExaminationQualifierDto>(x)).ToListAsync();
        }

        if (response.Plane == true)
            response.PlaneTypes = Enum.GetNames(typeof(PlaneType)).Select(name => name).ToList(); 
        
        return response;
    }
}