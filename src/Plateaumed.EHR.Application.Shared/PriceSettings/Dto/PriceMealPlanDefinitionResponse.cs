using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class PriceMealPlanDefinitionResponse :EntityDto<long>
    {
        public string PlanType { get; set; }
        public string Description { get; set; }
        public long PriceMealSettingId { get; set; }
        public long FacilityId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
