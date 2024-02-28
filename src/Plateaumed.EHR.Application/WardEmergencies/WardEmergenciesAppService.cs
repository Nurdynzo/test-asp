using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies;

[AbpAuthorize(AppPermissions.Pages_WardEmergencies)]
public class WardEmergenciesAppService : EHRAppServiceBase, IWardEmergenciesAppService
{
    private readonly IGetAllWardEmergenciesQueryHandler _getAllWardEmergencies;
    private readonly IGetNursingInterventionsQueryHandler _getNursingInterventions;
    private readonly ICreatePatientInterventionCommandHandler _createPatientIntervention;
    private readonly IDeleteInterventionCommandHandler _deleteIntervention;
    private readonly IGetPatientInterventionsQueryHandler _getPatientInterventions;

    public WardEmergenciesAppService(IGetAllWardEmergenciesQueryHandler getAllWardEmergencies,
        IGetNursingInterventionsQueryHandler getNursingInterventions,
        ICreatePatientInterventionCommandHandler createPatientIntervention,
        IDeleteInterventionCommandHandler deleteIntervention,
        IGetPatientInterventionsQueryHandler getPatientInterventions)
    {
        _getAllWardEmergencies = getAllWardEmergencies;
        _getNursingInterventions = getNursingInterventions;
        _createPatientIntervention = createPatientIntervention;
        _deleteIntervention = deleteIntervention;
        _getPatientInterventions = getPatientInterventions;
    }

    public async Task<List<GetWardEmergenciesResponse>> GetAll()
    {
        return await _getAllWardEmergencies.Handle();
    }

    public async Task<List<GetNursingInterventionsResponse>> GetNursingInterventions(long wardEmergencyId)
    {
        return await _getNursingInterventions.Handle(wardEmergencyId);
    }

    public async Task CreatePatientIntervention(CreatePatientInterventionRequest request)
    {
        await _createPatientIntervention.Handle(request);
    }

    public async Task DeleteIntervention(long interventionId)
    => await _deleteIntervention.Handle(interventionId);


    public async Task<List<GetPatientInterventionsResponse>> GetPatientInterventions(GetPatientInterventionsRequest request)
    {
        return await _getPatientInterventions.Handle(request);
    }
}
