using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetStaffMemberForEditOutput
    {
        [Required]
        public StaffMemberDto StaffMember { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }

        public bool SendActivationEmail { get; set; }

        public bool SetRandomPassword { get; set; }

        public List<long> OrganizationUnits { get; set; }

        public string JobTitleName { get; set; }

        public string JobLevelName { get; set; }

        public string DefaultFacility { get; set; }

        public List<long> AssignedFacilities { get; set; }

        public GetStaffMemberForEditOutput()
        {
            OrganizationUnits = new List<long>();

            AssignedFacilities = new List<long>();
        }
    }
}