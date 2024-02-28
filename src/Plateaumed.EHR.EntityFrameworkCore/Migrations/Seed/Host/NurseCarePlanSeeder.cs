using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json.NurseCarePlan;
using Plateaumed.EHR.NurseCarePlans;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class NurseCarePlanSeeder
{
    public static void Seed(EHRDbContext dbContext)
    {
        var carePlanJson = NurseCarePlanJson.jsonData;
        var diagnoses = JsonConvert.DeserializeObject<List<NursingDiagnosis>>(carePlanJson);
        if (!dbContext.NursingDiagnosis.Any())
        {
            diagnoses.ForEach(d => dbContext.NursingDiagnosis.Add(d));
        }

        dbContext.SaveChanges();
        
        var evaluationsJson = NurseEvaluationJson.jsonData;
        var evaluations = JsonConvert.DeserializeObject<List<NursingEvaluation>>(evaluationsJson);
        if (!dbContext.NursingEvaluation.Any())
        {
            evaluations.ForEach(d => dbContext.NursingEvaluation.Add(d));
        }

        dbContext.SaveChanges();
    }
}