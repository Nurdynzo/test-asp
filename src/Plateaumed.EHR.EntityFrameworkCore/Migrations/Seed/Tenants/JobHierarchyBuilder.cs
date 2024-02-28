using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Staff;
using System.Linq;

namespace Plateaumed.EHR.Migrations.Seed.Tenants
{
    public class JobHierarchyBuilder
    {
        private readonly EHRDbContext _context;

        public JobHierarchyBuilder(EHRDbContext context)
        {
            _context = context;
        }

        public void Create(int tenantId, long facilityId)
        {
            var jobTitles = StaticJobHierarchy.GetDefaultJobTitlesHierarchyForTenant(tenantId, facilityId);

            jobTitles.ForEach(jobTitle =>
            {
                var existingJobTitle = _context.JobTitles.IgnoreQueryFilters()
                    .FirstOrDefault(s => s.Name == jobTitle.Name && s.TenantId == tenantId);
                if (existingJobTitle == null)
                {
                    _context.JobTitles.Add(jobTitle);
                    _context.SaveChanges();
                }
            });
        }
    }
}