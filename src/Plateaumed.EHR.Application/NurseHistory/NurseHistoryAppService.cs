using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.NurseHistory.Abstractions;
using Plateaumed.EHR.NurseHistory.Dtos;

namespace Plateaumed.EHR.NurseHistory;

public class NurseHistoryAppService : INurseHistoryAppService
{
    private readonly ICreateNurseHistoryCommandHandler _createNurseHistoryCommandHandler;
    private readonly IGetNurseHistoryQueryHandler _getNurseHistoryQueryHandler;
    
    public NurseHistoryAppService(ICreateNurseHistoryCommandHandler createNurseHistoryCommandHandler, IGetNurseHistoryQueryHandler getNurseHistoryQueryHandler)
    {
        _createNurseHistoryCommandHandler = createNurseHistoryCommandHandler;
        _getNurseHistoryQueryHandler = getNurseHistoryQueryHandler;
    }

    public async Task CreateNurseHistory(CreateNurseHistoryDto input) =>
        await _createNurseHistoryCommandHandler.Handle(input);
    
    public async Task<List<NurseHistoryResponseDto>> GetNurseHistory(long patientId) 
        => await _getNurseHistoryQueryHandler.Handle(patientId); 
}