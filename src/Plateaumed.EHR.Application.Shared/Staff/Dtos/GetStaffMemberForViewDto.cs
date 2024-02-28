using System.Collections.Generic;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetStaffMemberForViewDto
    {
        public StaffMemberDto StaffMember { get; set; }

        public string Country { get; set; }

        public string JobTitle { get; set; }

        public string JobLevel { get; set; }

        public List<StaffAssignedFacilitiesListDto> AssignedFacilities { get; set;  } = new();
    }
}