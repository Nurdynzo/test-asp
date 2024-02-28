using Microsoft.EntityFrameworkCore;
using PayPalCheckoutSdk.Orders;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Migrations.Seed.Tenants
{
    public class FacilityTypeBuilder
    {
        private EHRDbContext _context;

        public FacilityTypeBuilder(EHRDbContext context)
        {
            _context = context;
        }

        public FacilityType Create()
        {

            return CreateFacilityTypes();

        }
        private FacilityType CreateFacilityTypes()
        {
            var facilityTypes = new List<FacilityType>()
            {
                new FacilityType()
                {
                     Id = 1,
                     IsActive = true,
                     Name = "Hospital",
                },
                new FacilityType()
                {
                     Id = 2,
                     IsActive = true,
                     Name = "Pharmacy",
                },
                new FacilityType()
                {
                     Id = 3,
                     IsActive = true,
                     Name = "Laboratory",
                },

            };
            foreach (var facilityType in facilityTypes)
            {
                var existingType = _context.FacilityTypes.IgnoreQueryFilters().FirstOrDefault(p => p.Id == facilityType.Id);

                if (existingType == null)
                {
                    _context.FacilityTypes.Add(facilityType);
                    _context.SaveChanges();
                }
            }

            return _context.FacilityTypes.FirstOrDefault();
        }
    }
}
