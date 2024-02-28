using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}