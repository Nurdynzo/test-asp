using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.NurseHistory.Dtos;

namespace Plateaumed.EHR.NurseHistory;

public interface INurseHistoryAppService : IApplicationService
{
    Task CreateNurseHistory(CreateNurseHistoryDto input);

    Task<List<NurseHistoryResponseDto>> GetNurseHistory(long patientId);
}