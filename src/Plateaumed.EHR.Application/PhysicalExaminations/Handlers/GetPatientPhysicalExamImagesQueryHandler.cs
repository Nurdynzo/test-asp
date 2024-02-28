using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPatientPhysicalExamImagesQueryHandler : IGetPatientPhysicalExamImagesQueryHandler
{
    private readonly IRepository<PatientPhysicalExaminationImageFile, long> _repository;
    private readonly IObjectMapper _objectMapper;

    public GetPatientPhysicalExamImagesQueryHandler(IRepository<PatientPhysicalExaminationImageFile, long> repository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _objectMapper = objectMapper;
    }

    public async Task<List<PatientPhysicalExaminationImageFileResponseDto>> Handle(long patientPhysicalExaminationId)
    {
        return await _repository.GetAll()
            .Where(x => x.PatientPhysicalExaminationId == patientPhysicalExaminationId && x.IsDeleted == false)
            .OrderBy(v => v.CreationTime)
            .Select(v => _objectMapper.Map<PatientPhysicalExaminationImageFileResponseDto>(v))
            .ToListAsync();
    }
}