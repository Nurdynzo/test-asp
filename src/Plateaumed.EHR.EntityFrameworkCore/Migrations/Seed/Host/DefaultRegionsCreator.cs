using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Misc.Payload;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class DefaultRegionsCreator
    {
        public List<Region> InitialRegions => GetInitialRegions();

        private readonly EHRDbContext _context;

        private List<Region> GetInitialRegions()
        {
            // Fix: Remove CountryData.Standard as a dependency
            var regionData = RegionHelper.GetDeserializedRegionData();
            List<Region> theRegions = new();
            foreach (var country in _context.Countries.ToList())
            {
               foreach (var helperCountry in regionData)
               {
                   if (country.Name == helperCountry.CountryName)
                   {
                       foreach (var helperRegion in helperCountry.Regions)
                       {
                           var newRegion = new Region
                           {
                               CountryId = country.Id,
                               ShortName = helperRegion.ShortCode,
                               Name = helperRegion.Name
                           };
                           theRegions.Add(newRegion);
                       }
                   }
               }
            }
            return theRegions;
        }
        public DefaultRegionsCreator(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateRegions();
        }

        private void CreateRegions()
        {
            if (!_context.Regions.ToList().Any())
            {
                _context.Regions.AddRange(InitialRegions);
                _context.SaveChanges();
            }
        }
    }
}