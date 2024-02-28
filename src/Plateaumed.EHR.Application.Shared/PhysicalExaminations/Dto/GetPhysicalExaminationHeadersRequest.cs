using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class GetPhysicalExaminationHeadersRequest
{
    [Required]
    public long PhysicalExaminationTypeId { get; set; }
}