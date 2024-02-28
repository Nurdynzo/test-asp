using System.Linq;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.Migrations.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly EHRDbContext _context;

        public DefaultTenantBuilder(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName, TenantType.Business);
                defaultTenant.Category = TenantCategoryType.Private;
                defaultTenant.HasSignedAgreement = true;

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
        }
    }
}
