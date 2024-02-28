using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public static class AllergyReactionExperiencedSeeder
{
    public static void Seed(EHRDbContext dbContext)
    {
        if(dbContext == null) return;
        var existingAllergyReactionExperiencedSuggestions = dbContext.AllergyReactionExperiencedSuggestions.ToList();
        var filterOutExistingAllergyReactionExperiencedSuggestions = GetAllergyReactionExperiencedSuggestions()
            .Where(x=> existingAllergyReactionExperiencedSuggestions.All(y => y.SnomedId != x.SnomedId)).ToList();
        dbContext.AllergyReactionExperiencedSuggestions.AddRange(filterOutExistingAllergyReactionExperiencedSuggestions);
        dbContext.SaveChanges();
        
    }

    private static List<AllergyReactionExperiencedSuggestion> GetAllergyReactionExperiencedSuggestions()
    {
        return new List<AllergyReactionExperiencedSuggestion>()
        {
            new()
            {
                SnomedId = 76067001,
                ReactionName = "Sneezing",
            },
            new()
            {
                SnomedId = 420103007,
                ReactionName = "Watery eyes",
            },
            new()
            {
                SnomedId = 64531003,
                ReactionName = "Watery nose",
            },
            new()
            {
                SnomedId = 40178009,
                ReactionName = "Itching",
            },
            new()
            {
                SnomedId = 402387002,
                ReactionName = "Body Swelling",
            },
            new()
            {
              SnomedId   = 49237006,
              ReactionName = "Diarrhoea",
            },
            new ()
            {
                SnomedId = 55985003,
                ReactionName = "Atopic skin reaction"
            },
            new()
            {
             SnomedId   = 23924001,
             ReactionName = "Chest tightness"
            },
            new ()
            {
                SnomedId = 267036007,
                ReactionName = "Shortness of breath"
            },
            new ()
            {
                SnomedId = 4448006,
                ReactionName = "Headache"
            },
            new ()
            {
                SnomedId = 249497008,
                ReactionName = "Vomiting"
            }
        };
    }
}