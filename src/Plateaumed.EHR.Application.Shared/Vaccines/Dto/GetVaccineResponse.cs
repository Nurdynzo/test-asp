using System.Collections.Generic;

namespace Plateaumed.EHR.Vaccines.Dto;

public class GetVaccineResponse
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string FullName { get; set; }

    public List<VaccineScheduleDto> Schedules { get; set; }
}