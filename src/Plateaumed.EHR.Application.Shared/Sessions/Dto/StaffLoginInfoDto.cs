using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Sessions.Dto
{
    public class StaffLoginInfoDto
    {
        public string StaffCode { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public string JobTitle { get; set; }

        public string JobLevel { get; set; }

        public List<StaffAssignedFacilitiesListDto> AssignedFacilities { get; set; }

        public List<ServiceCentreType> ServiceCentres { get; set; }

    }
}