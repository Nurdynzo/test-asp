using System.Threading.Tasks;
using Abp.Webhooks;

namespace Plateaumed.EHR.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
