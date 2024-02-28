using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.BedMaking.Dtos;

namespace Plateaumed.EHR.BedMaking;

public interface IBedMakingAppService : IApplicationService
{ 
    Task CreateBedMaking(CreateBedMakingDto input);
    Task<List<PatientBedMakingSummaryForReturnDto>> GetPatientSummary(int patientId);
    Task DeleteCreateBedMaking(long bedMakingId);
}