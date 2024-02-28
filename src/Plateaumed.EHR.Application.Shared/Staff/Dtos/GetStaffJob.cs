using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetStaffJob
    {
        public long? Id { get; set; }

        public bool IsPrimary { get; set; }

        public long? FacilityId { get; set; }
        public FacilityDto Facility { get; set; }

        public long? JobTitleId { get; set; }
        public JobTitleDto JobTitle { get; set; }

        public long? JobLevelId { get; set; }
        public JobLevelDto JobLevel { get; set; }

        public string TeamRole { get; set; }

        public long? DepartmentId { get; set; }
        public OrganizationUnitDto Department { get; set; }

        public long? UnitId { get; set; }
        public OrganizationUnitDto Unit { get; set; }

        public List<long> Wards { get; set; } = new();

        public List<WardDto> StaffWards { get; set; }

        public List<ServiceCentreType> ServiceCentres { get; set; } = new();
    }
}
