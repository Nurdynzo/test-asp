using System.Linq;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Notifications;

namespace Plateaumed.EHR.Migrations.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly EHRDbContext _context;

        public HostRoleAndUserCreator(EHRDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles
                .IgnoreQueryFilters()
                .FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);

            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles
                    .Add(
                        new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin)
                        {
                            IsStatic = true,
                            IsDefault = true
                        }
                    )
                    .Entity;
                _context.SaveChanges();
            }

            //admin user for host

            var adminUserForHost = _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);

            if (adminUserForHost == null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AbpUserBase.AdminUserName,
                    Name = "Host",
                    Surname = "Admin",
                    EmailAddress = "admin@plateaumed.com",
                    IsEmailConfirmed = true,
                    ShouldChangePasswordOnNextLogin = false,
                    IsActive = true,
                    Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions()))
                        .HashPassword(adminUserForHost, "Super.Admin!")
                };

                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(
                    new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id)
                );
                _context.SaveChanges();

                //User account of admin user
                _context.UserAccounts.Add(
                    new UserAccount
                    {
                        TenantId = null,
                        UserId = adminUserForHost.Id,
                        UserName = AbpUserBase.AdminUserName,
                        EmailAddress = adminUserForHost.EmailAddress
                    }
                );

                _context.SaveChanges();

                //Notification subscriptions
                _context.NotificationSubscriptions.Add(
                    new NotificationSubscriptionInfo(
                        SequentialGuidGenerator.Instance.Create(),
                        null,
                        adminUserForHost.Id,
                        AppNotificationNames.NewTenantRegistered
                    )
                );

                _context.NotificationSubscriptions.Add(
                    new NotificationSubscriptionInfo(
                        SequentialGuidGenerator.Instance.Create(),
                        null,
                        adminUserForHost.Id,
                        AppNotificationNames.NewUserRegistered
                    )
                );

                _context.SaveChanges();
            }
        }
    }
}
