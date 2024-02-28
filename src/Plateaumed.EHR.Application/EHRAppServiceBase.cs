using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class EHRAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }
        
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        protected EHRAppServiceBase()
        {
            LocalizationSourceName = EHRConsts.LocalizationSourceName;
        }

        protected virtual long GetCurrentUserFacilityId()
        {
            return HttpContextAccessor.GetFacilityId() 
                   ?? throw new UserFriendlyException("Current user does not have a default facility");
        }
        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdExtendedAsync(AbpSession.ToUserIdentifier());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual User GetCurrentUser()
        {
            return AsyncHelper.RunSync(GetCurrentUserAsync);
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}