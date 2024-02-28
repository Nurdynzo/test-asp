using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.NurseCarePlans.Handlers;

public class CreateNurseCareSummaryCommandHandler : ICreateNurseCareSummaryCommandHandler
{
    private readonly IRepository<NursingActivity, long> _activitiesRepository;
    private readonly IRepository<NursingCareIntervention, long> _interventionsRepository;
    private readonly IRepository<NursingDiagnosis, long> _diagnosisRepository;
    private readonly IRepository<NursingEvaluation, long> _evaluationRepository;
    private readonly IRepository<NursingOutcome, long> _outcomesRepository;
    private readonly IRepository<NursingCareSummary, long> _summaryRepository;
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<PatientEncounter, long> _encounterRepository;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IObjectMapper _mapper;

    public CreateNurseCareSummaryCommandHandler(IRepository<NursingActivity, long> activitiesRepository,
        IRepository<NursingCareIntervention, long> interventionsRepository,
        IRepository<NursingDiagnosis, long> diagnosisRepository,
        IRepository<NursingEvaluation, long> evaluationRepository,
        IRepository<NursingOutcome, long> outcomesRepository, IRepository<NursingCareSummary, long> summaryRepository,
        IRepository<Patient, long> patientRepository, IRepository<PatientEncounter, long> encounterRepository,
        IUnitOfWorkManager unitOfWork, IObjectMapper mapper)
    {
        _activitiesRepository = activitiesRepository;
        _interventionsRepository = interventionsRepository;
        _diagnosisRepository = diagnosisRepository;
        _evaluationRepository = evaluationRepository;
        _outcomesRepository = outcomesRepository;
        _summaryRepository = summaryRepository;
        _patientRepository = patientRepository;
        _encounterRepository = encounterRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateNurseCarePlanRequest request)
    {
        var patient = await _patientRepository.GetAsync(request.PatientId);
        var encounter = await _encounterRepository.GetAsync(request.EncounterId);
        var diagnosis = request.DiagnosisId.HasValue
            ? await _diagnosisRepository.GetAsync(request.DiagnosisId.Value)
            : null;
        var outcomes = await _outcomesRepository.GetAll()
            .Where(outcome => request.OutcomesId.Any(id => outcome.Id == id)).ToListAsync();
        var interventions = await _interventionsRepository.GetAll()
            .Where(intervention => request.InterventionsId.Any(id => intervention.Id == id)).ToListAsync();
        var activities = await _activitiesRepository.GetAll()
            .Where(activity => request.ActivitiesId.Any(id => activity.Id == id)).ToListAsync();
        var evaluation = request.EvaluationId.HasValue
            ? await _evaluationRepository.GetAsync(request.EvaluationId.Value)
            : null;


        var patientIntervention = new NursingCareSummary
        {
            PatientId = patient.Id,
            EncounterId = encounter.Id,
            NursingDiagnosisText = request.DiagnosisText,
            NursingEvaluationText = request.EvaluationText,

            NursingDiagnosisId = diagnosis?.Id,
            NursingEvaluationId = evaluation?.Id,
            
            NursingActivities = activities.Select(x => new PatientNursingActivity{NursingActivitiesId = x.Id})
                .Concat(request.ActivitiesText?.Select(x => new PatientNursingActivity{ NursingActivitiesText = x}) ?? Array.Empty<PatientNursingActivity>()).ToList(),
            NursingOutcomes = outcomes.Select(x => new PatientNursingOutcome{NursingOutcomeId = x.Id})
                .Concat(request.OutcomesText?.Select(x => new PatientNursingOutcome{ NursingOutcomesText = x}) ?? Array.Empty<PatientNursingOutcome>()).ToList(),
            NursingCareInterventions = interventions.Select(x => new PatientNursingCareIntervention{NursingCareInterventionsId = x.Id})
                .Concat(request.InterventionsText?.Select(x => new PatientNursingCareIntervention{ NursingCareInterventionsText = x}) ?? Array.Empty<PatientNursingCareIntervention>()).ToList(),
                
        };

        await _summaryRepository.InsertAsync(patientIntervention);
        await _unitOfWork.Current.SaveChangesAsync();
    }
}