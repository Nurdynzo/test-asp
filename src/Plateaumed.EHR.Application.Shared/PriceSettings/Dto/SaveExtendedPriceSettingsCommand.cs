namespace Plateaumed.EHR.PriceSettings.Dto
{
    public class SaveExtendedPriceSettingsCommand
    {
        public CreatePriceConsultationSettingsCommand ConsultationSettings { get; set; }
        public CreatePriceWardAdmissionSettingCommand WardAdmissionSettings { get; set; }
        public CreatePriceMealSettingCommand MealSettings { get; set; }
        public CreatePriceDiscountSettingCommand DiscountSettings { get; set; }
    }
}