using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.PatientWallet;
namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration
{
    public class WalletConfigurations : IEntityTypeConfiguration<Wallet>
    {

        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.Property(x => x.RowVersion).IsConcurrencyToken();
        }
    }
}