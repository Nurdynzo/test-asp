using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Organizations;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Organizations
{
    public class OrganizationUnitExtended : OrganizationUnit
    {
        [StringLength(OrganizationUnitExtendedConsts.MaxShortNameLength)]
        public string ShortName { get; set; }

        public bool? IsActive { get; set; }

        public OrganizationUnitType Type{ get; set; }
        
        public long FacilityId { get; set; }

        public virtual Facility Facility { get; set; }

        public ICollection<OrganizationUnitTime> OperatingTimes { get; set; }

        public ICollection<UserOrganizationUnit> UserOrganizationUnits { get; set; }

        public ICollection<OrganizationUnitRole> OrganizationUnitRoles{ get; set; }

        public bool IsStatic { get; set; }

        public ServiceCentreType? ServiceCentre { get; set; }

        public OrganizationUnitExtended()
        {
            OperatingTimes = new List<OrganizationUnitTime>();
        }

        public OrganizationUnitExtended(int? tenantId, string displayName, long? parentId = null) : base(tenantId, displayName, parentId) { }
    }
}