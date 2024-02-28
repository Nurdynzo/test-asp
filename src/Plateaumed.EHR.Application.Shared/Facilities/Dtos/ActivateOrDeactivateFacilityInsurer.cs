using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateOrDeactivateFacilityInsurerRequest : EntityDto<long?>
    {
        public bool IsActive { get; set; }
    }
}
