using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class PatientImplantSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if (context == null) return;
            var existingImplantSuggestion = context.PatientImplantSuggestions.ToList();
            var filteredSuggestions = GetImplantSuggestions().Where(x =>
            existingImplantSuggestion.All(e => e.SnomedId !=  x.SnomedId)).ToList();
            context.PatientImplantSuggestions.AddRange(filteredSuggestions);
            context.SaveChanges();

        }


        private static List<PatientImplantSuggestion> GetImplantSuggestions()
        {
            return new List<PatientImplantSuggestion>()
            {
                new()
                {
                    Name = "Breast",
                    SnomedId = 76752008
                },
                new()
                {
                    Name = "Jaw",
                    SnomedId = 661005
                },
                new()
                {
                    Name = "Cochlear",
                    SnomedId = 80169004
                },
                new()
                {
                    Name = "Middle ear",
                    SnomedId = 25342003
                },
                new()
                {
                    Name = "Heart valve",
                    SnomedId = 17401000
                },
                new()
                {
                    Name = "Gluteal muscle",
                    SnomedId = 102291007
                },
                new()
                {
                    Name = "Hip joint",
                    SnomedId = 24136001
                },
                new()
                {
                    Name = "Orbit",
                    SnomedId = 363654007
                },
                new()
                {
                    Name = "Palate",
                    SnomedId = 72914001
                },
                new()
                {
                    Name = "Tongue",
                    SnomedId = 21974007
                }
            };
        }
    }
}
