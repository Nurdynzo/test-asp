using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.NurseCarePlans.Dto;

public class GetNurseCareSummaryResponse
{
    public long Id { get; set; }
    public string Diagnosis { get; set; }
    public List<string> Outcomes { get; set; }
    public List<string> Interventions { get; set; }
    public List<string> Activities { get; set; }
    public string Evaluation { get; set; }
    public DateTime Time { get; set; }
}