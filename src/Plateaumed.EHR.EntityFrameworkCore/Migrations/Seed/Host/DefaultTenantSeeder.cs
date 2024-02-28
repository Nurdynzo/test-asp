using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Migrations.Seed.Tenants;

namespace Plateaumed.EHR.Migrations.Seed.Host;

public class DefaultTenantSeeder
{
    public static void Seed(EHRDbContext context)
    {
        new DefaultTenantBuilder(context).Create();
        new TenantRoleAndUserBuilder(context, 1).Create();

        context.SaveChanges();
    }
}