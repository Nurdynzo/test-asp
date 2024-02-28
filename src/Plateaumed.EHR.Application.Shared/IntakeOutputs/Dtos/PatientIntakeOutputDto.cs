using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.IntakeOutputs.Dtos;

public class PatientIntakeOutputDto
{
    [Required]
    public long PatientId { get; set; }
    [Required]
    public ChartingType Type { get; set; }
    public string ChartingTypeText { get; set; }
    public List<SuggestedTextDto> SuggestedText { get; set; }
}

public class SuggestedTextDto
{
    public long? Id { get; set; }
    [Required]
    public string SuggestedText { get; set; }
    [Required]
    public double VolumnInMls { get; set; }
    public string Frequency { get; set; }
    public long? ProcedureId { get; set; }
    public ProcedureEntryType? ProcedureEntryType { get; set; }
    public DateTime? CreatedAt { get; set; }
}