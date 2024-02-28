using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Misc.Payload;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class DefaultDistrictCreator
    {
        public List<District> InitialDistricts => GetInitialDistricts();

        private readonly EHRDbContext _context;

        private List<District> GetInitialDistricts()
        {
            var districtData = DistrictsHelper.GetDeserializedDistrictData();

            List<District> allDistricts = new();

            foreach (var districtPayload in districtData)
            {
                if (districtPayload != null && districtPayload.CountryName !=null )
                {
                    foreach (var region in _context.Regions.Include(x => x.CountryFk).ToList())
                    {
                        if (region != null && districtPayload.Districts != null)
                        {
                            foreach (var district in districtPayload.Districts)
                            {
                                if (district != null && districtPayload.RegionName != null && districtPayload.RegionName.Contains(region.Name.Replace(" ", ""), System.StringComparison.OrdinalIgnoreCase))
                                {
                                    var newDistrict = new District
                                    {
                                        Name = district.Name,
                                        RegionId = region.Id,
                                    };
                                    allDistricts.Add(newDistrict);
                                }
                            }
                        }
                    }
                }
            }
            return allDistricts;
        }

        public DefaultDistrictCreator(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDistricts();
        }

        private void CreateDistricts()
        {
            if (!_context.Districts.ToList().Any())
            {
                _context.Districts.AddRange(InitialDistricts);
                _context.SaveChanges();
            }
        }
    }
}
