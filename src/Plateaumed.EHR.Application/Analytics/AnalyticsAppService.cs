using Abp.Authorization;
using Plateaumed.EHR.Analytics.Abstractions;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics
{
    [AbpAuthorize(AppPermissions.Pages_Administration)]
    public class AnalyticsAppService : EHRAppServiceBase, IAnalyticsAppService
    {
        private readonly IGetTotalActualInvoiceQueryHandler _queryHandler;
        private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacilityIdQueryHandler;
        private readonly ITotalWalletTopUpQueryHandler _getTotalWalletQueryHandler;
        private readonly IGetTotalPaidAmoundQueryHandler _getTotalPaymentsQueryHandler;
        private readonly IGetTotalOutstandingQueryHandler _getTotalOutstandingQueryHandler;
        private readonly IGetTopupAwaitingApprovalQueryHandler _getTopupAwaitingApprovalQueryHandler;
        private readonly IAnalyticsForCancelledVsUncancelledInvoiceQueryHandler _getAnalyticsForCancelledinvoiceQueryHandler;
        private readonly IGetTotalEdittedInvoiceVsTotalUnedittedAnalyticsQueryHandler _getEditedVsUnedited;

        public AnalyticsAppService(IGetTotalActualInvoiceQueryHandler queryHandler,
            IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacilityIdQueryHandler,
            ITotalWalletTopUpQueryHandler getTotalWalletQueryHandler,
            IGetTotalPaidAmoundQueryHandler getTotalPaymentsQueryHandler,
            IGetTotalOutstandingQueryHandler getTotalOutstandingQueryHandler,
            IGetTopupAwaitingApprovalQueryHandler getTopupAwaitingApproval,
            IAnalyticsForCancelledVsUncancelledInvoiceQueryHandler getAnalyticsForCancelledinvoiceQueryHandler,
            IGetTotalEdittedInvoiceVsTotalUnedittedAnalyticsQueryHandler getEditedVsUnedited)
        {
            _queryHandler = queryHandler;
            _getCurrentUserFacilityIdQueryHandler = getCurrentUserFacilityIdQueryHandler;
            _getTotalWalletQueryHandler = getTotalWalletQueryHandler;
            _getTotalPaymentsQueryHandler = getTotalPaymentsQueryHandler;
            _getTotalOutstandingQueryHandler = getTotalOutstandingQueryHandler;
            _getTopupAwaitingApprovalQueryHandler = getTopupAwaitingApproval;
            _getAnalyticsForCancelledinvoiceQueryHandler = getAnalyticsForCancelledinvoiceQueryHandler;
            _getEditedVsUnedited = getEditedVsUnedited;
        }


        public async Task<GetAnalyticsResponseDto> GetTotalActualInvoice(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _queryHandler.Handle(facilityId, selectionMode);
        }

        public async Task<GetAnalyticsResponseDto> GetTotalWalletTopup(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getTotalWalletQueryHandler.Handle(facilityId, selectionMode);
        }


        public async Task<GetAnalyticsResponseDto> GetTotalPayments(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getTotalPaymentsQueryHandler.Handle(facilityId, selectionMode);
        }

        public async Task<GetAnalyticsResponseDto> GetTotalOutstanding(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getTotalOutstandingQueryHandler.Handle(facilityId, selectionMode);
        }

        public async Task<GetAnalyticsResponseDto> GetTotalTopupAwaitingApproval(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getTopupAwaitingApprovalQueryHandler.Handle(facilityId, selectionMode);
        }

        public async Task<GetCountAnalyticsResponseDto> GetAnalyticsForCancelledInvoice(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getAnalyticsForCancelledinvoiceQueryHandler.Handle(facilityId, selectionMode);
        }

        public async Task<GetCountAnalyticsResponseDto> GetEditedInvoiceAnalytics(AnalyticsEnum selectionMode)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getEditedVsUnedited.Handle(facilityId, selectionMode);
        }
    }
}
