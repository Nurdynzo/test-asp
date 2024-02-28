using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientOccupationForEditOutput
    {
        public CreateOrEditPatientOccupationDto PatientOccupation { get; set; }

        public string PatientOccupationCategoryName { get; set; }
    }
}
