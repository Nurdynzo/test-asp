using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class PatientGynaecologicalIllnessSuggestionSeeder
    {
        public static void Seed(EHRDbContext context)
        {
          if(context is null) return;
          var existingPatientGynaecologicalIllnessSuggestions = context.PatientGynaecologicalIllnessSuggestions.ToList();
          var filterOutExistingPatientGynaecologicalIllnessSuggestions = GetPatientGynaecologicalIllnessSuggestion()
              .Where(x=> existingPatientGynaecologicalIllnessSuggestions.All(y => y.SnomedId != x.SnomedId))
              .ToList();
          context.PatientGynaecologicalIllnessSuggestions.AddRange(filterOutExistingPatientGynaecologicalIllnessSuggestions);
          context.SaveChanges();
            
        }
        
        public static List<PatientGynaecologicalIllnessSuggestion> GetPatientGynaecologicalIllnessSuggestion()
        {
            return new List<PatientGynaecologicalIllnessSuggestion>()
            {
                new()
                {
                    Name = "Ectopic pregnancy",
                    SnomedId = 34801009
                },
                new()
                {
                    Name = "Sexually transmitted infections",
                    SnomedId = 8098009
                    
                },
                new ()
                {
                    Name = "Endometriosis",
                    SnomedId = 129103003
                },
                new()
                {
                    Name = "Bartholin’s cyst",
                    SnomedId = 57044006
                    
                },
                new()
                {
                    Name = "Cervical ectropion",
                    SnomedId = 79342006
                },
                new()
                {
                    Name = "Malignancy",
                    SnomedId = 1240414004
                }
            };
        }
    }
}