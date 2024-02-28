using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientOccupationCategoryDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
