using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Invoices;

namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration;

public class ItemPricingCategoryConfiguration: IEntityTypeConfiguration<ItemPricingCategory>
{
    public void Configure(EntityTypeBuilder<ItemPricingCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x=>x.ItemPricing)
            .WithOne(x=>x.ItemPricingCategory)
            .HasForeignKey(x=>x.ItemPricingCategoryId)
            .IsRequired(false);
    }
}