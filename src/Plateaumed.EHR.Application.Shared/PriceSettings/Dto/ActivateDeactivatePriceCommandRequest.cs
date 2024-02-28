using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class ActivateDeactivatePriceCommandRequest : EntityDto<long>
    {
        public bool IsActive { get; set; }
    }
}
