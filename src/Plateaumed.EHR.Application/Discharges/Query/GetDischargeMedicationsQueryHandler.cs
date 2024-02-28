using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Discharges.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Discharges.Query;

public class GetDischargeMedicationsQueryHandler : IGetDischargeMedicationsQueryHandler
{
    private readonly IDischargeBaseQuery _baseQuery;

    public GetDischargeMedicationsQueryHandler(IDischargeBaseQuery baseQuery)
    {
        _baseQuery = baseQuery;
    }

    public async Task<List<PatientMedicationForReturnDto>> Handle(long dischargeId)
    {
        var medications = dischargeId == 0 ?
            null :
            await _baseQuery.GetDischargeMedications(dischargeId).ToListAsync();
        return medications;
    }
}
