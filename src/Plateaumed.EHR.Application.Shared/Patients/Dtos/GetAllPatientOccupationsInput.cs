using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientOccupationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }
    }
}
