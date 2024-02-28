using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Command;

public class CreatePhysicalExerciseCommandHandler : ICreatePhysicalExerciseCommandHandler
{
    private readonly IRepository<PatientPhysicalExercise,long> _patientPhysicalExerciseRepository;
    private readonly IObjectMapper _objectMapper;

    public CreatePhysicalExerciseCommandHandler(
        IRepository<PatientPhysicalExercise, long> patientPhysicalExerciseRepository, 
        IObjectMapper objectMapper)
    {
        _patientPhysicalExerciseRepository = patientPhysicalExerciseRepository;
        _objectMapper = objectMapper;
    }

    /// <summary>
    /// Create new Physical Exercise if not exists otherwise update
    /// </summary>
    /// <param name="request"></param>
    public async Task Handle(CreatePhysicalExerciseCommandRequest request)
    {
        var exists = await _patientPhysicalExerciseRepository.FirstOrDefaultAsync(x=>x.PatientId == request.PatientId);
        if (exists != null)
        {
           exists.Intensity = request.Intensity;
           exists.DurationPerMinute = request.DurationPerMinute;
           exists.FrequencyPerWeek = request.FrequencyPerWeek;
           await _patientPhysicalExerciseRepository.UpdateAsync(exists);
           return;
        }
        var physicalExercise = _objectMapper.Map<PatientPhysicalExercise>(request);
        await _patientPhysicalExerciseRepository.InsertAsync(physicalExercise);
    }
}