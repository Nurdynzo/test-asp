using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Command;

public class CreatePatientMajorInjuryCommandHandler : ICreatePatientMajorInjuryCommandHandler
{
    private readonly IRepository<PatientMajorInjury,long> _patientMajorInjuryRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IAbpSession _abpSession;

    public CreatePatientMajorInjuryCommandHandler(
        IRepository<PatientMajorInjury, long> patientMajorInjuryRepository, 
        IObjectMapper objectMapper, 
        IAbpSession abpSession)
    {
        _patientMajorInjuryRepository = patientMajorInjuryRepository;
        _objectMapper = objectMapper;
        _abpSession = abpSession;
    }

    public async Task Handle(CreatePatientMajorInjuryRequest request)
    {
        var patientMajorInjury = _objectMapper.Map<PatientMajorInjury>(request);
        patientMajorInjury.TenantId = _abpSession.TenantId.GetValueOrDefault();
        await _patientMajorInjuryRepository.InsertAsync(patientMajorInjury);
    }
}