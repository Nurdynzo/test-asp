using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Migrations.Seed.Tenants;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class InitialHostDbSeeder
    {
        public static void Seed(EHRDbContext context)
        {
            new DefaultSubscribableEditionCreator(context).Create();
            new DefaultEditionCreator(context).Create();
            new DefaultLanguagesCreator(context).Create();
            new HostRoleAndUserCreator(context).Create();
            new DefaultSettingsCreator(context).Create();
            new DefaultCountriesCreator(context).Create();
            new DefaultRegionsCreator(context).Create();
            new DefaultDistrictCreator(context).Create();
            new DefaultOccupationCategoriesCreator(context).Create();
            new DefaultOccupationsCreator(context).Create();
            new FacilityTypeBuilder(context).Create();
            new InsuranceProviderBuilder(context).Create();

            context.SaveChanges();
        }
    }
}
