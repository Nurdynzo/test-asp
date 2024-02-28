using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Organizations.Dto
{
    public class FindOrganizationUnitRolesInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}