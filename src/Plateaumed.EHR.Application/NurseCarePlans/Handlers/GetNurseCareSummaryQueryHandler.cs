using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans.Handlers;

public class GetNurseCareSummaryQueryHandler : IGetNurseCareSummaryQueryHandler
{
    private readonly IRepository<NursingCareSummary, long> _nursingSummaryRepository;
    private readonly IRepository<NursingOutcome, long> _nursingOutcomes;
    private readonly IRepository<NursingActivity, long> _nursingActivities;
    private readonly IRepository<NursingCareIntervention, long> _nursingInterventions;

    public GetNurseCareSummaryQueryHandler(IRepository<NursingCareSummary, long> nursingSummaryRepository,
        IRepository<NursingOutcome, long> nursingOutcomes, IRepository<NursingActivity, long> nursingActivities,
        IRepository<NursingCareIntervention, long> nursingInterventions)
    {
        _nursingSummaryRepository = nursingSummaryRepository;
        _nursingOutcomes = nursingOutcomes;
        _nursingActivities = nursingActivities;
        _nursingInterventions = nursingInterventions;
    }

    public async Task<List<GetNurseCareSummaryResponse>> Handle(GetNurseCareRequest request)
    {
        var carePlan = await _nursingSummaryRepository.GetAll()
            .Include(x => x.NursingDiagnosis)
            .Include(x => x.NursingEvaluation)
            .Include(x => x.NursingActivities)
            .ThenInclude(x => x.NursingActivity)
            .Include(x => x.NursingCareInterventions)
            .ThenInclude(x => x.NursingCareIntervention)
            .Include(x => x.NursingOutcomes)
            .ThenInclude(x => x.NursingOutcome)
            .Where(x => x.PatientId == request.PatientId && x.EncounterId == request.EncounterId)
            .ToListAsync();


        return carePlan
            .OrderByDescending(v => v.CreationTime)
            .Select(x => new GetNurseCareSummaryResponse
            {
                Id = x.Id,
                Time = x.CreationTime,
                Diagnosis = x.NursingDiagnosisId.HasValue ? x.NursingDiagnosis.Name : x.NursingDiagnosisText,
                Evaluation = x.NursingEvaluationId.HasValue ? x.NursingEvaluation.Name : x.NursingEvaluationText,
                Activities = x.NursingActivities.Select(p => p.NursingActivitiesText ?? p.NursingActivity.Name).ToList(),
                Interventions = x.NursingCareInterventions.Select(p => p.NursingCareInterventionsText ?? p.NursingCareIntervention.Name).ToList(),
                Outcomes = x.NursingOutcomes.Select(p => p.NursingOutcomesText ?? p.NursingOutcome.Name).ToList(),
            }).ToList();
    }
}