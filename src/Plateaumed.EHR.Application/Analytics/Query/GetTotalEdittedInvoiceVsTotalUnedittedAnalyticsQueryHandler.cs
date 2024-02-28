using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Analytics.Abstractions;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Query
{
    public class GetTotalEdittedInvoiceVsTotalUnedittedAnalyticsQueryHandler : IGetTotalEdittedInvoiceVsTotalUnedittedAnalyticsQueryHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;

        public GetTotalEdittedInvoiceVsTotalUnedittedAnalyticsQueryHandler(IRepository<Invoice, long> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetCountAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            List<Invoice> invoiceBeforeSelectedTime = new();
            var timeSelection = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeSelection.BeforeStartDate;
            DateTime startDate = timeSelection.StartDate;
            DateTime endDate = timeSelection.EndDate;



            var editedInvoiceQuery = from i in _invoiceRepository.GetAll()
                                     where (i.IsEdited &&
                                     (i.CreationTime >= startDate && i.CreationTime <= endDate)
                                     && i.FacilityId == facilityId)
                                     select new
                                     {
                                         i.Id
                                     };
            var editedInvoiceCount = await editedInvoiceQuery.CountAsync();


            var uneditedInvoiceQuery = from i in _invoiceRepository.GetAll()
                                       where (!i.IsEdited &&
                                            (i.CreationTime >= startDate && i.CreationTime <= endDate)
                                            && i.FacilityId == facilityId)
                                       select new
                                       {
                                           i.Id
                                       };

            var uneditedInvoiceCount = await uneditedInvoiceQuery.CountAsync();

            decimal percentIncreaseOrDecrease = 0.0m;
            if (editedInvoiceCount > 0 && uneditedInvoiceCount == 0)
            {
                percentIncreaseOrDecrease = 100m;
            }
            else if (editedInvoiceCount == 0 && uneditedInvoiceCount > 0)
            {
                percentIncreaseOrDecrease = -100m;
            }
            if (uneditedInvoiceCount > 0 && editedInvoiceCount > 0)
            {
                percentIncreaseOrDecrease = ((editedInvoiceCount - uneditedInvoiceCount) / uneditedInvoiceCount) * 100;
            }

            return new GetCountAnalyticsResponseDto
            {
                TotalCount = editedInvoiceCount,
                PercentageIncreaseOrDecrease = percentIncreaseOrDecrease,
                Increased = percentIncreaseOrDecrease > 0

            };

        }
    }
}