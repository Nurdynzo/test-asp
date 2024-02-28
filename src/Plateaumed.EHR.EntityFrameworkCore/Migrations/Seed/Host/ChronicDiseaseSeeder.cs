using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public static class ChronicDiseaseSeeder
{

    public static void Seed(EHRDbContext dbContext)
    {
        if(dbContext == null) return;
        var existingChronicDiseases = dbContext.ChronicDiseases.ToList();
        var filterOutExistingChronicDiseases = GetChronicConditions().Where(x=> existingChronicDiseases.All(y => y.SynonymSnomedId != x.SynonymSnomedId)).ToList();
        dbContext.ChronicDiseases.AddRange(filterOutExistingChronicDiseases);
        dbContext.SaveChanges();
        
    }

    private static List<ChronicDisease> GetChronicConditions()
    {
        return new List<ChronicDisease>()
        {
            new()
            {
                Suggestion = "Myocardial Infarction",
                Synonym = "Coronary Arteriosclerosis",
                SnomedId = 22298006,
                SynonymSnomedId = 53741008,
                IsPrimaryFormat = false
            },
            new()
            {
                Suggestion = "Hepatitis B",
                Synonym = "Viral Hepatitis",
                SnomedId = 66071002,
                SynonymSnomedId = 3738000,
                IsPrimaryFormat = true
            },
            new()
            {
                Suggestion = "Tuberculosis",
                Synonym = string.Empty,
                SnomedId = 56717001,
                SynonymSnomedId = null,
                IsPrimaryFormat = true
            },
            new()
            {
                Suggestion = "Hypertension",
                Synonym = "Hypertensive Heart Disease",
                SnomedId = 38241003,
                SynonymSnomedId = 64715009,
                IsPrimaryFormat = true
                
            },
            new()
            {
                Suggestion = "Hypertension",
                Synonym = "Pregnancy-Induced Hypertension",
                SnomedId = 38241003,
                SynonymSnomedId = 48194001,
                IsPrimaryFormat = true
            },
            new()
            {
                Suggestion = "Rheumatic Fever",
                Synonym = "Rheumatism",
                SnomedId = 58718002,
                SynonymSnomedId = 396332003,
                IsPrimaryFormat = true
            },
            new()
            {
                Suggestion = "Epilepsy",
                Synonym = "Seizure disorder",
                SnomedId = 84757009,
                SynonymSnomedId = 91175000,
                IsPrimaryFormat = true
            },
            new()
            {
                Suggestion = "Asthma",
                Synonym = "COPD - Chronic Obstructive Pulmonary Disease",
                SnomedId = 195967001,
                SynonymSnomedId = 13645005,
                IsPrimaryFormat = true
                
            },
            new()
            {
                Suggestion = "Diabetes",
                Synonym = "Gestational diabetes mellitus",
                SnomedId = 73211009,
                SynonymSnomedId = 11687002,
                IsPrimaryFormat = true
            },
            new()
            {
                Suggestion = "Stroke",
                Synonym = "Cerebral infarction",
                SnomedId = 230690007,
                SynonymSnomedId = 432504007,
                IsPrimaryFormat = false
                
            }

        };
    }
}