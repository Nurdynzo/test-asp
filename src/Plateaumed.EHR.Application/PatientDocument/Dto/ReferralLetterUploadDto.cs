using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientDocument.Dto;

/// <summary>
/// Referral document upload request
/// </summary>
public class ReferralLetterUploadRequest: EntityDto<long?>
{
    /// <summary>
    /// Referring Hospital
    /// </summary>
    [StringLength(
        PatientReferralDocumentConsts.MaxReferringHealthCareProviderLength,
        MinimumLength = PatientReferralDocumentConsts.MinReferringHealthCareProviderLength
    )]
    public string ReferringHealthCareProvider { get; set; }
   
    /// <summary>
    /// Diagnosis Summary
    /// </summary>
    [StringLength(
        PatientReferralDocumentConsts.MaxDiagnosisSummaryLength,
        MinimumLength = PatientReferralDocumentConsts.MinDiagnosisSummaryLength
    )]
    public string DiagnosisSummary { get; set; }

    /// <summary>
    /// Unique file ID
    /// </summary>
    public Guid? FileId { get; set; }

    /// <summary>
    /// File to be uploaded
    /// </summary>
    [Required]
    public IFormFile File { get; set; }
    
    /// <summary>
    /// Patient Id
    /// </summary>
    [Required]
    public long PatientId { get; set; }


    /// <summary>
    /// Appointment Id
    /// </summary>
    public long? AppointmentId { get; set; }
    
}