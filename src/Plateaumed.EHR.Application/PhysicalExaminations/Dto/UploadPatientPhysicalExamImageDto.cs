using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class UploadPatientPhysicalExamImageDto
{
    [Required]
    public long PatientPhysicalExaminationId { get; set; }
    
    [Required]
    public List<IFormFile> ImageFiles { get; set; }
}