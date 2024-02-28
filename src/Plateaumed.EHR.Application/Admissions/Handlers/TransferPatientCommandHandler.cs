using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Admissions.Handlers;

public class TransferPatientCommandHandler : ITransferPatientCommandHandler
{
    private readonly IRepository<Patient, long> _patientRepository; 
    private readonly IRepository<Ward, long> _wardRepository;
    private readonly IRepository<WardBed, long> _wardBedRepository;
    private readonly IEncounterManager _encounterManager;

    public TransferPatientCommandHandler(IRepository<Patient, long> patientRepository, 
        IRepository<Ward, long> wardRepository, IRepository<WardBed, long> wardBedRepository, 
        IEncounterManager encounterManager)
    {
        _patientRepository = patientRepository;
        _wardRepository = wardRepository;
        _wardBedRepository = wardBedRepository;
        _encounterManager = encounterManager;
    }

    public async Task Handle(TransferPatientRequest request)
    {
        await ValidateRequest(request);

        await _encounterManager.RequestTransferPatient(request.EncounterId, request.WardId, request.WardBedId, request.Status);
    }
    
    private async Task ValidateRequest(TransferPatientRequest request)
    {
        _ = await _patientRepository.GetAsync(request.PatientId);  
        _ = await _wardRepository.GetAsync(request.WardId);
        if (request.WardBedId != null) _ = await _wardBedRepository.GetAsync(request.WardBedId.Value);
        await _encounterManager.CheckEncounterExists(request.EncounterId);
    }
}