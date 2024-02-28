using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Migrations.Seed.Tenants
{
    public class InsuranceProviderBuilder
    {
        private EHRDbContext _context;

        public InsuranceProviderBuilder(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateInsuranceProviders();
        }
        private void CreateInsuranceProviders()
        {
            var insuanceProviderTypes = new List<InsuranceProvider>()
            {
                new InsuranceProvider ()
                {
                    Id = 1,
                    Name = "NHIS",
                    Type = InsuranceProviderType.National,                    
                },
                new InsuranceProvider ()
                {
                    Id = 2,
                    Name = "ABSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 3,
                    Name = "CRSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 4,
                    Name = "DSCHC",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 5,
                    Name = "BSCHMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 6,
                    Name = "ADSPHCDA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 7,
                    Name = "BNSHIS",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 8,
                    Name = "BHIS",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 9,
                    Name = "ASHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 10,
                    Name = "BOSCHMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 11,
                    Name = "EBSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 12,
                    Name = "EDOHIS",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 13,
                    Name = "EKHIS",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 14,
                    Name = "GOHEALTH",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 15,
                    Name = "IMSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 16,
                    Name = "JICHMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 17,
                    Name = "KADCHMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 18,
                    Name = "KSCHCMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 19,
                    Name = "KTSCHMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 20,
                    Name = "KECHES",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 21,
                    Name = "KGSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 22,
                    Name = "KSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 23,
                    Name = "LSHIS",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 24,
                    Name = "NASHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 25,
                    Name = "NGHSCA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 26,
                    Name = "OGSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 27,
                    Name = "ODCHC",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 28,
                    Name = "OSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 29,
                    Name = "OYSHIA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 30,
                    Name = "PLASCHEMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 31,
                    Name = "SOCHEMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 32,
                    Name = "YSCHMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 33,
                    Name = "ZAMCHEMA",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 34,
                    Name = "FHIS",
                    Type = InsuranceProviderType.State,
                },
                new InsuranceProvider ()
                {
                    Id = 35,
                    Name = "Avon",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 36,
                    Name = "Axa Mansard",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 37,
                    Name = "Clearline HMO",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 38,
                    Name = "HCI Healthcare",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 39,
                    Name = "HYGEIA HMO LIMITED",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 40,
                    Name = "Leadway",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 41,
                    Name = "Multishield",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 42,
                    Name = "Red Care",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 43,
                    Name = "Reliance HMO",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 44,
                    Name = "THT/Liberty",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 45,
                    Name = "iHMS",
                    Type = InsuranceProviderType.Private,
                },
                new InsuranceProvider ()
                {
                    Id = 46,
                    Name = "Mediplan Healthcare",
                    Type = InsuranceProviderType.Private,
                },
            };

            foreach (var insuranceProvider in insuanceProviderTypes)
            {
                var existingInsuranceProviderType = _context.InsuranceProviders.IgnoreQueryFilters().FirstOrDefault(p => p.Id == insuranceProvider.Id);

                if (existingInsuranceProviderType == null)
                {
                    _context.InsuranceProviders.Add(insuranceProvider);
                }
            }
            _context.SaveChanges();

        }
    }
}
