using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientCodeTemplateForEditOutput
    {
        public CreateOrEditPatientCodeTemplateDto PatientCodeTemplate { get; set; }
    }
}
