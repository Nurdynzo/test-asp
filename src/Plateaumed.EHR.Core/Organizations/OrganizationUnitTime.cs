using Plateaumed.EHR.Misc;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Plateaumed.EHR.Organizations
{
    [Table("AppOrganizationUnitTimes")]
    [Audited]
    public class OrganizationUnitTime : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public virtual DaysOfTheWeek DayOfTheWeek { get; set; }

        public virtual DateTime? OpeningTime { get; set; }

        public virtual DateTime? ClosingTime { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual long? OrganizationUnitExtendedId { get; set; }

        public OrganizationUnitExtended OrganizationUnitExtendedFk { get; set; }
     }
}