using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientCodeTemplatesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
