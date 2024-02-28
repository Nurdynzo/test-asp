using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.WoundDressing.Abstractions;
using Plateaumed.EHR.WoundDressing.Dtos;

namespace Plateaumed.EHR.WoundDressing.Query.BaseQueryHelper;

public class GetPatientWoundDressingSummaryQueryHandler : IGetPatientWoundDressingSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetPatientWoundDressingSummaryQueryHandler(IBaseQuery baseQuery, IObjectMapper objectMapper)
    {
        _baseQuery = baseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<WoundDressingSummaryForReturnDto>> Handle(int patientId)
    {
        var options = await _baseQuery.GetPatientWoundDressingBaseQuery(patientId).OrderByDescending(v => v.Id).ToListAsync();
        return  _objectMapper.Map<List<WoundDressingSummaryForReturnDto>>(options);
    }
}