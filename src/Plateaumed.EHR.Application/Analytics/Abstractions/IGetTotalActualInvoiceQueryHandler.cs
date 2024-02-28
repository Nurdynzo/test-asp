using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Abstractions
{
    public interface IGetTotalActualInvoiceQueryHandler : ITransientDependency
    {
        Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode);
    }
}
