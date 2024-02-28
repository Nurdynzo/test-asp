using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetUserFacilityViewDto
    {
        public UserFacilityDto UserFacility { get; set; }

        public List<UserFacilityJobTitleDto> UserFacilityJobTitleDto { get; set; }
    }
}
