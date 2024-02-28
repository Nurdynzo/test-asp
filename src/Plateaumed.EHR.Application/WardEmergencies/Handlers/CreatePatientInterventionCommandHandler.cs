using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies.Handlers;

public class CreatePatientInterventionCommandHandler : ICreatePatientInterventionCommandHandler
{
    private readonly IRepository<PatientIntervention, long> _interventionRepository;
    private readonly IRepository<NursingIntervention, long> _nursingInterventionRepository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<PatientEncounter, long> _encounterRepository;
    private readonly IRepository<WardEmergency, long> _wardEmergency;
    private readonly IUnitOfWorkManager _unitOfWork;

    public CreatePatientInterventionCommandHandler(IRepository<PatientIntervention, long> interventionRepository,
        IRepository<NursingIntervention, long> nursingInterventionRepository,
        IRepository<Patient, long> patientRepository, 
        IRepository<PatientEncounter, long> encounterRepository,
        IRepository<WardEmergency, long> wardEmergency,
        IUnitOfWorkManager unitOfWork)
    {
        _interventionRepository = interventionRepository;
        _nursingInterventionRepository = nursingInterventionRepository;
        _patientRepository = patientRepository;
        _encounterRepository = encounterRepository;
        _wardEmergency = wardEmergency;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreatePatientInterventionRequest request)
    {
        var patient = await _patientRepository.GetAsync(request.PatientId);
        var encounter = await _encounterRepository.GetAsync(request.EncounterId);
        var wardEmergency = request.EventId.HasValue ? await _wardEmergency.GetAsync(request.EventId.Value) : null;
        var interventions = await _nursingInterventionRepository.GetAll()
            .Where(x => request.InterventionIds.Contains(x.Id)).ToListAsync();

        var patientIntervention = new PatientIntervention
        {
            PatientId = patient.Id,
            EncounterId = encounter.Id,
            EventText = request.EventText,
            EventId = wardEmergency?.Id,
            InterventionEvents = interventions.Select(x => new InterventionEvent
            {
                NursingInterventionId = x.Id,
            }).Concat(request.InterventionTexts?.Select(x => new InterventionEvent
            {
                InterventionText = x,
            }) ?? new List<InterventionEvent>()).ToList()
        };

        await _interventionRepository.InsertAsync(patientIntervention);
        await _unitOfWork.Current.SaveChangesAsync();
    }
}