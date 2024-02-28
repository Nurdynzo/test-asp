using Abp.Domain.Repositories;
using Plateaumed.EHR.Analytics.Abstractions;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using Plateaumed.EHR.PatientWallet;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics.Query
{
    public class GetTopupAwaitingApprovalQueryHandler : IGetTopupAwaitingApprovalQueryHandler
    {
        private readonly IRepository<WalletHistory, long> _walletHistoryRepository;

        public GetTopupAwaitingApprovalQueryHandler(IRepository<WalletHistory, long> walletHistoryRepository)
        {
            _walletHistoryRepository = walletHistoryRepository;
        }

        public Task<GetAnalyticsResponseDto> Handle(long facilityId, AnalyticsEnum selectionMode)
        {
            var timeRange = selectionMode.GetTimeRange();
            DateTime beforeStartDate = timeRange.BeforeStartDate;
            DateTime startDate = timeRange.StartDate;
            DateTime endDate = timeRange.EndDate;
            var query = from i in _walletHistoryRepository.GetAll()
                        where (i.Status == TransactionStatus.Pending && (i.CreationTime >= startDate
                        && i.CreationTime >= endDate)
                        && i.FacilityId == facilityId)
                        select new
                        {
                            i.Amount
                        };
            var totalAmountPending = query.Sum(x => x.Amount.Amount);

            return Task.FromResult(new GetAnalyticsResponseDto
            {
                Total = new MoneyDto { Amount = totalAmountPending }
            });
        }
    }
}
