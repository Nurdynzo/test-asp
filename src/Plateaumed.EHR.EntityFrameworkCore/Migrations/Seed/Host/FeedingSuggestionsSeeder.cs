using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json.Feeding;
using Plateaumed.EHR.Misc.Json.VitalSigns;
using Plateaumed.EHR.VitalSigns;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class FeedingSuggestionsSeeder
    {
        public static void Seed(EHRDbContext dbContext)
        {
            var feedingSuggestionsJson = FeedingSuggestionsJson.jsonData;
            var vitalSigns = JsonConvert.DeserializeObject<List<FeedingSuggestions>>(feedingSuggestionsJson);
            if (!dbContext.FeedingSuggestions.Any())
            {
                vitalSigns.ForEach(v => dbContext.FeedingSuggestions.Add(v));
            }
            
            dbContext.SaveChanges();
        }
    }
}
