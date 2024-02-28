using Abp.Domain.Services;

namespace Plateaumed.EHR
{
    public abstract class EHRDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected EHRDomainServiceBase()
        {
            LocalizationSourceName = EHRConsts.LocalizationSourceName;
        }
    }
}
