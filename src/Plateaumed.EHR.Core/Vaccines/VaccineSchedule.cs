using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Vaccines;

[Table("VaccineSchedules")]
public class VaccineSchedule : FullAuditedEntity<long>
{
    public long VaccineId { get; set; }

    [ForeignKey("VaccineId")]
    public Vaccine Vaccine { get; set; }

    public string Dosage { get; set; }

    public int Doses { get; set; }

    public string RouteOfAdministration { get; set; }

    public int? AgeMin { get; set; }

    public UnitOfTime? AgeMinUnit { get; set; }

    public int? AgeMax { get; set; }

    public UnitOfTime? AgeMaxUnit { get; set; }

    public string Notes { get; set; }
}