using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPhysicalExaminationListQueryHandler : IGetPhysicalExaminationListQueryHandler
{
    private readonly IRepository<PhysicalExamination, long> _repository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IObjectMapper _objectMapper;

    public GetPhysicalExaminationListQueryHandler(IRepository<PhysicalExamination, long> repository,
        IRepository<Patient, long> patientRepository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _objectMapper = objectMapper;
    }

    public async Task<List<GetPhysicalExaminationListResponse>> Handle(GetPhysicalExaminationListRequest request)
    {
        var patient = await _patientRepository.GetAsync(request.PatientId) ??
                      throw new UserFriendlyException("Patient not found");

        return await _repository.GetAll().Where(x => x.Header == request.Header)
            .Where(x => !x.Gender.HasValue || x.Gender == patient.GenderType )
            .Select(x => _objectMapper.Map<GetPhysicalExaminationListResponse>(x)).ToListAsync();
    }
}