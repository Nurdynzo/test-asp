using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.PatientDocument.Dto;

/// <summary>
/// Scan Document Upload Request
/// </summary>
public class PatientScanDocumentUploadRequest : EntityDto<long?>
{
    /// <summary>
    /// File Id to be generated internally by the system
    /// </summary>
    public Guid? FileId { get; set; }  
    
    /// <summary>
    /// File to be uploaded by the user
    /// Please note that the file name should be in the format: PatientCode#FirstName#LastName
    /// </summary>
    [Required]
    public IFormFile File { get; init; }

    /// <summary>
    /// if this true the guid for the existing must be set
    /// </summary>
    public bool IsUpdate { get; set; }
}