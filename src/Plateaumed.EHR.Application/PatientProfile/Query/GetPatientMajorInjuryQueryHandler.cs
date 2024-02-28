using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.PatientProfile.Query;

public class GetPatientMajorInjuryQueryHandler : IGetPatientMajorInjuryQueryHandler
{
    private readonly IRepository<PatientMajorInjury,long> _patientMajorInjuryRepository;

    public GetPatientMajorInjuryQueryHandler(IRepository<PatientMajorInjury, long> patientMajorInjuryRepository)
    {
        _patientMajorInjuryRepository = patientMajorInjuryRepository;
    }

    public async Task<List<GetPatientMajorInjuryResponse>> Handle(long patientId)
    {
        var query = _patientMajorInjuryRepository
            .GetAll()
            .Where(v => v.PatientId == patientId)
            .Select(x=> new GetPatientMajorInjuryResponse
            {
                Id = x.Id,
                Diagnosis = x.Diagnosis,
                PeriodOfInjury = $"{x.PeriodOfInjury} {x.PeriodOfInjuryUnit} ago",
                IsOngoing = x.IsOngoing,
                IsComplicationPresent = x.IsComplicationPresent,
                Notes = x.Notes
                
            });
        return await query.ToListAsync();
    }
}