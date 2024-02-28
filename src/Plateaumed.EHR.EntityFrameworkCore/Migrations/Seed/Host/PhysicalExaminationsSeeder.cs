using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Json.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class PhysicalExaminationsSeeder
{
    public static void Seed(EHRDbContext dbContext)
    {
        // seed in the examination
        var physicalExaminationTypeJson = GeneralPhysicalExaminationTypeJson.jsonData;
        var physicalExaminationTypes = JsonConvert.DeserializeObject<List<PhysicalExaminationType>>(physicalExaminationTypeJson);
        if (!dbContext.PhysicalExaminationTypes.Any())
            physicalExaminationTypes.ForEach(v => dbContext.PhysicalExaminationTypes.Add(v));
        dbContext.SaveChanges();
        
        // seed physical examinations and link foreign key
        var generalJson = GeneralPhysicalExaminationJson.jsonData;
        var general = JsonConvert.DeserializeObject<List<PhysicalExamination>>(generalJson);
        if (!dbContext.PhysicalExaminations.Any(x => x.Type == "General"))
        {
            var typeId = dbContext.PhysicalExaminationTypes.FirstOrDefault(v => v.Type == "General")?.Id;
            general.ForEach(g =>
            {
                g.PhysicalExaminationTypeId = typeId;
                dbContext.PhysicalExaminations.Add(g);
            });
        }

        var cardiovascularJson = CardiovascularExaminationJson.jsonData;
        var cardiovascular = JsonConvert.DeserializeObject<List<PhysicalExamination>>(cardiovascularJson);
        if (!dbContext.PhysicalExaminations.Any(x => x.Type == "Cardiovascular"))
        {
            var typeId = dbContext.PhysicalExaminationTypes.FirstOrDefault(v => v.Type == "Cardiovascular")?.Id;
            cardiovascular.ForEach(c =>
            {
                c.PhysicalExaminationTypeId = typeId;
                dbContext.PhysicalExaminations.Add(c);
            });
        }

        var neurologyJson = NeurologyExaminationJson.jsonData;
        var neurology = JsonConvert.DeserializeObject<List<PhysicalExamination>>(neurologyJson);
        if (!dbContext.PhysicalExaminations.Any(x => x.Type == "Neurology"))
        {
            var typeId = dbContext.PhysicalExaminationTypes.FirstOrDefault(v => v.Type == "Neurology")?.Id;
            neurology.ForEach(n =>
            {
                n.PhysicalExaminationTypeId = typeId;
                dbContext.PhysicalExaminations.Add(n);
            });
        }

        var gynaecologyJson = GynaecologyExaminationJson.jsonData;
        var gynaecology = JsonConvert.DeserializeObject<List<PhysicalExamination>>(gynaecologyJson);
        if (!dbContext.PhysicalExaminations.Any(x => x.Type == "Gynaecology"))
        {
            var typeId = dbContext.PhysicalExaminationTypes.FirstOrDefault(v => v.Type == "Gynaecology")?.Id;
            gynaecology.ForEach(g =>
            {
                g.PhysicalExaminationTypeId = typeId;
                dbContext.PhysicalExaminations.Add(g);
            });
        }

        var obstetricsJson = ObstetricsExaminationJson.jsonData;
        var obstetrics = JsonConvert.DeserializeObject<List<PhysicalExamination>>(obstetricsJson);
        if (!dbContext.PhysicalExaminations.Any(x => x.Type == "Obstetrics"))
        {
            var typeId = dbContext.PhysicalExaminationTypes.FirstOrDefault(v => v.Type == "Obstetrics")?.Id;
            obstetrics.ForEach(o =>
            {
                o.PhysicalExaminationTypeId = typeId;
                dbContext.PhysicalExaminations.Add(o);
            });
        }
        
        // add the qualifiers
        var qualifiersJson = PhysicalExaminationQualifiersJson.jsonData;
        var qualifiers = JsonConvert.DeserializeObject<List<ExaminationQualifier>>(qualifiersJson);
        if (!dbContext.ExaminationQualifiers.Any())
        {
            qualifiers.ForEach(q => dbContext.ExaminationQualifiers.Add(q));
        }
        dbContext.SaveChanges();
    }
}