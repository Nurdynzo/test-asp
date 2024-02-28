using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns;

public interface IVitalSignsAppService
{
    Task<List<GetAllVitalSignsResponse>> GetAll();
    Task<List<GetGCSScoringResponse>> GetGCSScoring(long patientId);
    Task<List<GetApgarScoringResponse>> GetApgarScoring();
}