using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.WardEmergencies.Dto;

namespace Plateaumed.EHR.WardEmergencies;

public interface IWardEmergenciesAppService
{
    Task<List<GetWardEmergenciesResponse>> GetAll();
    Task<List<GetNursingInterventionsResponse>> GetNursingInterventions(long wardEmergencyId);
    Task CreatePatientIntervention(CreatePatientInterventionRequest request);
    Task DeleteIntervention(long interventionId);
}
