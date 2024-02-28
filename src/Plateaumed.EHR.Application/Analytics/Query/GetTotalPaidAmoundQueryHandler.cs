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
    public class GetTotalPaidAmoundQueryHandler : IGetTotalPaidAmoundQueryHandler
    {
        private readonly IRepository<PaymentActivityLog, long> _paymentActivitylogRepo;

        public GetTotalPaidAmoundQueryHandler(IRepository<PaymentActivityLog, long> paymentActivitylogRepo)
        {
            _paymentActivitylogRepo = paymentActivitylogRepo;
        }

        public Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            List<PaymentActivityLog> paymentBeforeSelectedTime = new();
            var timeRange = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeRange.BeforeStartDate;
            DateTime startDate = timeRange.StartDate;
            DateTime endDate = timeRange.EndDate;

            var selectedTimePaymentsQuery = from p in _paymentActivitylogRepo.GetAll()
                                            where (p.TransactionAction == TransactionAction.PaidInvoice &&
                                            (p.CreationTime >= startDate && p.CreationTime <= endDate)
                                            && p.FacilityId == facilityId)
                                            select new
                                            {
                                                p.AmountPaid
                                            };
            var selectedTimePaymentSum = selectedTimePaymentsQuery.Sum(x => x.AmountPaid.Amount);


            var paymentsBeforeQuery = from p in _paymentActivitylogRepo.GetAll()
                                      where (p.TransactionAction == TransactionAction.PaidInvoice &&
                                      (p.CreationTime >= beforeStartDate && p.CreationTime <= startDate)
                                      && p.FacilityId == facilityId)
                                      select new
                                      {
                                          p.AmountPaid
                                      };
            var sumOfPaymentsBefore = paymentsBeforeQuery.Sum(x => x.AmountPaid.Amount);
            decimal percentIncreaseOrDecrease = 0.0m;
            if (selectedTimePaymentSum > 0 && sumOfPaymentsBefore == 0)
            {
                percentIncreaseOrDecrease = 100m;
            }
            else if (selectedTimePaymentSum == 0 && sumOfPaymentsBefore > 0)
            {
                percentIncreaseOrDecrease = -100m;
            }
            if (sumOfPaymentsBefore > 0 && selectedTimePaymentSum > 0)
            {
                percentIncreaseOrDecrease = ((selectedTimePaymentSum - sumOfPaymentsBefore) / sumOfPaymentsBefore) * 100;
            }

            return Task.FromResult(new GetAnalyticsResponseDto
            {
                Total = new MoneyDto { Amount = selectedTimePaymentSum },
                Difference = new MoneyDto { Amount = selectedTimePaymentSum - sumOfPaymentsBefore },
                PercetageIncreaseOrDecrease = percentIncreaseOrDecrease,
                Increased = percentIncreaseOrDecrease > 0
            });
        }
    }
}