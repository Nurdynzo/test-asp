using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Staff.Dtos;

public class CreateOrEditJobRequest
{
    [Required] 
    public long UserId { get; set; }

    [Required] 
    public JobDto Job { get; set; }
}