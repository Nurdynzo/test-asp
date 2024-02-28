using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {

        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.RowVersion).IsConcurrencyToken();
        }
    }
}