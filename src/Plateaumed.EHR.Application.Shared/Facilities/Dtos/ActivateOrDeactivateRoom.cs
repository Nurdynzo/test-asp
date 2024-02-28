using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateOrDeactivateRoom : EntityDto<long?>
    {
        public bool IsActive { get; set; }
    }
}
