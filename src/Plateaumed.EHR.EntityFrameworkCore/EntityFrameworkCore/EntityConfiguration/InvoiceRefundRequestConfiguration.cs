using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Invoices;
namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration
{
    public class InvoiceRefundRequestConfiguration: IEntityTypeConfiguration<InvoiceRefundRequest>
    {

        public void Configure(EntityTypeBuilder<InvoiceRefundRequest> builder)
        {
            builder.Property(x => x.RowVersion).IsConcurrencyToken();
        }
    }
}