using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Vaccines.Dto;

public class VaccineScheduleDto
{
    public long Id { get; set; }

    public string Dosage { get; set; }

    public int Doses { get; set; }

    public string RouteOfAdministration { get; set; }

    public int? AgeMin { get; set; }

    public UnitOfTime? AgeMinUnit { get; set; }

    public int? AgeMax { get; set; }

    public UnitOfTime? AgeMaxUnit { get; set; }

    public string Notes { get; set; }
}