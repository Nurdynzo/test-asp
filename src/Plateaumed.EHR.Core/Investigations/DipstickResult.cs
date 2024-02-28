using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations;

public class DipstickResult : Entity<long>
{
    public string Result { get; set; }

    public int Order { get; set; }
}