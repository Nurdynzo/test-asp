using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.MultiTenancy.Payments.Dto;
using Plateaumed.EHR.MultiTenancy.Payments.Stripe.Dto;

namespace Plateaumed.EHR.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}