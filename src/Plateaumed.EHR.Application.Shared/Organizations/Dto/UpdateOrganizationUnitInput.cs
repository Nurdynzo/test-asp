using System.ComponentModel.DataAnnotations;
using Abp.Organizations;
using Plateaumed.EHR.Organizations.Dtos;

namespace Plateaumed.EHR.Organizations.Dto
{
    public class UpdateOrganizationUnitInput
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(OrganizationUnitExtendedConsts.MaxShortNameLength)]
        public string ShortName { get; set; }

        public bool? IsActive { get; set; }

        public OrganizationUnitTimeDto[] OperatingTimes { get; set; }
    }
}