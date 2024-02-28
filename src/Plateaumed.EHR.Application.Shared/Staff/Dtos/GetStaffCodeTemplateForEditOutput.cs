using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetStaffCodeTemplateForEditOutput
    {
        public CreateOrEditStaffCodeTemplateDto StaffCodeTemplate { get; set; }
    }
}
