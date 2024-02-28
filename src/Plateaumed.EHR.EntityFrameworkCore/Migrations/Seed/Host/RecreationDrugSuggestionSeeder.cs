using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public static class RecreationDrugSuggestionSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if (context == null) return;
            var existingSuggestions = context.RecreationalDrugSuggestions.ToList();
            var filteredSuggestion = GetRecreationalDrugsSuggestions()
                .Where(x => existingSuggestions.All(c => c.SnomedId != x.SnomedId)).ToList();
            context.RecreationalDrugSuggestions.AddRange(filteredSuggestion);
            context.SaveChanges();
        }


        private static List<RecreationalDrugSuggestion> GetRecreationalDrugsSuggestions()
        {
            return new List<RecreationalDrugSuggestion>
            {
                new()
                {
                    Name = "Cannabis",
                    SnomedId = 398705004
                },
                new()
                {
                    Name = "MDMA",
                    SnomedId = 288459003
                },
                new()
                {
                    Name = "Codeine",
                    SnomedId = 387494007
                },
                new()
                {
                    Name = "Cocaine",
                    SnomedId = 387085005
                },
                new()
                {
                    Name = "Heroin",
                    SnomedId = 387341002
                },
                new()
                {
                    Name = "Other"
                }
            };
        }
    }
}
