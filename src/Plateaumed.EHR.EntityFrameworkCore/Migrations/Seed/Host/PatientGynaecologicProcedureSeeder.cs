using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class PatientGynaecologicProcedureSeeder
    {
        public static void Seed(EHRDbContext dbContext)
        {
            if(dbContext == null) return;
            var existingGynaecologicProcedures = dbContext.PatientGynaecologicProcedureSuggestions.ToList();
            var filterOutExistingGynaecologicProcedures = GetGynaecologicProcedures()
                .Where(x=> existingGynaecologicProcedures.All(y => y.SnomedId != x.SnomedId)).ToList();
            dbContext.PatientGynaecologicProcedureSuggestions.AddRange(filterOutExistingGynaecologicProcedures);
            dbContext.SaveChanges();
            
        }
        private static List<PatientGynaecologicProcedureSuggestion> GetGynaecologicProcedures()
        {
            return new List<PatientGynaecologicProcedureSuggestion>()
            {
                new()
                {
                    Name = "Abdominal or pelvic surgery",
                },
                new()
                {
                    SnomedId = 11466000,
                    Name = "Caesarian section "
                },
                new()
                {
                    SnomedId = 176761007,
                    Name = "Loop excision of the transitional zone (LETZ) "
                },
                new ()
                {
                    SnomedId = 238034001,
                    Name = "Vaginal prolapse repair"
                },
                new ()
                {
                    SnomedId = 236886002,
                    Name = "Hysterectomy"
                    
                }
            };
        }
    }
}