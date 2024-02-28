using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public static class AlcoholTypesSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            var existingSuggestions = context.AlcoholTypes.ToList();
            var filteredSuggestions = GetAlcoholTypes()
                .Where(x => existingSuggestions.All(y => y.Type != x.Type && y.AlcoholUnit != x.AlcoholUnit)).ToList();
            context.AlcoholTypes.AddRange(filteredSuggestions);
            context.SaveChanges();
        }


        private static List<AlcoholType> GetAlcoholTypes()
        {
            return new List<AlcoholType>
            {
                new()
                {
                    Type = "Single small shot of spirits * (25ml, ABV 40%)",
                    AlcoholUnit = 1
                },
                new()
                {
                    Type = "Alcopop (275ml, ABV 5.5%)",
                    AlcoholUnit = 1.5f
                },
                new()
                {
                    Type = "Small glass of red/white/rose wine (125ml, ABV 12%)",
                    AlcoholUnit = 1.5f
                },
                new()
                {
                    Type = "Bottle of lager/beer/cider (330ml, ABV 5%)",
                    AlcoholUnit = 1.7f
                },
                new()
                {
                    Type = "Can of  lager/beer/cider (440ml, ABV 5.5%)",
                    AlcoholUnit = 2
                },
                new()
                {
                    Type = "Pint of lower-strength  lager/beer/cider (ABV 3.5%)",
                    AlcoholUnit = 2
                },
                new()
                {
                    Type = "Standard glass of red/white/rose wine (175ml, ABV 12%)",
                    AlcoholUnit = 2.1f
                },
                new()
                {
                    Type = "Pint of higher-strength  lager/beer/cider (ABV 5.2%)",
                    AlcoholUnit = 3
                },
                new()
                {
                    Type = "Larger glass of red/white/rose wine (250ml, ABV 12%)",
                    AlcoholUnit = 3
                }
            };
        }
    }
}
