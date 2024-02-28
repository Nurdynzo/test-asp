using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientReferralDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
