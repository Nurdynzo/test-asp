using Abp.Domain.Entities;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.Investigations;

public class InvestigationRange : Entity<long>
{
    public long InvestigationId { get; set; }

    [ForeignKey(nameof(InvestigationId))]
    public Investigation Investigation { get; set; }

    public int? AgeMin { get; set; }

    public UnitOfTime? AgeMinUnit { get; set; }

    public int? AgeMax { get; set; }

    public UnitOfTime? AgeMaxUnit { get; set; }

    public GenderType? Gender { get; set; }

    public string Unit { get; set; }

    public decimal? MinRange { get; set; }

    public decimal? MaxRange { get; set; }

    public string Other { get; set; }
}