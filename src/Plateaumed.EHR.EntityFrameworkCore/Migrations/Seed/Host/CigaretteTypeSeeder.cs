using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public static class CigaretteTypeSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            var existingSuggestions = context.TobaccoSuggestions.ToList();
            var filteredSuggestion = GetTobaccoSuggestions().Where(x => existingSuggestions
            .All(y => y.SnomedId != x.SnomedId)).ToList();
            context.TobaccoSuggestions.AddRange(filteredSuggestion);
            context.SaveChanges();
            
        }

        private static List<TobaccoSuggestion> GetTobaccoSuggestions()
        {
            return new List<TobaccoSuggestion>
            {
                new()
                {
                    ModeOfConsumption = "Chewing tobacco",
                    SnomedId = 81911001
                },
                new()
                {
                    ModeOfConsumption = "Cigar smoking tobacco",
                    SnomedId = 26663004
                },
                new()
                {
                    ModeOfConsumption = "Cigarette smoking tobacco",
                    SnomedId = 66562002
                },
                new()
                {
                    ModeOfConsumption = "Pipe smoking tobacco",
                    SnomedId = 84498003
                },
                new()
                {
                    ModeOfConsumption = "Snuff tobacco",
                    SnomedId = 39789004
                }
            };
        }
    }
}
