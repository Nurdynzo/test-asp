using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.EntityFrameworkCore.EntityConfiguration
{
    public class PatientRelationConfiguration : IEntityTypeConfiguration<PatientRelation>
    {

        public void Configure(EntityTypeBuilder<PatientRelation> builder)
        {
            builder.HasMany(p => p.Diagnoses)
            .WithOne(pr => pr.PatientRelation)
            .HasForeignKey(pr => pr.PatientRelationId).IsRequired(false);
        }
    }
}
