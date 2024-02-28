using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateOrDeactivateLaboratoryRequest : EntityDto<long?>
    {
        public bool HasLaboratory { get; set; }
    }
}
