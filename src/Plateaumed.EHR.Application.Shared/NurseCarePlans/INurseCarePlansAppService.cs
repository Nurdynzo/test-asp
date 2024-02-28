using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans;

public interface INurseCarePlansAppService
{
    Task<List<GetNurseCareSummaryResponse>> GetAll(GetNurseCareRequest request);
    Task<List<GetNurseCarePlansResponse>> GetNursingDiagnosis();
    Task<List<GetNurseCarePlansResponse>> GetNursingOutcomes(long diagnosisId);
    Task<List<GetNurseCarePlansResponse>> GetNursingCareInterventions(List<long> outcomesIds);
    Task<List<GetNurseCarePlansResponse>> GetNursingActivities(List<long> careInterventionIds);
    Task<List<GetNurseCarePlansResponse>> GetNursingEvaluation();
    Task CreateNurseCarePlan(CreateNurseCarePlanRequest request);
}