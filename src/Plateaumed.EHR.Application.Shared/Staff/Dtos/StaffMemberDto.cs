using System;
using Plateaumed.EHR.Authorization.Users.Dto;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class StaffMemberDto : UserEditDto
    {
        public string StaffCode { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public long? JobTitleId { get; set; }

        public long? JobLevelId { get; set; }

        public long? DepartmentId { get; set; }

        public long? UnitId { get; set; }

        public long? FacilityId { get; set; }
    }
}