using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Patients;

[Table("SerialCode")]
public class SerialCode : Entity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }

    public long FacilityId { get; set; }

    public virtual long LastGeneratedNo { get; set; }
}