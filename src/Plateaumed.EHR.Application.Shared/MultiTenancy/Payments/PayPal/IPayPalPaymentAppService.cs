using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.MultiTenancy.Payments.PayPal.Dto;

namespace Plateaumed.EHR.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
