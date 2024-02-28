using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Abstractions
{
    public interface IAnalyticsForCancelledVsUncancelledInvoiceQueryHandler : ITransientDependency
    {
        Task<GetCountAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode);
    }
}
