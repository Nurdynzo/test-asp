using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations.Dtos;

namespace Plateaumed.EHR.Organizations.Dto
{
    public class OrganizationUnitDto : AuditedEntityDto<long>
    {
        public long? ParentId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string ShortName { get; set; }

        public bool? IsActive { get; set; }

        public bool IsStatic { get; set; }

        public string Type { get; set; }

        public ServiceCentreType? ServiceCentre { get; set; }

        public long? FacilityId { get; set; }

        public int MemberCount { get; set; }

        public int RoleCount { get; set; }

        public OrganizationUnitTimeDto[] OperatingTimes { get; set; }
    }
}