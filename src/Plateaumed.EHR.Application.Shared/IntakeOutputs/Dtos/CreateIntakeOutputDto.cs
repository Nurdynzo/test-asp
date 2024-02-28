﻿using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Procedures;

namespace Plateaumed.EHR.IntakeOutputs.Dtos;

public class CreateIntakeOutputDto
{
    public long? Id { get; set; }
    [Required]
    public long PatientId { get; set; }
    public ChartingType Type { get; set; }
    [Required]
    public string SuggestedText { get; set; }
    [Required]
    public double VolumnInMls { get; set; }
    public long EncounterId { get; set; }
    public long? ProcedureId { get; set; } = null;
    public ProcedureEntryType? ProcedureEntryType { get; set; }
}
