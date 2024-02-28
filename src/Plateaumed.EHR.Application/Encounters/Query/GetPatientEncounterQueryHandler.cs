using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Encounters.Abstractions;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.IntakeOutputs.Abstractions;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Encounters.Query;

public class GetPatientEncounterQueryHandler : IGetPatientEncounterQueryHandler
{
    private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;

    public GetPatientEncounterQueryHandler(IRepository<PatientEncounter, long> patientEncounterRepository)
    {
        _patientEncounterRepository = patientEncounterRepository;
    }

    public async Task<PatientEncounter> Handle(long encounterId)
    {
        var patientEncounter = await _patientEncounterRepository
        .GetAll()
        .Include(s => s.Patient)
        .Include(s => s.Unit)
        .Include(s => s.StaffEncounters)
        .FirstOrDefaultAsync(encounter => encounter.Id == encounterId) ??
        throw new UserFriendlyException($"No patient encounter found.");

        return patientEncounter;
    }
}