using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations;

public class DipstickRange : Entity<long>
{
    public string Unit { get; set; }

    public List<DipstickResult> Results { get; set; }
}