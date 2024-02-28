using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientForEditOutput
    {
        public CreateOrEditPatientDto Patient { get; set; }

        public string PatientOccupationName { get; set; }

        public string PatientOccupationCategoryName { get; set; }
    }
}
