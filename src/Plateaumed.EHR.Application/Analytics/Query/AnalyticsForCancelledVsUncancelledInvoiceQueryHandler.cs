using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Analytics.Abstractions;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Query
{
    public class AnalyticsForCancelledVsUncancelledInvoiceQueryHandler : IAnalyticsForCancelledVsUncancelledInvoiceQueryHandler
    {
        private readonly IRepository<InvoiceCancelRequest, long> _invoiceCancelRequestRepository;

        public AnalyticsForCancelledVsUncancelledInvoiceQueryHandler(IRepository<InvoiceCancelRequest, long> invoiceCancelRequestRepository)
        {
            _invoiceCancelRequestRepository = invoiceCancelRequestRepository;
        }

        public async Task<GetCountAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            var timeRange = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeRange.BeforeStartDate;
            DateTime startDate = timeRange.StartDate;
            DateTime endDate = timeRange.EndDate;

            var queryForCancelled = from i in _invoiceCancelRequestRepository.GetAll()
                        where (i.Status == InvoiceCancelStatus.Approved
                        && (i.CreationTime >= startDate && i.CreationTime <= endDate)
                        && i.FacilityId == facilityId)
                        select new { i.Id };
            var totalCountForCancelledInvoice = await queryForCancelled.CountAsync();

            var queryForUncancelled = from i in _invoiceCancelRequestRepository.GetAll()
                                      where (i.Status != InvoiceCancelStatus.Approved
                                      && (i.CreationTime >= startDate && i.CreationTime <= endDate)
                                      && i.FacilityId == facilityId)
                                      select new { i.Id };
            var totalCountForUncanclledInvoice = await queryForUncancelled.CountAsync();
            decimal percentageDifference = 0.00m;

            if (totalCountForCancelledInvoice > 0 && totalCountForUncanclledInvoice == 0)
            {
                percentageDifference = 100m;
            }
            else if (totalCountForCancelledInvoice == 0 && totalCountForUncanclledInvoice > 0)
            {
                percentageDifference = -100m;
            }
            if (totalCountForUncanclledInvoice > 0 && totalCountForCancelledInvoice > 0)
            {
                percentageDifference = ((totalCountForCancelledInvoice - totalCountForUncanclledInvoice) / totalCountForUncanclledInvoice) * 100;
            }

            return new GetCountAnalyticsResponseDto
            {
                TotalCount = totalCountForCancelledInvoice,
                PercentageIncreaseOrDecrease = percentageDifference,
                Increased = percentageDifference > 0
            };

        }
    }
}
