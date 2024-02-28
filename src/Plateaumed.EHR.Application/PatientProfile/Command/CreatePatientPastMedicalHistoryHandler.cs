using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Command;

public class CreatePatientPastMedicalHistoryHandler : ICreatePatientPastMedicalHistoryHandler
{
    private readonly IRepository<PatientPastMedicalCondition,long> _patientPastMedicalConditionRepository;
    private readonly IObjectMapper _objectMapper;

    public CreatePatientPastMedicalHistoryHandler(
        IRepository<PatientPastMedicalCondition, long> patientPastMedicalConditionRepository,
        IObjectMapper objectMapper)
    {
        _patientPastMedicalConditionRepository = patientPastMedicalConditionRepository;
        _objectMapper = objectMapper;
    }
    public async Task Handle(PatientPastMedicalConditionCommandRequest request)
    {
        var pastMedicalHistory = _objectMapper.Map<PatientPastMedicalCondition>(request);
        await _patientPastMedicalConditionRepository.InsertAsync(pastMedicalHistory).ConfigureAwait(false);
    }
}
