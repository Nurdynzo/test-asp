using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class ReviewOfSystemsSuggestionSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if (context == null) return;
            var existingSuggestions = context.ReviewOfSystemsSuggestions.ToList();
            var filteredSuggestions = GetReviewOfSystemsSuggestions().Where(x =>
                existingSuggestions.All(e => e.SnomedId != x.SnomedId)).ToList();
            context.ReviewOfSystemsSuggestions.AddRange(filteredSuggestions);
            context.SaveChanges();
        }

        private static List<ReviewOfSystemsSuggestion> GetReviewOfSystemsSuggestions()
        {
            return new List<ReviewOfSystemsSuggestion>
            {
                new()
                {
                    Name = "Fevers",
                    SnomedId = 386661006,
                    Category = SymptomsCategory.Systemic
                },
                new()
                {
                    Name = "Night sweats",
                    SnomedId = 42984000,
                    Category = SymptomsCategory.Systemic
                },
                new()
                {
                    Name = "Weight change",
                    SnomedId = 365921005,
                    Category = SymptomsCategory.Systemic
                },
                new()
                {
                    Name = "Excessive weight loss",
                    SnomedId = 309257005,
                    Category = SymptomsCategory.Systemic
                },
                new()
                {
                    Name = "Excessive weight gain",
                    SnomedId = 224994002,
                    Category = SymptomsCategory.Systemic
                },
                new()
                {
                    Name = "Fatigue",
                    SnomedId = 84229001,
                    Category = SymptomsCategory.Systemic
                },
                new()
                {
                    Name = "Chest pain",
                    SnomedId = 29857009,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Palpitations",
                    SnomedId = 80313002,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Dyspnoea",
                    SnomedId = 267036007,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Pre-syncope",
                    SnomedId = 427461000,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Syncope",
                    SnomedId = 271594007,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Orthopnoea",
                    SnomedId = 62744007,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Peripheral oedema",
                    SnomedId = 82014009,
                    Category = SymptomsCategory.Cardiovascular
                },
                new()
                {
                    Name = "Dyspnoea",
                    SnomedId = 267036007,
                    Category = SymptomsCategory.Respiratory
                },
                new()
                {
                    Name = "Cough",
                    SnomedId = 49727002,
                    Category = SymptomsCategory.Respiratory
                },
                new()
                {
                    Name = "Sputum",
                    SnomedId = 28743005,
                    Category = SymptomsCategory.Respiratory
                },
                new()
                {
                    Name = "Wheeze",
                    SnomedId = 56018004,
                    Category = SymptomsCategory.Respiratory
                },
                new()
                {
                    Name = "Haemoptysis",
                    SnomedId = 66857006,
                    Category = SymptomsCategory.Respiratory
                },
                new()
                {
                    Name = "Pleuritic chest pain",
                    SnomedId = 13057000,
                    Category = SymptomsCategory.Respiratory
                },
                new()
                {
                    Name = "Appetite change",
                    SnomedId = 249473004,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Nausea",
                    SnomedId = 162057007,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Vomiting",
                    SnomedId = 422400008,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Dyspepsia",
                    SnomedId = 162031009,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Dysphagia",
                    SnomedId = 40739000,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Weight loss",
                    SnomedId = 309257005,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Abdominal pain",
                    SnomedId = 21522001,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Abdominal distension",
                    SnomedId = 162037008,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Jaundice",
                    SnomedId = 18165001,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Constipation",
                    SnomedId = 14760008,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Diarrhoea",
                    SnomedId = 62315008,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Steatorrhoea",
                    SnomedId = 66187002,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Melaena",
                    SnomedId = 2901004,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Hematochezia",
                    SnomedId = 405729008,
                    Category = SymptomsCategory.Gastrointestinal
                },
                new()
                {
                    Name = "Oliguria",
                    SnomedId = 83128009,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Polyuria",
                    SnomedId = 28442001,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Anuria",
                    SnomedId = 2472002,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Dark urine",
                    SnomedId = 39575007,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Hematuria",
                    SnomedId = 34436003,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Urinary frequency",
                    SnomedId = 162116003,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Dysuria",
                    SnomedId = 49650001,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Flank pain", 
                    SnomedId = 247355005,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Pelvic pain",
                    SnomedId = 274671002,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Urinary urgency",
                    SnomedId = 75088002,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Urinary incontinence", 
                    SnomedId = 165232002,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Terminal dribbling",
                    SnomedId = 162130008,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Nocturia",
                    SnomedId = 139394000,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Fatigue",
                    SnomedId = 84229001,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Nausea",
                    SnomedId = 162057007,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Anorexia",
                    SnomedId = 79890006,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Pruritus",
                    SnomedId = 418363000,
                    Category = SymptomsCategory.Genitourinary
                },
                new()
                {
                    Name = "Trauma",
                    SnomedId = 417746004,
                    Category = SymptomsCategory.Musculoskeletal
                },
                new()
                {
                    Name = "Bone pain",
                    SnomedId = 12584003,
                    Category = SymptomsCategory.Musculoskeletal
                },
                new()
                {
                    Name = "Joint pain",
                    SnomedId = 57676002,
                    Category = SymptomsCategory.Musculoskeletal
                },
                new()
                {
                    Name = "Muscular pain",
                    SnomedId = 68962001,
                    Category = SymptomsCategory.Musculoskeletal
                },
                new()
                {
                    Name = "Rash present",
                    SnomedId = 1806006,
                    Category = SymptomsCategory.Dermatological
                },
                new()
                {
                    Name = "Skin lesions",
                    SnomedId = 95324001,
                    Category = SymptomsCategory.Dermatological
                },
                new()
                {
                    Name = "Skin colour changes",
                    SnomedId = 402604004,
                    Category = SymptomsCategory.Dermatological
                },
                new()
                {
                    Name = "Ulcers",
                    SnomedId = 429040005,
                    Category = SymptomsCategory.Dermatological
                },
                new()
                {
                    Name = "Changes in colour vision",
                    SnomedId = 367469000,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Blurred vision",
                    SnomedId = 111516008,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Sudden loss of vision",
                    SnomedId = 15203004,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Floaters",
                    SnomedId = 162278001,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Unilateral headache",
                    SnomedId = 162300006,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Bilateral headache",
                    SnomedId = 162301005,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Headache",
                    SnomedId = 25064002,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Thunderclap Headache",
                    SnomedId = 95660002,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Migraine",
                    SnomedId = 37796009,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Muscle weakness",
                    SnomedId = 26544005,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Numbness",
                    SnomedId = 44077006,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Paraesthesia",
                    SnomedId = 91019004,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Loss of consciousness",
                    SnomedId = 419045004,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Seizures",
                    SnomedId = 91175000,
                    Category = SymptomsCategory.Neurological
                },
                new()
                {
                    Name = "Confusion",
                    SnomedId = 40917007,
                    Category = SymptomsCategory.Neurological
                }
            };
        }
    }
}
