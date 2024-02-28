using Abp.AutoMapper;
using Plateaumed.EHR.Organizations.Dto;

namespace Plateaumed.EHR.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}
