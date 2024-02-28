using System.Linq.Dynamic.Core;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Migrations.Seed.Host;
using Plateaumed.EHR.Migrations.Seed.Tenants;

namespace Plateaumed.EHR.Test.Base.TestData
{
    public class TestDataBuilder
    {
        private readonly EHRDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(EHRDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            // Copy seed data from app start for now. Will cleanup up 
            InitialHostDbSeeder.Seed(_context);
            DefaultTenantSeeder.Seed(_context);

            var facilityType = _context.FacilityTypes.FirstOrDefault();
            var facilityGroup = new FacilityGroupBuilder(_context, 1).Create();
            var facility = new FacilityBuilder(_context, 1).Create(facilityGroup.Id, facilityType.Id);
            new JobHierarchyBuilder(_context).Create(1, facility.Id);
            new StaffMemberBuilder(_context, 1).Create(facility.Id);
            // ----
            
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();
            new TestEditionsBuilder(_context).Create();

            _context.SaveChanges();
        }
    }
}
