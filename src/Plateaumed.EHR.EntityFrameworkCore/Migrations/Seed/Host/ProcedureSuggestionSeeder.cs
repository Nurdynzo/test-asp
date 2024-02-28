using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class ProcedureSuggestionSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if (context == null) return;
            var existingProcedure = context.ProcedureSuggestions.ToList();
            var filteredProcedures = GetProcedureSuggestions()
                .Where(x => existingProcedure.All(e => e.SnomedId != x.SnomedId)).ToList();
            context.ProcedureSuggestions.AddRange(filteredProcedures);
            context.SaveChanges();
        }


        private static List<ProcedureSuggestion> GetProcedureSuggestions()
        {
            return new List<ProcedureSuggestion>
            {
                new()
                {
                    Name = "Trauma- Road Traffic Injuries",
                    SnomedId = 214031005
                },
                new()
                {
                    Name = "Trauma- Gunshot Injury",
                    SnomedId = 283545005
                },
                new()
                {
                    Name = "Trauma- Burns",
                    SnomedId = 125666000
                },
                new()
                {
                    Name = "Trauma- Falls",
                    SnomedId = 1912002
                },
                new()
                {
                    Name = "Acute Appendicitis",
                    SnomedId = 85189001
                },
                new()
                {
                    Name = "Acute Intestinal Obstruction",
                    SnomedId = 197078006
                },
                new()
                {
                    Name = "Typhoid Perforation",
                    SnomedId = 789006005
                },
                new()
                {
                    Name = "Acute urinary retention",
                    SnomedId = 236648008
                },
                new()
                {
                    Name = "Enterocutaneous fistula",
                    SnomedId = 197247001
                },
                new()
                {
                    Name = "Acute testicular torsion",
                    SnomedId = 81996005
                },
                new()
                {
                    Name = "Acute low back pain",
                    SnomedId = 279039007
                },
                new()
                {
                    Name = "Limb gangrene",
                    SnomedId = 240108006
                },
                new()
                {
                    Name = "Malignancy",
                    SnomedId = 1240414004
                },
                new()
                {
                    Name = "Upper GI bleeding",
                    SnomedId = 37372002
                },
                new()
                {
                    Name = "Perforated Peptic Ulcer Disease",
                    SnomedId = 88169003
                },
                new()
                {
                    Name = "Cholecystitis",
                    SnomedId = 76581006
                },
                new()
                {
                    Name = "Ovarian torsion",
                    SnomedId = 13595002
                },
                new()
                {
                    Name = "Ectopic Pregnancy",
                    SnomedId = 34801009
                },
                new()
                {
                    Name = "Post Partum Haemorrhage",
                    SnomedId = 47821001
                },
                new()
                {
                    Name = "Ruptured Ovarian Cyst",
                    SnomedId = 95598005
                }
            };
        }
    }
}
