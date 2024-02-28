using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class CreatePriceMealSettingCommand : EntityDto<long>
    {
        [Required]
        public long FacilityId { get; set; }
        public int DefaultInitialPeriodValue { get; set; }
        public PriceTimeFrequency DefaultInitialPeriodUnit { get; set; }
        public PriceTimeFrequency DefaultContinuePeriodUnit { get; set; }
        public int DefaultContinuePeriodValue { get; set; }
        public bool IsMealPlanEnabled { get; set; }
        public List<PriceMealPlanDefinitionCommand> MealPlanDefinitions { get; set; }
    }
}
