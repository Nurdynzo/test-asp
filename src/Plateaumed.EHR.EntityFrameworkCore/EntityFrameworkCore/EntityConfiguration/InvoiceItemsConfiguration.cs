using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration
{
    public class InvoiceItemsConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {

        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.Property(x => x.RowVersion).IsConcurrencyToken();
        }
    }
}