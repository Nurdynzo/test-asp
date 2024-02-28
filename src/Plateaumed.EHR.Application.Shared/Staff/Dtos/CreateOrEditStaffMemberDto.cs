using Plateaumed.EHR.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class CreateOrEditStaffMemberDto
    {
        [Required]
        public StaffMemberDto StaffMember { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }

        public bool SendActivationEmail { get; set; }

        public bool SetRandomPassword { get; set; }

        public long? DefaultFacilityId { get; set; }

        public List<long> OrganizationUnits { get; set; }

        public List<long> AssignedFacilities { get; set; }

        public CreateOrEditStaffMemberDto()
        {
            OrganizationUnits = new List<long>();

            AssignedFacilities = new List<long>();
        }
    }
}
