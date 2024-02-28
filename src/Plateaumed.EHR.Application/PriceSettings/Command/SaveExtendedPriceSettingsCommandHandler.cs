using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Command
{
    public class SaveExtendedPriceSettingsCommandHandler : ISaveExtendedPriceSettingsCommandHandler
    {
        private readonly IRepository<PricingDiscountSetting, long> _pricingDiscountSettingRepository;
        private readonly IRepository<PriceWardAdmissionSetting, long> _priceWardAdmissionSettingRepository;
        private readonly IRepository<PriceMealSetting, long> _priceMealSettingRepository;
        private readonly IRepository<PriceConsultationSetting, long> _priceConsultationSettingRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public SaveExtendedPriceSettingsCommandHandler(
            IRepository<PricingDiscountSetting, long> pricingDiscountSettingRepository,
            IRepository<PriceWardAdmissionSetting, long> priceWardAdmissionSettingRepository,
            IRepository<PriceMealSetting, long> priceMealSettingRepository,
            IRepository<PriceConsultationSetting, long> priceConsultationSettingRepository,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _pricingDiscountSettingRepository = pricingDiscountSettingRepository;
            _priceWardAdmissionSettingRepository = priceWardAdmissionSettingRepository;
            _priceMealSettingRepository = priceMealSettingRepository;
            _priceConsultationSettingRepository = priceConsultationSettingRepository;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task Handle(SaveExtendedPriceSettingsCommand request)
        {
            var pricingDiscountSetting = _objectMapper.Map<PricingDiscountSetting>(request.DiscountSettings);
            var priceConsultationSetting = _objectMapper.Map<PriceConsultationSetting>(request.ConsultationSettings);
            var priceWardAdmissionSetting = _objectMapper.Map<PriceWardAdmissionSetting>(request.WardAdmissionSettings);
            var priceMealSetting = _objectMapper.Map<PriceMealSetting>(request.MealSettings);

            await SaveConsultationSetting(priceConsultationSetting).ConfigureAwait(false);
            await SaveWardAdmissionSetting(priceWardAdmissionSetting).ConfigureAwait(false);
            await SaveMealSetting(priceMealSetting).ConfigureAwait(false);
            await SaveDiscountSetting(pricingDiscountSetting).ConfigureAwait(false);


        }
        private async Task SaveDiscountSetting(PricingDiscountSetting pricingDiscountSetting)
        {
            if (pricingDiscountSetting.Id <= 0)
            {
                await _pricingDiscountSettingRepository.InsertAsync(pricingDiscountSetting).ConfigureAwait(false);
                return;
            }
            var pricingDiscount = await _pricingDiscountSettingRepository
                .GetAll()
                .Include(x=>x.PriceCategoryDiscounts)
                .FirstOrDefaultAsync(x => x.FacilityId == pricingDiscountSetting.FacilityId)
                .ConfigureAwait(false) ?? throw new UserFriendlyException("The discount setting is not found");

            pricingDiscount.GlobalDiscount = pricingDiscountSetting.GlobalDiscount;

            foreach (var categoryDiscount in pricingDiscountSetting.PriceCategoryDiscounts)
            {
	            var requestItems = pricingDiscount.PriceCategoryDiscounts
		            .FirstOrDefault(x => x.Id == categoryDiscount.Id);
	            if (requestItems is null)
	            {
		            categoryDiscount.PricingDiscountSettingId = pricingDiscount.Id;
		            categoryDiscount.Id = 0;
		            pricingDiscount.PriceCategoryDiscounts.Add(categoryDiscount);
	            }
	            else
	            {
		            requestItems.IsActive = categoryDiscount.IsActive;
		            requestItems.PricingCategory = categoryDiscount.PricingCategory;
		            requestItems.Percentage = categoryDiscount.Percentage;
	            }
            }
            await _pricingDiscountSettingRepository.UpdateAsync(pricingDiscount).ConfigureAwait(false);
            await _unitOfWorkManager.Current.SaveChangesAsync().ConfigureAwait(false);
        }
        private async Task SaveMealSetting(PriceMealSetting priceMealSetting)
        {
            if (priceMealSetting.Id <= 0)
            {
                await _priceMealSettingRepository.InsertAsync(priceMealSetting);
                return;
            }
            var priceMeal = await _priceMealSettingRepository.GetAll()
                                .Include(x => x.MealPlanDefinitions)
                                .FirstOrDefaultAsync(x => x.FacilityId == priceMealSetting.FacilityId).ConfigureAwait(false)
                            ?? throw new UserFriendlyException("The meal setting is not found");
            priceMeal.IsMealPlanEnabled = priceMealSetting.IsMealPlanEnabled;
            priceMeal.DefaultInitialPeriodValue = priceMealSetting.DefaultInitialPeriodValue;
            priceMeal.DefaultInitialPeriodUnit = priceMealSetting.DefaultInitialPeriodUnit;
            priceMeal.DefaultContinuePeriodValue = priceMealSetting.DefaultContinuePeriodValue;
            priceMeal.DefaultContinuePeriodUnit = priceMealSetting.DefaultContinuePeriodUnit;
            var priceMealMealPlanDefinitions = priceMealSetting.MealPlanDefinitions;

            foreach (var planDefinition in priceMealMealPlanDefinitions)
            {
	            var planRequest = priceMeal.MealPlanDefinitions.FirstOrDefault(x => x.Id == planDefinition.Id);
	            if (planRequest is null)
	            {
		            planDefinition.PriceMealSettingId = priceMeal.Id;
		            planDefinition.Id = 0;
		            priceMeal.MealPlanDefinitions.Add(planDefinition);
	            }
	            else
	            {
		            planRequest.PlanType = planDefinition.PlanType;
		            planRequest.IsActive = planDefinition.IsActive;
		            planRequest.Description = planDefinition.Description;
		            planRequest.IsDefault = planDefinition.IsDefault;
	            }
            }
            await _priceMealSettingRepository.UpdateAsync(priceMeal).ConfigureAwait(false);
            await _unitOfWorkManager.Current.SaveChangesAsync().ConfigureAwait(false);
        }
        private async Task SaveWardAdmissionSetting(PriceWardAdmissionSetting priceWardAdmissionSetting)
        {
            if (priceWardAdmissionSetting.Id <= 0)
            {
                await _priceWardAdmissionSettingRepository.InsertAsync(priceWardAdmissionSetting).ConfigureAwait(false);
                return;
            }
            var item = await _priceWardAdmissionSettingRepository
	                       .FirstOrDefaultAsync(x=>x.FacilityId == priceWardAdmissionSetting.FacilityId).ConfigureAwait(false)
                ?? throw new UserFriendlyException("The admission setting is not found");
            item.DefaultContinuePeriodUnit = priceWardAdmissionSetting.DefaultContinuePeriodUnit;
            item.DefaultContinuePeriodValue = priceWardAdmissionSetting.DefaultContinuePeriodValue;
            item.DefaultInitialPeriodUnit = priceWardAdmissionSetting.DefaultInitialPeriodUnit;
            item.DefaultInitialPeriodValue = priceWardAdmissionSetting.DefaultInitialPeriodValue;
            item.AdmissionChargeTime = priceWardAdmissionSetting.AdmissionChargeTime;
            item.FollowUpVisitPercentage = priceWardAdmissionSetting.FollowUpVisitPercentage;
            await _priceWardAdmissionSettingRepository.UpdateAsync(item).ConfigureAwait(false);
			await _unitOfWorkManager.Current.SaveChangesAsync().ConfigureAwait(false);
        }
        private async Task SaveConsultationSetting(PriceConsultationSetting priceConsultationSetting)
        {

            if (priceConsultationSetting.Id <= 0)
            {
                await _priceConsultationSettingRepository.InsertAsync(priceConsultationSetting).ConfigureAwait(false);
                return;
            }
            var item = await _priceConsultationSettingRepository
	                       .FirstOrDefaultAsync(x => x.FacilityId == priceConsultationSetting.FacilityId).ConfigureAwait(false)
                ?? throw new UserFriendlyException("The consultation setting is not found");
            item.FrequencyOfChargesTimes = priceConsultationSetting.FrequencyOfChargesTimes;
            item.FrequencyOfChargeValue = priceConsultationSetting.FrequencyOfChargeValue;
            item.PercentageOfFrequencyCharges = priceConsultationSetting.PercentageOfFrequencyCharges;
            item.IsFollowUpVisitEnabled = priceConsultationSetting.IsFollowUpVisitEnabled;
            item.UnitOfFrequencyChargeUnit = priceConsultationSetting.UnitOfFrequencyChargeUnit;

            await _priceConsultationSettingRepository.UpdateAsync(item).ConfigureAwait(false);
            await _unitOfWorkManager.Current.SaveChangesAsync().ConfigureAwait(false);
        }
    }

}
