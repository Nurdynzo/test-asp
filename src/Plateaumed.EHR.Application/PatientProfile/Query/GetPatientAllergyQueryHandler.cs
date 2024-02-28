using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetPatientAllergyQueryHandler : IGetPatientAllergyQueryHandler
{
    private readonly IRepository<PatientAllergy, long> _allergyRepository;
    private readonly IObjectMapper _objectMapper;

    public GetPatientAllergyQueryHandler(
        IRepository<PatientAllergy, long> allergyRepository, 
        IObjectMapper objectMapper)
    {
        _allergyRepository = allergyRepository;
        _objectMapper = objectMapper;
    }

    public async Task<List<GetPatientAllergyResponseDto>> Handle(long patientId)
    {
        var patientAllergies = _allergyRepository
            .GetAll()
            .Where(x => x.PatientId == patientId)
            .Select(x => new GetPatientAllergyResponseDto
             {
                 Id = x.Id,
                 AllergyType = x.AllergyType,
                 AllergySnomedId = x.AllergySnomedId,
                 Reaction = x.Reaction,
                 ReactionSnomedId = x.ReactionSnomedId,
                 Notes = x.Notes,
                 Severity = x.Severity,
                 PatientId = x.PatientId,
                 Interval = x.Interval,
                 CreatorUserId = x.CreatorUserId.GetValueOrDefault(),
                 CreationTime = x.CreationTime
             });
        return await patientAllergies.ToListAsync();
    }
}