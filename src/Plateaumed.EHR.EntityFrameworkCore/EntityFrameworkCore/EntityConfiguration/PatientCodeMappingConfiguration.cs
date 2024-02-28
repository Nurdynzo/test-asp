using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration;

public class PatientCodeMappingConfiguration : IEntityTypeConfiguration<PatientCodeMapping>
{
    public void Configure(EntityTypeBuilder<PatientCodeMapping> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PatientCode).IsRequired();

        builder.HasOne(x =>x.PatientFk)
            .WithMany(x=>x.PatientCodeMappings)
            .HasForeignKey(x=>x.PatientId)
            .IsRequired(false);
        
        builder.HasOne(x => x.FacilityFk)
            .WithMany(x => x.PatientCodeMappings)
            .HasForeignKey(x=>x.FacilityId)
            .IsRequired(false);
        
    }
}