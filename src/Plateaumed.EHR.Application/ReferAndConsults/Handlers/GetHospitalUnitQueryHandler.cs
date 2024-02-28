using System.Threading.Tasks;
using System.Collections.Generic;
using Plateaumed.EHR.Organizations.Abstractions;
using Plateaumed.EHR.Organizations.Dto;
using Abp.Runtime.Session;
using System.Linq;
using Plateaumed.EHR.ReferAndConsults.Abstraction;

namespace Plateaumed.EHR.ReferAndConsults.Handlers;

public class GetHospitalUnitQueryHandler : IGetHospitalUnitQueryHandler
{
    private readonly IGetOrganizationUnitsQueryHandler _orgUnitQueryHandler;
    private readonly IAbpSession _abpSession;

    public GetHospitalUnitQueryHandler(IGetOrganizationUnitsQueryHandler orgUnitQueryHandler,
            IAbpSession abpSession
        )
    {
        _orgUnitQueryHandler = orgUnitQueryHandler;
        _abpSession = abpSession;
    }

    public async Task<List<OrganizationUnitDto>> Handle(long facilityId)
    {
        var input = new GetOrganizationUnitsInput()
        {
            FacilityId = facilityId
        };
        var units = await _orgUnitQueryHandler.Handle(input, _abpSession.TenantId.GetValueOrDefault()) ?? null;
        var hospitalUnits = units != null ? units.Items.ToList() : new List<OrganizationUnitDto>();
        return hospitalUnits;
    }

}
