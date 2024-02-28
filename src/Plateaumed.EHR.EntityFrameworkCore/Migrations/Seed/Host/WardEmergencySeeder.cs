using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json.WardEmergencies;
using Plateaumed.EHR.WardEmergencies;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class WardEmergencySeeder
{
    public static void Seed(EHRDbContext dbContext)
    {
        var wardEmergenciesJson = WardEmergenciesJson.jsonData;
        var wardEmergencies = JsonConvert.DeserializeObject<List<WardEmergency>>(wardEmergenciesJson);
        if (!dbContext.WardEmergency.Any())
        {
            wardEmergencies.ForEach(v => dbContext.WardEmergency.Add(v));
        }
        
        dbContext.SaveChanges();
    }
}