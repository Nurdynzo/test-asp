using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientInsurersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string InsuranceProviderNameFilter { get; set; }
    }
}
