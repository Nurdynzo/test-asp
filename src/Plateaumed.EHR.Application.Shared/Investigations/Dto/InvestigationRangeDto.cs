using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Investigations.Dto;

public class InvestigationRangeDto
{
    public int? AgeMin { get; set; }
    public UnitOfTime? AgeMinUnit { get; set; }
    public int? AgeMax { get; set; }
    public UnitOfTime? AgeMaxUnit { get; set; }
    public GenderType? Gender { get; set; }
    public string Unit { get; set; }
    public decimal? MinRange { get; set; }
    public decimal? MaxRange { get; set; }
}