using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string PatientCodeFilter { get; set; }

        public string CountryNameFilter { get; set; }

        public string PatientOccupationNameFilter { get; set; }

        public string PatientOccupationCategoryNameFilter { get; set; }
    }
}
