using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration;

public class SerialCodeConfiguration: IEntityTypeConfiguration<SerialCode>
{
    public void Configure(EntityTypeBuilder<SerialCode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LastGeneratedNo).HasDefaultValue(0);
    }
}