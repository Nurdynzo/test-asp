using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class PriceMealPlanDefinitionCommand : EntityDto<long>
    {
        [Required]
        public long FacilityId { get; set; }
        public string PlanType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
