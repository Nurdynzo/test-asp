using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.UI;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Dto;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Authorization.Users
{
    // ReSharper disable once InconsistentNaming
    public class UserAppService_Update_Tests : UserAppServiceTestBase
    {
        [Fact(Skip = "TODO: To be fixed")]
        public async Task Update_User_Basic_Tests()
        {
            //Arrange
            var managerRole = CreateRole("Manager");
            var adminUser = await GetUserByUserNameOrNullAsync(AbpUserBase.AdminUserName);
            var user = GetUserInstance(adminUser, new[] { "Manager" });

            //Act
            await UserAppService.CreateOrUpdateUser(user);

            //Assert
            await UsingDbContextAsync(async context =>
            {
                //Get created user
                var updatedAdminUser = await GetUserByUserNameOrNullAsync(adminUser.UserName, includeRoles: true);
                updatedAdminUser.ShouldNotBe(null);
                updatedAdminUser.Id.ShouldBe(adminUser.Id);

                //Check some properties
                updatedAdminUser.EmailAddress.ShouldBe(user.User.EmailAddress);
                updatedAdminUser.TenantId.ShouldBe(AbpSession.TenantId);

                LocalIocManager
                    .Resolve<IPasswordHasher<User>>()
                    .VerifyHashedPassword(updatedAdminUser, updatedAdminUser.Password, "123qwE*")
                    .ShouldBe(PasswordVerificationResult.Success);

                //Check roles
                updatedAdminUser.Roles.Count.ShouldBe(2); // Admin role is always assigned to admin user
                updatedAdminUser.Roles.Any(ur => ur.RoleId == managerRole.Id).ShouldBe(true);
            });
        }

        [Fact]
        public async Task Should_Not_Update_User_With_Duplicate_Username()
        {
            //Arrange

            CreateTestUsers();
            var jnashUser = await GetUserByUserNameOrNullAsync("jnash");
            var user = GetUserInstance(jnashUser, Array.Empty<string>());
            user.User.Id = null;

            //Act and Assert

            //Try to update with existing username
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
                await UserAppService.CreateOrUpdateUser(user));

            exception.Message.ShouldContain(user.User.UserName);

           
        }

        [Fact]
        public async Task Should_Not_Update_User_With_Duplicate_EmailAddress()
        {
            //Arrange
            CreateTestUsers();
            var jnashUser = await GetUserByUserNameOrNullAsync("jnash");
            var user = GetUserInstance(jnashUser, Array.Empty<string>());
            user.User.Id = null;
            
            //Act and Assert
            //Try to update with existing email address
            user.User.Id = null;
            user.User.UserName = "XXXXXXXX";
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
                await UserAppService.CreateOrUpdateUser(user));

            exception.Message.ShouldContain(user.User.EmailAddress);
        }

        [MultiTenantFact]
        public async Task Should_Remove_From_Role()
        {
            LoginAsHostAdmin();

            //Arrange
            var adminUser = await GetUserByUserNameOrNullAsync(AbpUserBase.AdminUserName);
            await UsingDbContextAsync(async context =>
            {
                var roleCount = await context.UserRoles.CountAsync(ur => ur.UserId == adminUser.Id);
                roleCount.ShouldBeGreaterThan(0); //There should be 1 role at least
            });

            //Act
            await UserAppService.CreateOrUpdateUser(GetUserInstance(adminUser,new[]{ StaticRoleNames.Host.Admin } ));

            //Assert
            await UsingDbContextAsync(async context =>
            {
                var roleCount = await context.UserRoles.CountAsync(ur => ur.UserId == adminUser.Id);
                roleCount.ShouldBe(1);
            });
        }

        [MultiTenantFact]
        public async Task Should_Not_Remove_From_Admin_Role()
        {
            LoginAsHostAdmin();

            // Arrange
            var adminUser = await GetUserByUserNameOrNullAsync(AbpUserBase.AdminUserName);
            CreateRole("super_admin");
            
            await UsingDbContextAsync(async context =>
            {
                var roleCount = await context.UserRoles.CountAsync(ur => ur.UserId == adminUser.Id);
                roleCount.ShouldBe(1);
            });
            
            //Act
            await UserAppService.CreateOrUpdateUser(
                GetUserInstance(adminUser,new[]{ "super_admin" }));
            
            // Assert
            var hasSuperAdminRole = await UserManager.IsInRoleAsync(adminUser, "super_admin");
            var hasAdminRole = await UserManager.IsInRoleAsync(adminUser, StaticRoleNames.Host.Admin);
            
            hasSuperAdminRole.ShouldBe(true);
            hasAdminRole.ShouldBe(true);
        }

        private static CreateOrUpdateUserInput GetUserInstance(User adminUser, string[] roleNames = null)
        {
            return new CreateOrUpdateUserInput
            {
                User = new UserEditDto //Not changing user properties
                {
                    Id = adminUser.Id,
                    EmailAddress = adminUser.EmailAddress,
                    Name = adminUser.Name,
                    Surname = adminUser.Surname,
                    UserName = adminUser.UserName,
                    Password = null,
                    Title = TitleType.Mr,
                    Gender = GenderType.Male,
                    DateOfBirth = new DateTime(1980, 1, 1),
                        
                        
                },
                AssignedRoleNames = roleNames // remove admin role and assign super_admin role
            };
        }

        protected Role CreateRole(string roleName)
        {
            return UsingDbContext(context => context.Roles.Add(new Role(AbpSession.TenantId, roleName, roleName)).Entity);
        }
    }
}
