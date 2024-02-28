using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Misc.Payload;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class DefaultCountriesCreator
    {
        public static List<Country> InitialCountries => GetInitialCountries();
        private readonly EHRDbContext _context;
        private static List<Country> GetInitialCountries()
        {
            var JsonData = CountryHelper.GetDeserializedCountriesData();
            List<Country> Country = new ();
            foreach (var country in JsonData)
            {
                var newCountry = new Country(country.Name, country.Nationality, country.Code, country.PhoneCode, country.Currency, country.CurrencyCode, country.CurrencySymbol);
                Country.Add(newCountry);
            }
            return Country;
        }
        public DefaultCountriesCreator(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateCountries();
        }
        private void CreateCountries()
        {
            if(!_context.Countries.ToList().Any()){
                _context.Countries.AddRange(InitialCountries);
                _context.SaveChanges();
            }
        }

    }
}
