using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.VitalSigns.Dto;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos;

public class DoctorSummaryDto
{
    public long DoctorUserId { get; set; }
    public string SummaryHeader { get; set; }
    public DoctorNoteDto DoctorNote { get; set; }
}