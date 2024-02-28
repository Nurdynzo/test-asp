using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class DiagnosisSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if (context == null) return;
            var existingDiagnosis = context.DiagnosisSuggestions.ToList();
            var filteredDiagnosis = GetDiagnosisSuggestions().Where(x => existingDiagnosis
                .All(e => e.SnomedId != x.SnomedId)).ToList();
            context.DiagnosisSuggestions.AddRange(filteredDiagnosis);
            context.SaveChanges();
        }


        private static List<DiagnosisSuggestion> GetDiagnosisSuggestions()
        {
            return new List<DiagnosisSuggestion>()
            {
                new DiagnosisSuggestion
                {
                    Name = "Myocardial Infarction",
                    SnomedId = 22298006
                },
                new DiagnosisSuggestion
                {
                    Name = "Hepatitis B",
                    SnomedId = 66071002
                },
                new DiagnosisSuggestion
                {
                    Name = "Tuberculosis",
                    SnomedId = 56717001
                },
                new DiagnosisSuggestion
                {
                    Name = "Hypertension",
                    SnomedId = 38341003
                },
                new DiagnosisSuggestion
                {
                    Name = "Rheumatic Fever",
                    SnomedId = 58718002
                },
                new DiagnosisSuggestion
                {
                    Name = "Epilepsy",
                    SnomedId = 84757009
                },
                new DiagnosisSuggestion
                {
                    Name = "Asthma",
                    SnomedId = 195967001
                },
                new DiagnosisSuggestion
                {
                    Name = "Diabetes",
                    SnomedId = 73211009
                },
                new DiagnosisSuggestion
                {
                    Name = "Stroke",
                    SnomedId = 230690007
                }
            };
        }
    }
}
