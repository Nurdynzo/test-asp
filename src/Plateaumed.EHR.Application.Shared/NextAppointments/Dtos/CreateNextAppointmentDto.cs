using System;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.NextAppointments.Dtos;

public class CreateNextAppointmentDto
{
    public long? Id { get; set; }
    [Required]
    public long PatientId { get; set; }
    [Required]
    public long UnitId { get; set; }
    [Required]
    public long EncounterId { get; set; }
    [Required]
    public DateTypes? DateType { get; set; }
    public int? SeenIn { get; set; }
    public bool? IsToSeeSameDoctor { get; set; }
    public DateTime? AppointmentDate { get; set; }
}
