using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class ActivateOrDeactivateStaffMemberRequest : EntityDto<long?>
    {
        public bool IsActive { get; set; }
    }
}
