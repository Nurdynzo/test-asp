using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Command;

public class DeletePatientMajorInjuryCommandHandler : IDeletePatientMajorInjuryCommandHandler
{
    private readonly IRepository<PatientMajorInjury, long> _patientMajorInjuryRepository;

    public DeletePatientMajorInjuryCommandHandler(
        IRepository<PatientMajorInjury, long> patientMajorInjuryRepository)
    {
        _patientMajorInjuryRepository = patientMajorInjuryRepository;
    }

    public  Task Handle(long id)
    {
        return _patientMajorInjuryRepository.DeleteAsync(id);
    }
}