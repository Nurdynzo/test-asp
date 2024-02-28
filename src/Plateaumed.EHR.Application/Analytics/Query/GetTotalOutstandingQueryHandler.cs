using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using Plateaumed.EHR.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Analytics.Abstractions;

namespace Plateaumed.EHR.Analytics.Query
{
    public class GetTotalOutstandingQueryHandler : IGetTotalOutstandingQueryHandler
    {
        private readonly IRepository<PaymentActivityLog, long> _logRepository;

        public GetTotalOutstandingQueryHandler(IRepository<PaymentActivityLog, long> logRepository)
        {
            _logRepository = logRepository;
        }


        public Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            List<Invoice> invoiceBeforeSelectedTime = new();
            var timeSelection = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeSelection.BeforeStartDate;
            DateTime startDate = timeSelection.StartDate;
            DateTime endDate = timeSelection.EndDate;

            var selectedTimeOutstandingQuery = from i in _logRepository.GetAll()
                                            where ((i.CreationTime >= startDate && i.CreationTime <= endDate)
                                            && i.FacilityId == facilityId)
                                            select new
                                            {
                                                OutStanding = i.ActualAmount.Amount - i.AmountPaid.Amount
                                            };
            var selectedTimeOutstandingSum = selectedTimeOutstandingQuery.Sum(x => x.OutStanding);


            var OutstandingBeforeQuery = from i in _logRepository.GetAll()
                                     where ((i.CreationTime >= beforeStartDate && i.CreationTime <= startDate)
                                     && i.FacilityId == facilityId)
                                     select new
                                     {
                                         OutStanding = i.ActualAmount.Amount - i.AmountPaid.Amount
                                     };

            var sumOfOutstandingBefore = OutstandingBeforeQuery.Sum(x => x.OutStanding);
            decimal percentIncreaseOrDecrease = 0.0m;
            if (selectedTimeOutstandingSum > 0 && sumOfOutstandingBefore == 0)
            {
                percentIncreaseOrDecrease = 100m;
            }
            else if (selectedTimeOutstandingSum == 0 && sumOfOutstandingBefore > 0)
            {
                percentIncreaseOrDecrease = -100m;
            }
            if (sumOfOutstandingBefore > 0 && selectedTimeOutstandingSum > 0)
            {
                percentIncreaseOrDecrease = ((selectedTimeOutstandingSum - sumOfOutstandingBefore) / sumOfOutstandingBefore) * 100;
            }

            return Task.FromResult(new GetAnalyticsResponseDto
            {
                Total = new MoneyDto { Amount = selectedTimeOutstandingSum },
                Difference = new MoneyDto { Amount = selectedTimeOutstandingSum - sumOfOutstandingBefore },
                PercetageIncreaseOrDecrease = percentIncreaseOrDecrease,
                Increased = percentIncreaseOrDecrease > 0
            });
        }
    }
}
