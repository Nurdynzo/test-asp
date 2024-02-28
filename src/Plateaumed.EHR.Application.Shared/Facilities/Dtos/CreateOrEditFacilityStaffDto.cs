using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityStaffDto : EntityDto<long?>
    {

        public long FacilityId { get; set; }

        public long StaffMemberId { get; set; }

    }
}