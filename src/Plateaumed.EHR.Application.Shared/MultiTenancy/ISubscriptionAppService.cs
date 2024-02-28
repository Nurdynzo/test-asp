using System.Threading.Tasks;
using Abp.Application.Services;

namespace Plateaumed.EHR.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
