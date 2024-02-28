using System.ComponentModel.DataAnnotations;
using Abp.Organizations;
using Plateaumed.EHR.Organizations.Dtos;

namespace Plateaumed.EHR.Organizations.Dto
{
    public class CreateOrganizationUnitInput
    {
        public long? ParentId { get; set; }

        [Required]
        public long FacilityId { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(OrganizationUnitExtendedConsts.MaxShortNameLength)]
        public string ShortName { get; set; }

        public bool? IsActive { get; set; }

        [Required]
        public string Type{ get; set; }

        public OrganizationUnitTimeDto[] OperatingTimes { get; set; }
    }
}