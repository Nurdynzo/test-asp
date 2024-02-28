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
    public class TotalWalletTopUpQueryHandler : ITotalWalletTopUpQueryHandler
    {
        private readonly IRepository<PaymentActivityLog, long> _logRepository;
        public TotalWalletTopUpQueryHandler(IRepository<PaymentActivityLog, long> logRepository)
        {
            _logRepository = logRepository;
        }

        public Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            List<PaymentActivityLog> paymentBeforeSelectedTime = new();
            var timeRange = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeRange.BeforeStartDate;
            DateTime startDate = timeRange.StartDate;
            DateTime endDate = timeRange.EndDate;

            var selectTimeQuery = from p in _logRepository.GetAll()

                                  where (p.TransactionAction == TransactionAction.FundWallet
                                  && (p.CreationTime >= startDate && p.CreationTime <= endDate)
                                  && p.FacilityId == facilityId)
                                  select new
                                  {
                                      p.ToUpAmount
                                  };
            var selectedTopUpTotal = selectTimeQuery.Sum(x => x.ToUpAmount.Amount);
            var queryBeforeSelected = from p in _logRepository.GetAll()
                                      where (p.TransactionAction == TransactionAction.FundWallet &&
                                      (p.CreationTime >= beforeStartDate && p.CreationTime <= startDate)
                                      && p.FacilityId == facilityId)
                                      select new
                                      {
                                          p.ToUpAmount
                                      };
            var sumOfPreviousTopUps = queryBeforeSelected.Sum(x => x.ToUpAmount.Amount);
            decimal percentageIncreaseOrDecrease = 0.0m;
            if (selectedTopUpTotal > 0 && sumOfPreviousTopUps == 0)
            {
                percentageIncreaseOrDecrease = 100m;
            }
            else if (selectedTopUpTotal == 0 && sumOfPreviousTopUps > 0)
            {
                percentageIncreaseOrDecrease = -100m;
            }
            if (sumOfPreviousTopUps > 0 && selectedTopUpTotal > 0)
            {
                percentageIncreaseOrDecrease = ((selectedTopUpTotal - sumOfPreviousTopUps) / sumOfPreviousTopUps) * 100;
            }

            return Task.FromResult(new GetAnalyticsResponseDto
            {
                Total = new MoneyDto { Amount = selectedTopUpTotal },
                Difference = new MoneyDto { Amount = selectedTopUpTotal - sumOfPreviousTopUps },
                PercetageIncreaseOrDecrease = percentageIncreaseOrDecrease,
                Increased = percentageIncreaseOrDecrease > 0,
            });
        }
    }
}
