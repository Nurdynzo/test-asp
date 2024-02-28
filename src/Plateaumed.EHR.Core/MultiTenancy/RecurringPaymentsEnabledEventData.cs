using Abp.Events.Bus;

namespace Plateaumed.EHR.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}