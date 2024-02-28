using System.Threading.Tasks;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
