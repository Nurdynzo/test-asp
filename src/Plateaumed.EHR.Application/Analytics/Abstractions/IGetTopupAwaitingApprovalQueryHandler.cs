using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Abstractions
{
    public interface IGetTopupAwaitingApprovalQueryHandler : ITransientDependency
    {
        Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode);
    }
}
