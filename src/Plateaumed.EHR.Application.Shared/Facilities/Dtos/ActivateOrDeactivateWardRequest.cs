using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateOrDeactivateWardRequest : EntityDto<long?>
    {
        public bool IsActive { get; set; }
    }
}