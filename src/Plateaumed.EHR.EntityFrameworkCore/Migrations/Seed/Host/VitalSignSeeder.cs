using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json.VitalSigns;
using Plateaumed.EHR.VitalSigns;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class VitalSignSeeder
    {
        public static void Seed(EHRDbContext dbContext)
        {
            var vitalSignsJson = VitalSignsJson.jsonData;
            var vitalSigns = JsonConvert.DeserializeObject<List<VitalSign>>(vitalSignsJson);
            if (!dbContext.VitalSigns.Any())
            {
                vitalSigns.ForEach(v => dbContext.VitalSigns.Add(v));
            }

            var gcsScoringJson = GCSScoringJson.jsonData;
            var gcsScoring = JsonConvert.DeserializeObject<List<GCSScoring>>(gcsScoringJson);
            if (!dbContext.GCSScorings.Any())
            {
                gcsScoring.ForEach(g => dbContext.GCSScorings.Add(g));
            }

            var apgarScoringJson = ApgarScoringJson.jsonData;
            var apgarScoring = JsonConvert.DeserializeObject<List<ApgarScoring>>(apgarScoringJson);
            if (!dbContext.ApgarScorings.Any())
            {
                apgarScoring.ForEach(a => dbContext.ApgarScorings.Add(a));
            }
            dbContext.SaveChanges();
        }
    }
}
