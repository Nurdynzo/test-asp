using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
