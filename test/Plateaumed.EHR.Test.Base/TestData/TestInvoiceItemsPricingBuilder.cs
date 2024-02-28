using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestInvoiceItemsPricingBuilder
{
    private readonly int _tenantId;
    private readonly long? _categoryId;
    private readonly long _facilityId;
    private readonly string _name;
    private readonly Money _amount;
    private ItemPricing _itemPricing;

    private TestInvoiceItemsPricingBuilder(int tenantId, long? categoryId, string name, Money amount, long facilityId)
    {
        _tenantId = tenantId;
        _categoryId = categoryId;
        _name = name;
        _amount = amount;
        _facilityId = facilityId;

    }
    public static TestInvoiceItemsPricingBuilder Create(int tenantId, long? categoryId, string name, Money amount, long facilityId)
    {
        return new TestInvoiceItemsPricingBuilder(tenantId, categoryId, name, amount, facilityId);
    }

    public ItemPricing Build()
    {
        _itemPricing = new ItemPricing
        {
            TenantId = _tenantId,
            Name = _name,
            Amount = _amount,
            ItemPricingCategoryId = _categoryId,
            IsActive = true,
            PricingCategory = PricingCategory.Consultation,
            FacilityId = _facilityId
            

        };
        return _itemPricing;
    }
    public TestInvoiceItemsPricingBuilder Save(EHRDbContext context)
    {
        context.ItemPricing.Add(Build());
        context.SaveChanges();
        return this;
    }
    
    
}
