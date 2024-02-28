using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Plateaumed.EHR.Authentication.TwoFactor.Google;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>(options =>
                {
                    options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
                })
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
