using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetJobTitleForEditOutput
    {
        public CreateOrEditJobTitleDto JobTitle { get; set; }
    }
}
