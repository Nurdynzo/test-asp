using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PhysicalExaminations.Dto;

public class GetPhysicalExaminationListRequest
{
    [Required]
    public string Header { get; set; }

    [Required]
    public long PatientId { get; set; }
}