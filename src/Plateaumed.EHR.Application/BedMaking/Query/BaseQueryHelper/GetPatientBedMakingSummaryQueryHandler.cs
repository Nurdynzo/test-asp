using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.BedMaking.Abstractions;
using Plateaumed.EHR.BedMaking.Dtos;

namespace Plateaumed.EHR.BedMaking.Query.BaseQueryHelper;

public class GetPatientBedMakingSummaryQueryHandler : IGetPatientBedMakingSummaryQueryHandler
{
    private readonly IBaseQuery _baseQuery;
    private readonly IObjectMapper _objectMapper;
    
    public GetPatientBedMakingSummaryQueryHandler(IBaseQuery baseQuery, IObjectMapper objectMapper)
    {
        _baseQuery = baseQuery;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<PatientBedMakingSummaryForReturnDto>> Handle(int patientId, int? tenantId)
    {
        var options = await _baseQuery.GetPatientBedMakingBaseQuery(patientId, tenantId).OrderByDescending(v => v.Id).ToListAsync();
        return  _objectMapper.Map<List<PatientBedMakingSummaryForReturnDto>>(options);
    }
}