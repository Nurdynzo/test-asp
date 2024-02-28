using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetStaffMembersWithJobsRequest : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string StaffCodeFilter { get; set; }

        public long? FacilityIdFilter { get; set; }
    }
}
