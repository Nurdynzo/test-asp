using Plateaumed.EHR.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.NextAppointments.Dtos;

public class NextAppointmentUnitReturnDto
{
    [Required]
    public long Id { get; set; }
    public string Name { get; set; }
    public List<OperationTimeDto> OperatingTimes { get; set; }
    public bool IsClinic { get; set; }
}

public class OperationTimeDto
{
    public long Id { get; set; }
    public virtual DaysOfTheWeek DayOfTheWeek { get; set; }
    public int DayOfTheWeekNo { get; set; }
    public DateTime? OpeningTime { get; set; }
    public DateTime? ClosingTime { get; set; }
}
