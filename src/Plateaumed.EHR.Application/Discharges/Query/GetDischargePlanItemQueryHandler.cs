using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Patients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges.Query;

public class GetDischargePlanItemsQueryHandler : IGetDischargePlanItemsQueryHandler
{
    private readonly IDischargeBaseQuery _baseQuery;

    public GetDischargePlanItemsQueryHandler(IDischargeBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<List<DischargePlanItemDto>> Handle(long dischargeId)
    {
        if (dischargeId == 0)
            throw new UserFriendlyException("Discharge Id is required.");

        var planItems = await _baseQuery.GetDischargePlanItem(dischargeId).ToListAsync();

        return planItems;
    }
}
