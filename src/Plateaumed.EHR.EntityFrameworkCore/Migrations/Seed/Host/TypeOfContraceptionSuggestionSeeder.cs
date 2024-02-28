using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class TypeOfContraceptionSuggestionSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            if(context == null) return;
            var existingTypeOfContraceptionSuggestions = context.TypeOfContraceptionSuggestions.ToList();
            var filterOutExistingTypeOfContraceptionSuggestions = GetTypeOfContraceptionSuggestions()
                .Where(x=> existingTypeOfContraceptionSuggestions.All(y => y.SnomedId != x.SnomedId))
                .ToList();
            context.TypeOfContraceptionSuggestions.AddRange(filterOutExistingTypeOfContraceptionSuggestions);
            context.SaveChanges();
        }

        private static List<TypeOfContraceptionSuggestion> GetTypeOfContraceptionSuggestions()
        {
            return new List<TypeOfContraceptionSuggestion>()
            {
                new()
                {
                    SnomedId = 706506000,
                    Name = "Condom",
                },
                new()
                {
                    SnomedId = 706507009,
                    Name = "Contraceptive coil",
                },
                new()
                {
                    SnomedId = 368441006,
                    Name = "Cervical contraceptive cap",
                },
                new()
                {
                    SnomedId = 20359006,
                    Name = "Diaphragm Contraception",
                },
                new()
                {
                    SnomedId = 442288006,
                    Name = "Female condom",
                },
                new()
                {
                    SnomedId = 268460000,
                    Name = "Intrauterine contraceptive device",
                },
                new()
                {
                    SnomedId = 336688002,
                    Name = "Vaginal contraceptive cap",
                },
                new()
                {
                    SnomedId = 108899006,
                    Name = "Contraceptive pill",
                },
                new()
                {
                    SnomedId = 326430003,
                    Name = "Emergency contraceptive - Levonogestrel 750Ug oral"
                    
                }


            };
        }
    }
}