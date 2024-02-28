using Abp.AutoMapper;
using Plateaumed.EHR.Organizations.Dto;

namespace Plateaumed.EHR.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}