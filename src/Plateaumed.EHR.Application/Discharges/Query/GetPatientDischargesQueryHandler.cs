using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Query;

public class GetPatientDischargeQueryHandler : IGetPatientDischargeQueryHandler
{
    private readonly IDischargeBaseQuery _baseQuery;
    private readonly IAbpSession _abpSession;

    public GetPatientDischargeQueryHandler(IDischargeBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<List<DischargeDto>> Handle(long patientId)
    {
        if (patientId == 0)
            throw new UserFriendlyException("Patient Id is required.");

        var discharges = await _baseQuery.GetPatientDischarges(patientId);
        
        if (discharges != null && discharges.Count > 0)
        {
            foreach (var item in discharges)
            {
                item.Prescriptions = await _baseQuery.GetDischargeMedications(item.Id).ToListAsync();
                item.PlanItems = await _baseQuery.GetDischargePlanItem(item.Id).ToListAsync();
            }
        }
        return discharges;
    }
}