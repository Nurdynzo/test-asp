using Abp.Domain.Repositories;
using Plateaumed.EHR.Analytics.Abstractions;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using Plateaumed.EHR.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Query
{
    public class GetTotalActualInvoiceQueryHandler : IGetTotalActualInvoiceQueryHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;

        public GetTotalActualInvoiceQueryHandler(IRepository<Invoice, long> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }


        public Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            List<Invoice> invoiceBeforeSelectedTime = new();
            var timeSelection = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeSelection.BeforeStartDate;
            DateTime startDate = timeSelection.StartDate;
            DateTime endDate = timeSelection.EndDate;
            
            var selectedTimeInvoicesQuery = from i in _invoiceRepository.GetAll()
                                       where (i.InvoiceType == InvoiceType.InvoiceCreation && 
                                       (i.CreationTime >= startDate && i.CreationTime <= endDate)
                                       && i.FacilityId == facilityId)
                                       select new
                                       {
                                           i.TotalAmount
                                       };
            var selectedTimeInvoicesAmountSum = selectedTimeInvoicesQuery.Sum(x => x.TotalAmount.Amount);
            
            
            var invoiceBeforeQuery = from i in _invoiceRepository.GetAll()
                                     where (i.InvoiceType == InvoiceType.InvoiceCreation && 
                                     (i.CreationTime >= beforeStartDate && i.CreationTime <= startDate)
                                     && i.FacilityId == facilityId)
                                     select new
                                     {
                                         i.TotalAmount
                                     };

            var sumOfInvoiceBefore = invoiceBeforeQuery.Sum(x => x.TotalAmount.Amount);
            decimal percentIncreaseOrDecrease = 0.0m;
            if (selectedTimeInvoicesAmountSum > 0 && sumOfInvoiceBefore == 0)
            {
                percentIncreaseOrDecrease = 100m;
            }
            else if (selectedTimeInvoicesAmountSum == 0 && sumOfInvoiceBefore > 0)
            {
                percentIncreaseOrDecrease = -100m;
            }
            if (sumOfInvoiceBefore > 0 && selectedTimeInvoicesAmountSum > 0)
            {
                percentIncreaseOrDecrease = ((selectedTimeInvoicesAmountSum - sumOfInvoiceBefore) / sumOfInvoiceBefore) * 100;
            }

            return Task.FromResult(new GetAnalyticsResponseDto
            {
                Total = new MoneyDto { Amount = selectedTimeInvoicesAmountSum },
                Difference = new MoneyDto { Amount = selectedTimeInvoicesAmountSum - sumOfInvoiceBefore },
                PercetageIncreaseOrDecrease = percentIncreaseOrDecrease,
                Increased = percentIncreaseOrDecrease > 0
            });
        }
    }
}
