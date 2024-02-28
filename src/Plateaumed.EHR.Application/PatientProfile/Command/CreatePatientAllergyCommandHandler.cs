using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Command;

public class CreatePatientAllergyCommandHandler : ICreatePatientAllergyCommandHandler
{
    private readonly IRepository<PatientAllergy,long> _allergyRepository;
    private readonly IObjectMapper _objectMapper;

    public CreatePatientAllergyCommandHandler(
        IRepository<PatientAllergy, long> allergyRepository,
        IObjectMapper objectMapper)
    {
        _allergyRepository = allergyRepository;
        _objectMapper = objectMapper;
    }

    public Task Handle(CreatePatientAllergyCommandRequest request)
    {
        var allergy = _objectMapper.Map<PatientAllergy>(request);
        return _allergyRepository.InsertAsync(allergy);
    }
}
