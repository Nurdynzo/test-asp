using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Command;

public class DeletePatientChronicConditionsCommandHandler : IDeletePatientChronicConditionsCommandHandler
{
    private readonly IRepository<PatientPastMedicalCondition, long> _pastMedicationConditionRepository;

    public DeletePatientChronicConditionsCommandHandler(IRepository<PatientPastMedicalCondition, long> pastMedicationConditionRepository)
    {
        _pastMedicationConditionRepository = pastMedicationConditionRepository;
    }

    public Task Handle(long id)
    {
        return _pastMedicationConditionRepository.DeleteAsync(id);
    }
}