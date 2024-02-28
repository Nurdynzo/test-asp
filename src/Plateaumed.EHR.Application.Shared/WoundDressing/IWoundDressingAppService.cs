using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.WoundDressing.Dtos;

namespace Plateaumed.EHR.WoundDressing;

public interface IWoundDressingAppService : IApplicationService
{ 
    Task CreateWoundDressing(CreateWoundDressingDto input);
    Task<List<WoundDressingSummaryForReturnDto>> GetPatientWoundDressing(GetWoundDressingDto woundDressingDto);
    Task DeleteCreateWoundDressing(long planItemId);
}