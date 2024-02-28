using Plateaumed.EHR.Misc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.NextAppointments.Dtos;

public class NextAppointmentReturnDto
{
    public long Id { get; set; }
    [Required]
    public long PatientId { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public long StaffId { get; set; }
    [Required]
    public long UnitId { get; set; }
    [Required]
    public DateTypes DateType { get; set; }
    public int? SeenIn { get; set; }
    public bool? IsToSeeSameDoctor { get; set; }
    public DaysOfTheWeek DayOfTheWeek { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public virtual string Title { get; set; }
    public virtual int Duration { get; set; }
}