using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PriceSettings.Abstraction;
using Plateaumed.EHR.PriceSettings.Dto;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.PriceSettings.Integration;

[Trait("Category","Integration")]
public class PriceSettingsAppServiceTest: AppTestBase
{
	private readonly IPriceSettingsAppService _priceSettingsAppService;
	private readonly IAbpSession _abpSession;
	public PriceSettingsAppServiceTest()
	{
		_priceSettingsAppService = Resolve<IPriceSettingsAppService>();
		_abpSession = Resolve<IAbpSession>();
	}
	[Fact]
	public async Task CreateNewPricing_Should_Create_New_Pricing()
	{
		//arrange
		LoginAsDefaultTenantAdmin();
		var tenantId = _abpSession.TenantId;
		var request = GetCreatNewPricingCommandRequest();
		//act
		await _priceSettingsAppService.CreateNewPricing(request);
		//assert
		ItemPricing itemPricing = null;
		UsingDbContext(context =>
		{
			itemPricing = context.ItemPricing.FirstOrDefault(x => x.Name == request[0].Name);

		});
		itemPricing.ShouldNotBeNull();
		itemPricing.IsActive.ShouldBeTrue();
		itemPricing.Amount.Currency.ShouldBe(request[0].Amount.Currency);
		itemPricing.Amount.Amount.ShouldBe(request[0].Amount.Amount);
		itemPricing.TenantId.ShouldBe(tenantId.GetValueOrDefault());

	}
	[Fact]
	public async Task SaveExtendedPriceSettings_Should_Save_All_Extended_Price_Settings_With_Valid_Request()
	{
		//arrange
		LoginAsDefaultTenantAdmin();
		var request = GetCreatePriceSettingsCommandRequest();
		//act
		await _priceSettingsAppService.SaveExtendedPriceSettings(request);
		//assert
		var priceConsultationSetting =
			UsingDbContext(context => context.PriceConsultationSettings.FirstOrDefault(x => x.FacilityId == request.ConsultationSettings.FacilityId));
		var pricingDiscountSetting =
			UsingDbContext(context => context.PricingDiscountSettings.Include(x=>x.PriceCategoryDiscounts).FirstOrDefault(x => x.FacilityId == request.DiscountSettings.FacilityId));
		var priceMealSetting =
			UsingDbContext(context => context.PriceMealSettings.Include(x=>x.MealPlanDefinitions).FirstOrDefault(x => x.FacilityId == request.MealSettings.FacilityId));
		var priceWardAdmissionSetting =
			UsingDbContext(context => context.PriceWardAdmissionSettings.FirstOrDefault(x => x.FacilityId == request.WardAdmissionSettings.FacilityId));

		priceConsultationSetting.ShouldNotBeNull();
		priceConsultationSetting.Id.ShouldBeGreaterThan(0);
		pricingDiscountSetting.ShouldNotBeNull();
		pricingDiscountSetting.Id.ShouldBeGreaterThan(0);
		pricingDiscountSetting.PriceCategoryDiscounts.Count.ShouldBe(2);
		priceMealSetting.ShouldNotBeNull();
		priceMealSetting.Id.ShouldBeGreaterThan(0);
		priceMealSetting.MealPlanDefinitions.Count.ShouldBe(2);
		priceWardAdmissionSetting.ShouldNotBeNull();
		priceWardAdmissionSetting.Id.ShouldBeGreaterThan(0);

	}
	private static SaveExtendedPriceSettingsCommand GetCreatePriceSettingsCommandRequest()
		=> new()
		{
			ConsultationSettings = new CreatePriceConsultationSettingsCommand()
			{
				FacilityId = 1,
				FrequencyOfChargesTimes = 1,
				FrequencyOfChargeValue = 1,
				PercentageOfFrequencyCharges = 1,
				IsFollowUpVisitEnabled = true,
				UnitOfFrequencyChargeUnit = PriceTimeFrequency.Day,
			},
			DiscountSettings = new CreatePriceDiscountSettingCommand()
			{
				FacilityId = 1,
				PriceCategoryDiscounts = new List<PriceCategoryDiscountCommand>()
				{
					new()
					{
						FacilityId = 1,
						PricingCategory = PricingCategory.Consultation,
						IsActive = true,
						Percentage = 2
					},
					new()
					{
						FacilityId = 1,
						PricingCategory = PricingCategory.Laboratory,
						IsActive = true,
						Percentage = 2
					}

				},
			},
			MealSettings = new CreatePriceMealSettingCommand()
			{
				FacilityId = 1,
				DefaultContinuePeriodUnit = PriceTimeFrequency.Day,
				DefaultContinuePeriodValue = 2,
				DefaultInitialPeriodUnit = PriceTimeFrequency.Week,
				DefaultInitialPeriodValue = 3,
				IsMealPlanEnabled = true,
				MealPlanDefinitions = new List<PriceMealPlanDefinitionCommand>()
				{
					new()
					{
						FacilityId = 1,
						Description = "test description",
						PlanType = "test plan type",
					},
					new()
					{
						FacilityId = 1,
						Description = "test description",
						PlanType = "test plan type",
					}
				}
			},
			WardAdmissionSettings = new CreatePriceWardAdmissionSettingCommand()
			{
				FacilityId = 1,
				DefaultContinuePeriodUnit = PriceTimeFrequency.Day,
				DefaultContinuePeriodValue = 2,
				DefaultInitialPeriodUnit = PriceTimeFrequency.Day,
				DefaultInitialPeriodValue = 3,
				AdmissionChargeTime = new TimeOnly(4,10),
				FollowUpVisitPercentage = 2
			}
		};
	private static List<CreateNewPricingCommandRequest> GetCreatNewPricingCommandRequest()
		=> new()
		{
			new()
			{
				Amount = new MoneyDto
				{
					Amount = 100,
					Currency = "NGN"
				},
				ItemId = "ItemId",
				PricingCategory = PricingCategory.Consultation,
				PricingType = PricingType.GeneralPricing,
				SubCategory = "SubCategory",
				Name = "Item1",
				FacilityId = 1,

			},
			new ()
			{
				Amount = new MoneyDto
				{
					Amount = 100,
					Currency = "NGN"
				},
				ItemId = "ItemId",
				PricingCategory = PricingCategory.Consultation,
				PricingType = PricingType.GeneralPricing,
				SubCategory = "SubCategory",
				Name = "Item2",
				FacilityId = 1,

			}

		};

}
