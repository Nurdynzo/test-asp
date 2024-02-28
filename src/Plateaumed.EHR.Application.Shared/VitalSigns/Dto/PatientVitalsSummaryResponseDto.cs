using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.VitalSigns.Dto;

public class PatientVitalsSummaryResponseDto
{
    public DateTime Date { get; set; }
    public List<PatientVitalResponseDto> PatientVitals { get; set; }
    public DateTime Time { get; set; }

    public bool IsDeleted { get; set; }
}
