using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Roles.Dto;
using Plateaumed.EHR.Test.Base;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Authorization.Roles
{
    // ReSharper disable once InconsistentNaming
    public class RoleAppService_Tests : AppTestBase
    {
        private readonly IRoleAppService _roleAppService;

        public RoleAppService_Tests()
        {
            _roleAppService = Resolve<IRoleAppService>();
        }

        [MultiTenantFact]
        public async Task Should_Get_Roles_For_Host()
        {
            LoginAsHostAdmin();

            //Act
            var output = await _roleAppService.GetRoles(new GetRolesInput());

            //Assert
            output.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Get_Roles_For_Tenant()
        {
            //Act
            var output = await _roleAppService.GetRoles(new GetRolesInput());

            //Assert
            output.Items.Count.ShouldBeGreaterThanOrEqualTo(1);
        }
    }
}
