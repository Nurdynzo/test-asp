using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans;

[AbpAuthorize(AppPermissions.Pages_WardEmergencies)]
public class NurseCarePlansAppService : EHRAppServiceBase, INurseCarePlansAppService
{
    private readonly IGetAllNursingDiagnosisQueryHandler _nursingDiagnosisQueryHandler;
    private readonly IGetAllNursingOutcomesQueryHandler _nursingOutcomesQueryHandler;
    private readonly IGetAllNursingCareInterventionsQueryHandler _interventionsQueryHandler;
    private readonly IGetAllNursingActivitiesQueryHandler _activitiesQueryHandler;
    private readonly IGetAllNursingEvaluationsQueryHandler _evaluationsQueryHandler;
    private readonly IGetNurseCareSummaryQueryHandler _summaryQueryHandler;
    private readonly ICreateNurseCareSummaryCommandHandler _commandHandler;


    public NurseCarePlansAppService(IGetAllNursingDiagnosisQueryHandler nursingDiagnosisQueryHandler,
        IGetAllNursingOutcomesQueryHandler nursingOutcomesQueryHandler,
        IGetAllNursingCareInterventionsQueryHandler interventionsQueryHandler,
        IGetAllNursingActivitiesQueryHandler activitiesQueryHandler,
        IGetAllNursingEvaluationsQueryHandler evaluationsQueryHandler,
        IGetNurseCareSummaryQueryHandler summaryQueryHandler, ICreateNurseCareSummaryCommandHandler commandHandler)
    {
        _nursingDiagnosisQueryHandler = nursingDiagnosisQueryHandler;
        _nursingOutcomesQueryHandler = nursingOutcomesQueryHandler;
        _interventionsQueryHandler = interventionsQueryHandler;
        _activitiesQueryHandler = activitiesQueryHandler;
        _evaluationsQueryHandler = evaluationsQueryHandler;
        _summaryQueryHandler = summaryQueryHandler;
        _commandHandler = commandHandler;
    }

    public Task<List<GetNurseCareSummaryResponse>> GetAll(GetNurseCareRequest request)
    {
        return _summaryQueryHandler.Handle(request);
    }

    public Task<List<GetNurseCarePlansResponse>> GetNursingDiagnosis()
    {
        return _nursingDiagnosisQueryHandler.Handle();
    }

    public Task<List<GetNurseCarePlansResponse>> GetNursingOutcomes(long diagnosisId)
    {
        return _nursingOutcomesQueryHandler.Handle(diagnosisId);
    }

    public Task<List<GetNurseCarePlansResponse>> GetNursingCareInterventions(List<long> outcomesIds)
    {
        return _interventionsQueryHandler.Handle(outcomesIds);
    }

    public Task<List<GetNurseCarePlansResponse>> GetNursingActivities(List<long> careInterventionIds)
    {
        return _activitiesQueryHandler.Handle(careInterventionIds);
    }

    public Task<List<GetNurseCarePlansResponse>> GetNursingEvaluation()
    {
        return _evaluationsQueryHandler.Handle();
    }

    public Task CreateNurseCarePlan(CreateNurseCarePlanRequest request)
    {
        return _commandHandler.Handle(request);
    }
}