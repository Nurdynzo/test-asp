using Abp.Application.Services;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Abstractions
{
    public interface IAnalyticsAppService : IApplicationService
    {
        Task<GetAnalyticsResponseDto> GetTotalActualInvoice(AnalyticsEnum selectionMode);
        Task<GetAnalyticsResponseDto> GetTotalWalletTopup(AnalyticsEnum selectionMode);
        Task<GetAnalyticsResponseDto> GetTotalOutstanding(AnalyticsEnum selectionMode);
        Task<GetAnalyticsResponseDto> GetTotalPayments(AnalyticsEnum selectionMode);
        Task<GetAnalyticsResponseDto> GetTotalTopupAwaitingApproval(AnalyticsEnum selectionMode);
        Task<GetCountAnalyticsResponseDto> GetAnalyticsForCancelledInvoice(AnalyticsEnum selectionMode);
        Task<GetCountAnalyticsResponseDto> GetEditedInvoiceAnalytics(AnalyticsEnum selectionMode);
    }
}
