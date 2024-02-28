using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public static class AllergyTypeSeeder
{
    public static void Seed(EHRDbContext dbContext)
    {
        if(dbContext == null) return;
        var existingAllergyTypes = dbContext.PatientAllergyTypeSuggestions.ToList();
        var filterOutExistingAllergyTypes = GetAllergyTypes()
            .Where(x=> existingAllergyTypes.All(y => y.SnomedId != x.SnomedId))
            .ToList();
        dbContext.PatientAllergyTypeSuggestions.AddRange(filterOutExistingAllergyTypes);
        dbContext.SaveChanges();
    }

    private static List<PatientAllergyTypeSuggestion> GetAllergyTypes()
    {
        return new List<PatientAllergyTypeSuggestion>()
        {
            new()
            {
                SnomedId = 255620007,
                Name = "Food",
            },
            new()
            {
                SnomedId = 763158003,
                Name = "Medication"
            },
            new()
            {
                SnomedId = 276339004,
                Name = "Environmental"
            }
        };
    }
}