
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.IntakeOutputs.Dtos;

public class IntakeOutputReturnDto
{
    public long Id { get; set; }
    [Required]
    public long PatientId { get; set; }
    [Required]
    public ChartingType Type { get; set; }
    public string Pannel { get; set; }
    [Required]
    public string SuggestedText { get; set; }
    [Required]
    public double VolumnInMls { get; set; }
    public long? ProcedureId { get; set; } = null;
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}
