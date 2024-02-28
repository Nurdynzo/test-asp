using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration;

public class PatientEntityConfiguration : IEntityTypeConfiguration<Patient> 
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasOne(p => p.UserFk)
            .WithOne(u => u.PatientFk)
            .HasForeignKey<Patient>(p => p.UserId).IsRequired(false);

        builder.HasMany(p => p.Relations)
            .WithOne(r => r.PatientFk)
            .HasForeignKey(r => r.PatientId).IsRequired(false);

        builder.HasMany(p => p.Insurers)
            .WithOne(i => i.PatientFk)
            .HasForeignKey(i => i.PatientId).IsRequired(false);

        builder.HasMany(p => p.ReferralDocuments)
            .WithOne(pr => pr.PatientFk)
            .HasForeignKey(pr => pr.PatientId).IsRequired(false);
        
        builder.HasMany(p => p.PatientOccupations)
            .WithOne(pr => pr.Patient)
            .HasForeignKey(pr => pr.PatientId).IsRequired(false);
    }
}