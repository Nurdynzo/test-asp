using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.PatientWallet;

namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration;

public class WalletHistoryConfiguration: IEntityTypeConfiguration<WalletHistory>
{
    public void Configure(EntityTypeBuilder<WalletHistory> builder)
    {
        builder.HasKey(x => x.Id);
       builder.HasOne(x=>x.Wallet)
           .WithMany(x=>x.History)
           .HasForeignKey(x=>x.WalletId).IsRequired(false);
    }
}