using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Plateaumed.EHR.PatientDocument.Dto
{
    public class ReviewedScannedDocumentRequest: EntityDto<long>
    {
    
        [Required]
        public IFormFile File { get; set; }
        
        [Required]
        public Guid FileId { get; set; }
        
        [Required]
        public bool IsApproved { get; set; }

        public String ReviewNote { get; set; }
    }
}
