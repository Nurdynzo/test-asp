using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.PatientDocument;

public class GetScannedDocumentsForReviewResponse: EntityDto<long>
{
    public string PatientFullName { get; set; }
    public string PatientCode { get; set; }
    public string PictureId { get; set; }

    public string PictureUrl { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public Guid FileId { get; set; }

    public bool? IsApproved { get; set; }

    public string ReviewerNote { get; set; }
}