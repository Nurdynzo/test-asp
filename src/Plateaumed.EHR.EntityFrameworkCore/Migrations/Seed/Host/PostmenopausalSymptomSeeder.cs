using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class PostmenopausalSymptomSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if(context == null) return;
            var existingPostmenopausalSymptoms = context.PostmenopausalSymptomSuggestions.ToList();
            var filterOutExistingPostmenopausalSymptoms = GetPostmenopausalSymptoms()
                .Where(x=> 
                    existingPostmenopausalSymptoms.All(y => y.SnomedId != x.SnomedId))
                .ToList();
            context.PostmenopausalSymptomSuggestions.AddRange(filterOutExistingPostmenopausalSymptoms);
            context.SaveChanges();
        }

        private static List<PostmenopausalSymptomSuggestion> GetPostmenopausalSymptoms()
        {
            return new List<PostmenopausalSymptomSuggestion>()
            {
                new()
                {
                    SnomedId = 198438009,
                    Name = "Headaches",
                },
                new()
                {
                    SnomedId = 31908003,
                    Name = "Vaginal dryness"
                },
                new()
                {
                    SnomedId = 198436008,
                    Name = "Flushing"
                },
                new()
                {
                    SnomedId = 84788008,
                    Name = "Depression"
                },
                new()
                {
                    SnomedId = 1254878004,
                    Name = "Excessive sweating"
                },
                new()
                {
                    SnomedId = 198437004,
                    Name = "Sleeplessness"
                },
                new()
                {
                    SnomedId = 125605004,
                    Name = "Fracture"
                    
                },
                new()
                {
                    SnomedId = 301783004,
                    Name = "Abnormal bleeding"
                },
                new()
                {
                    SnomedId = 198439001,
                    Name = "Lack of concentration"
                },
                new()
                {
                    SnomedId = 373717006,
                    Name = "Premature menopause"
                }

            };
        }
    }
}