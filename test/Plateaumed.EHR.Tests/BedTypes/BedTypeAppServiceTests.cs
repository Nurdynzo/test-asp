using System.Threading.Tasks;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.BedTypes
{
    [Trait("Category", "Integration")]
    public class BedTypeAppServiceTests : AppTestBase
    {
        private readonly IBedTypesAppService _bedTypeAppService;

        public BedTypeAppServiceTests()
        {
            _bedTypeAppService = Resolve<IBedTypesAppService>();
        }

        [Fact]
        public async Task GetAll_GivenFacilityId_ShouldReturnFilteredBedTypes()
        {
            // Arrange
            await LoginAsCustomTenant();
            var tenantId = AbpSession.TenantId.Value;

            var facility1 = CreateFacility(tenantId);
            var facility2 = CreateFacility(tenantId);

            var expectedBedType = UsingDbContext(context =>
            {
                TestBedTypeBuilder.Create(tenantId).WithName("Bed").WithFacility(facility2.Id).Save(context);
                return TestBedTypeBuilder.Create(tenantId).WithName("Cot").WithFacility(facility1.Id).Save(context);
            });

            // Act
            var output = await _bedTypeAppService.GetAll(new GetAllBedTypesInput { FacilityId = facility1.Id });

            // Assert
            output.Items.Count.ShouldBe(1);
            output.Items[0].Id.ShouldBe(expectedBedType.Id);
            output.Items[0].Name.ShouldBe(expectedBedType.Name);
        }

        private async Task LoginAsCustomTenant()
        {
            var (tenant, user) = UsingDbContext(context =>
            {
                var tenant = TestTenantBuilder.Create().Save(context);
                var user = TestUserBuilder.Create(tenant.Id).Save(context);
                return (tenant, user);
            });

            LoginAsTenant(tenant.TenancyName, user.UserName);
            await SetUserPermissions(user, AppPermissions.Pages_BedTypes);
        }

        private Facility CreateFacility(int tenantId)
        {
            return UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(tenantId).Save(context);
                var facility = TestFacilityBuilder.Create(tenantId, group.Id).Save(context);

                context.SaveChanges();
                return facility;
            });
        }
    }
}