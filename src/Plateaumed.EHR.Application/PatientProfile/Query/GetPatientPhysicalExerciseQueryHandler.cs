using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetPatientPhysicalExerciseQueryHandler : IGetPatientPhysicalExerciseQueryHandler
{
    private readonly IRepository<PatientPhysicalExercise, long> _patientPhysicalExerciseRepository;
    private readonly IRepository<User,long> _userRepository;

    public GetPatientPhysicalExerciseQueryHandler(
        IRepository<PatientPhysicalExercise, long> patientPhysicalExerciseRepository, IRepository<User, long> userRepository)
    {
        _patientPhysicalExerciseRepository = patientPhysicalExerciseRepository;
        _userRepository = userRepository;
    }

    public async Task<GetPatientPhysicalExerciseQueryResponse> Handle(long patientId)
    {
        var query = from p in _patientPhysicalExerciseRepository.GetAll()
                    join cu in _userRepository.GetAll() on p.CreatorUserId equals cu.Id into uJoin
                    from cu in uJoin.DefaultIfEmpty()
                    join uu in _userRepository.GetAll() on p.LastModifierUserId equals uu.Id into uuJoin
                    from uu in uuJoin.DefaultIfEmpty()
                    where p.PatientId == patientId
                    select new GetPatientPhysicalExerciseQueryResponse
                    {
                        FrequencyPerWeek = p.FrequencyPerWeek,
                        DurationPerMinute = p.DurationPerMinute,
                        Intensity = p.Intensity,
                        PatientId = p.PatientId,
                        LastModifiedDate = p.LastModificationTime ?? p.CreationTime,
                        LastModifiedBy = uu == null ? $"{cu.Title} {cu.Name} {cu.Surname}" : $"{uu.Title} {uu.Name} {uu.Surname}"
                    };
            return await query.FirstOrDefaultAsync();
    }
}