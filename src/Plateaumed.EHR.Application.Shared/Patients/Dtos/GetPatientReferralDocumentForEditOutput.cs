using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientReferralDocumentForEditOutput
    {
        public CreateOrEditPatientReferralDocumentDto PatientReferralDocument { get; set; }

        public string ReferralDocumentFileName { get; set; }
    }
}
