using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.NurseHistory.Abstractions;
using Plateaumed.EHR.NurseHistory.Dtos;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.NurseHistory.Handlers;

public class GetNurseHistoryQueryHandler : IGetNurseHistoryQueryHandler
{
    private readonly IRepository<NursingHistory, long> _nurseHistoryRepository;  
    private readonly IObjectMapper _objectMapper;

    public GetNurseHistoryQueryHandler(IRepository<NursingHistory, long> nurseHistoryRepository, IObjectMapper objectMapper)
    {
        _nurseHistoryRepository = nurseHistoryRepository;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<NurseHistoryResponseDto>> Handle(long patientId)
    { 
        return await _nurseHistoryRepository.GetAll().Where(v => v.PatientId == patientId)
            .Select(v => _objectMapper.Map<NurseHistoryResponseDto>(v))
            .ToListAsync();
    }
}