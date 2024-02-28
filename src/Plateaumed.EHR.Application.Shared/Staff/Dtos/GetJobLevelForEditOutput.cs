using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetJobLevelForEditOutput
    {
        public CreateOrEditJobLevelDto JobLevel { get; set; }

        public string JobTitleName { get; set; }
    }
}
