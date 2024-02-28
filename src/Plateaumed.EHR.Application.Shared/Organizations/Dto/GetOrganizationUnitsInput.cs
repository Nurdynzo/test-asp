using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Organizations.Dto;

public class GetOrganizationUnitsInput : PagedAndFilteredInputDto
{
    [Required]
    public long FacilityId { get; set; }

    [DefaultValue(false)]
    public bool IncludeClinics { get; set; }
    public long? UnitId { get; set; }
}