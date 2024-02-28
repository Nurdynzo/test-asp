using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetFacilityStaffForEditOutput
    {
        public CreateOrEditFacilityStaffDto FacilityStaff { get; set; }

        public string FacilityName { get; set; }

        public string StaffMemberStaffCode { get; set; }

    }
}