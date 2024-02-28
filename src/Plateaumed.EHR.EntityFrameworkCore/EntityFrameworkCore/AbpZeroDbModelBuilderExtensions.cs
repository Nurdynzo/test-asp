using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Organizations;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.IdentityServer4vNext;
using Abp.Webhooks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Editions;

namespace Prism.EntityFrameworkCore
{
    /// <summary>
    /// Extension methods for <see cref="ModelBuilder"/>.
    /// </summary>
    public static class AbpZeroDbModelBuilderExtensions
    {
        /// <summary>
        /// Changes prefix for ABP tables (which is "Abp" by default).
        /// Can be null/empty string to clear the prefix.
        /// </summary>
        /// <typeparam name="TTenant">The type of the tenant entity.</typeparam>
        /// <typeparam name="TRole">The type of the role entity.</typeparam>
        /// <typeparam name="TUser">The type of the user entity.</typeparam>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="prefix">Table prefix, or null to clear prefix.</param>
        public static void ChangeAbpTablePrefix<TTenant, TRole, TUser>(this ModelBuilder modelBuilder, string prefix)
            where TTenant : AbpTenant<TUser>
            where TRole : AbpRole<TUser>
            where TUser : AbpUser<TUser>
        {
            prefix = prefix ?? "";

            modelBuilder.Entity<AuditLog>().ToTable(prefix + "AuditLogs");
            modelBuilder.Entity<BackgroundJobInfo>().ToTable(prefix + "BackgroundJobs");
            modelBuilder.Entity<DynamicEntityProperty>().ToTable(prefix + "DynamicEntityProperties");
            modelBuilder.Entity<DynamicEntityPropertyValue>().ToTable(prefix + "DynamicEntityPropertyValues");
            modelBuilder.Entity<DynamicProperty>().ToTable(prefix + "DynamicProperties");
            modelBuilder.Entity<DynamicPropertyValue>().ToTable(prefix + "DynamicPropertyValues");
            modelBuilder.Entity<Edition>().ToTable(prefix + "Editions");
            modelBuilder.Entity<SubscribableEdition>().ToTable(prefix + "Editions");
            modelBuilder.Entity<EntityChangeSet>().ToTable(prefix + "EntityChangeSets");
            modelBuilder.Entity<EntityChange>().ToTable(prefix + "EntityChanges");
            modelBuilder.Entity<EntityPropertyChange>().ToTable(prefix + "EntityPropertyChanges");
            modelBuilder.Entity<TenantFeatureSetting>().ToTable(prefix + "Features");
            modelBuilder.Entity<EditionFeatureSetting>().ToTable(prefix + "Features");
            modelBuilder.Entity<FeatureSetting>().ToTable(prefix + "Features");
            modelBuilder.Entity<ApplicationLanguageText>().ToTable(prefix + "LanguageTexts");
            modelBuilder.Entity<ApplicationLanguage>().ToTable(prefix + "Languages");
            modelBuilder.Entity<NotificationSubscriptionInfo>().ToTable(prefix + "NotificationSubscriptions");
            modelBuilder.Entity<NotificationInfo>().ToTable(prefix + "Notifications");
            modelBuilder.Entity<OrganizationUnitRole>().ToTable(prefix + "OrganizationUnitRoles");
            modelBuilder.Entity<OrganizationUnit>().ToTable(prefix + "OrganizationUnits");
            modelBuilder.Entity<RolePermissionSetting>().ToTable(prefix + "Permissions");
            modelBuilder.Entity<UserPermissionSetting>().ToTable(prefix + "Permissions");
            modelBuilder.Entity<PermissionSetting>().ToTable(prefix + "Permissions");
            modelBuilder.Entity<PersistedGrantEntity>().ToTable(prefix + "PersistentGrants");
            modelBuilder.Entity<RoleClaim>().ToTable(prefix + "RoleClaims");
            modelBuilder.Entity<TRole>().ToTable(prefix + "Roles");
            modelBuilder.Entity<Setting>().ToTable(prefix + "Settings");
            modelBuilder.Entity<TenantNotificationInfo>().ToTable(prefix + "TenantNotifications");
            modelBuilder.Entity<TTenant>().ToTable(prefix + "Tenant");
            modelBuilder.Entity<UserAccount>().ToTable(prefix + "UserAccounts");
            modelBuilder.Entity<UserClaim>().ToTable(prefix + "UserClaims");
            modelBuilder.Entity<UserLoginAttempt>().ToTable(prefix + "UserLoginAttempts");
            modelBuilder.Entity<UserLogin>().ToTable(prefix + "UserLogins");
            modelBuilder.Entity<UserNotificationInfo>().ToTable(prefix + "UserNotifications");
            modelBuilder.Entity<UserOrganizationUnit>().ToTable(prefix + "UserOrganizationUnits");
            modelBuilder.Entity<UserRole>().ToTable(prefix + "UserRoles");
            modelBuilder.Entity<UserToken>().ToTable(prefix + "UserTokens");
            modelBuilder.Entity<TUser>().ToTable(prefix + "Users");
            modelBuilder.Entity<WebhookEvent>().ToTable(prefix + "WebhookEvents");
            modelBuilder.Entity<WebhookSendAttempt>().ToTable(prefix + "WebhookSendAttempts");
            modelBuilder.Entity<WebhookSubscriptionInfo>().ToTable(prefix + "WebhookSubscriptions");
        }
    }
}
