using System.Threading.Tasks;
using System.Collections.Generic;
using Plateaumed.EHR.Staff.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.ReferAndConsults.Abstraction;

namespace Plateaumed.EHR.ReferAndConsults.Handlers;

public class GetHospitalConsultantQueryHandler : IGetHospitalConsultantQueryHandler
{
    private readonly IGetStaffMemberByFacilityIdQueryHandler _staffFacilityQueryHandler;

    public GetHospitalConsultantQueryHandler(IGetStaffMemberByFacilityIdQueryHandler staffFacilityQueryHandler
        )
    {
        _staffFacilityQueryHandler = staffFacilityQueryHandler;
    }

    public async Task<List<FacilityStaffDto>> Handle(long facilityId)
    {
        var staffMembers = await _staffFacilityQueryHandler.Handle(facilityId) ?? new List<FacilityStaffDto>();
        return staffMembers;
    }

}