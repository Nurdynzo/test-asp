using System.Linq;
using Abp;
using Abp.Authorization.Users;
using Abp.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Notifications;

namespace Plateaumed.EHR.Migrations.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly EHRDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(EHRDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            //Create Tenant Roles
            StaticRoleNames.Tenants.AllRoles.ForEach(role =>
            {
                var savedRole = _context.Roles
                    .IgnoreQueryFilters()
                    .FirstOrDefault(r => r.TenantId == _tenantId && r.Name == role);

                if (savedRole == null)
                {
                    _context.Roles
                        .Add(
                            new Role(_tenantId, role, role)
                            {
                                IsStatic = true,
                                IsDefault = role == StaticRoleNames.Tenants.User
                            }
                        );
                        
                    _context.SaveChanges();
                }
            });

            //Create Tenant Admin User
            CreateAdminUser();
        }

        private void CreateAdminUser()
        {
            //Admin User
            var adminUser = _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefault(
                    u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName
                );

            var adminRole = _context.Roles
                .IgnoreQueryFilters()
                .FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);

            if (adminUser == null && adminRole != null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, AbpUserBase.AdminUserName, "admin@defaulttenant.com", "Default", "Admin");

                adminUser.Password = new PasswordHasher<User>(
                    new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())
                ).HashPassword(adminUser, "Default.Admin!");

                adminUser.IsEmailConfirmed = true;
                adminUser.ShouldChangePasswordOnNextLogin = false;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(
                    new UserRole(
                        _tenantId,
                        adminUser.Id,
                        adminRole.Id
                    )
                );

                _context.SaveChanges();

                //User account of admin user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(
                        new UserAccount
                        {
                            TenantId = _tenantId,
                            UserId = adminUser.Id,
                            UserName = AbpUserBase.AdminUserName,
                            EmailAddress = adminUser.EmailAddress
                        }
                    );
                    _context.SaveChanges();
                }

                //Notification subscription
                _context.NotificationSubscriptions.Add(
                    new NotificationSubscriptionInfo(
                        SequentialGuidGenerator.Instance.Create(),
                        _tenantId,
                        adminUser.Id,
                        AppNotificationNames.NewUserRegistered
                    )
                );
                _context.SaveChanges();
            }
        }
    }
}
