using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Migrations.Seed.Host;

namespace Plateaumed.EHR.Migrations.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<EHRDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(EHRDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            //Host seed
            InitialHostDbSeeder.Seed(context);
            MedicalDataSeeder.Seed(context);
            //Default tenant seed (in host database).
            DefaultTenantSeeder.Seed(context);
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
