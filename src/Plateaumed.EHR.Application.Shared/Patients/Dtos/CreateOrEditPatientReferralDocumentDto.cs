using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientReferralDocumentDto : EntityDto<long?>
    {
        [StringLength(
            PatientReferralDocumentConsts.MaxReferringHealthCareProviderLength,
            MinimumLength = PatientReferralDocumentConsts.MinReferringHealthCareProviderLength
        )]
        public string ReferringHospital { get; set; }

        [StringLength(
            PatientReferralDocumentConsts.MaxDiagnosisSummaryLength,
            MinimumLength = PatientReferralDocumentConsts.MinDiagnosisSummaryLength
        )]
        public string DiagnosisSummary { get; set; }

        public Guid? ReferralDocument { get; set; }

        public string ReferralDocumentToken { get; set; }
    }
}
