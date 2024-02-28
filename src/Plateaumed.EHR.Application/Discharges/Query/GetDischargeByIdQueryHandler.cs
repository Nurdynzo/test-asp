using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Query;

public class GetDischargeByIdQueryHandler : IGetDischargeByIdQueryHandler
{
    private readonly IDischargeBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;

    public GetDischargeByIdQueryHandler(IDischargeBaseQuery baseQuery, IAbpSession abpSession)
    {
        _baseQuery = baseQuery;
        _abpSession = abpSession;
    }

    public async Task<DischargeDto> Handle(int dischargeId)
    {        
        if (dischargeId == 0)
            throw new UserFriendlyException("Discharge Id is required.");

        var discharge = await _baseQuery.GetDischargeInformation(dischargeId);
        if (discharge != null)
        {
            discharge.Prescriptions = await _baseQuery.GetDischargeMedications(dischargeId).ToListAsync();
            discharge.PlanItems = await _baseQuery.GetDischargePlanItem(dischargeId).ToListAsync();
        }
        return discharge;
    }
}