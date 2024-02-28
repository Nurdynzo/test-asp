using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public static class VaccinationSuggestionSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            var existingSuggestions = context.VaccinationSuggestions.ToList();
            var filteredSuggestion = GetVaccinationSuggestions().Where(x => existingSuggestions
            .All(a => a.SnomedId != x.SnomedId && a.Name != x.Name)).ToList();
            context.VaccinationSuggestions.AddRange(filteredSuggestion);
            context.SaveChanges();
        }

        private static List<VaccinationSuggestion> GetVaccinationSuggestions()
        {
            return new List<VaccinationSuggestion>
            {
                new VaccinationSuggestion()
                {
                    Name = "Bacillus Calmette-Guerin (BCG)",
                    SnomedId = 319941000221104
                },
                new VaccinationSuggestion()
                {
                    Name = "Hepatitis B",
                    SnomedId = 836374004
                },
                new()
                {
                    Name = "Influenza",
                    SnomedId = 1181000221105
                },
                new()
                {
                    Name = "Messles, Mumps, Rubella (MMR)",
                    SnomedId = 2241000221103
                },
                new()
                {
                    Name = "Pneumococcus",
                    SnomedId = 28531000087107
                },
                new()
                {
                    Name = "Varicella",
                    SnomedId = 871919004
                },
                new()
                {
                    Name = "Yellow fever",
                    SnomedId = 1121000221106
                },
                new()
                {
                    Name = "Pentavalent",
                    SnomedId = 871886002
                },
                new()
                {
                    Name = "Rabies",
                    SnomedId = 1131000221109
                },
                new()
                {
                    Name = "Tetanus",
                    SnomedId = 2021000221101
                },
                new()
                {
                    Name = "Rotavirus",
                    SnomedId = 1081000221109
                },
                new()
                {
                    Name = "Preumococcal Conjugato Vaccine",
                    SnomedId = 981000221107
                },
                new()
                {
                    Name = "Measles",
                    SnomedId = 871766009
                },
                new()
                {
                    Name = "Oral Polio Vaccine",
                    SnomedId = 1051000221104
                },
                new()
                {
                    Name = "Inactivated Polio Vaccino",
                    SnomedId = 871740006
                },
                new()
                {
                    Name = "Cholera",
                    SnomedId = 2181000221101
                },
                new()
                {
                    Name = "Covid-19",
                    SnomedId = 1119305005
                }
            };
        }
    }
}
