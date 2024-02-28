
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet
{
    public class PatientWalletAppService : EHRAppServiceBase, IPatientWalletAppService
    {
        private readonly IGetInvoicesForRefundQueryHandler _getInvoiceForRefundQueryHandler;
        private readonly IGetListOfInvoiceItemsForRefundHandler _getListOfInvoiceItemsForRefundHandler;
        private readonly IGetWalletFundingRequestsQueryHandler _getWalletFundingRequestsQueryHandler;
        private readonly IApproveWalletFundingRequestsHandler _approveWalletFundingRequestsHandler;
        private readonly ICreateWalletRefundCommandHandler _createWalletRefundRequestHandler;
        private readonly IGetListOfRequestedRefundInvoiceItemForApprovalQueryHandler _getListOfRequestedRefundInvoiceItemForApprovalQueryHandler;
        private readonly IGetAllRefundRequestQueryHandler _getAllRefundRequestQueryHandler;
        private readonly IProcessRefundRequestCommandHandler _processRefundRequestCommandHandler;
        
        public PatientWalletAppService(IGetInvoicesForRefundQueryHandler getInvoiceForRefundQueryHandler,
            IGetListOfInvoiceItemsForRefundHandler getListOfInvoiceItemsForRefundHandler,
            IGetWalletFundingRequestsQueryHandler getWalletFundingRequestsQueryHandler,
            IApproveWalletFundingRequestsHandler approveWalletFundingRequestsHandler,
            ICreateWalletRefundCommandHandler createWalletRefundRequestHandler,
            IGetAllRefundRequestQueryHandler getAllRefundRequestQueryHandler,
            IGetListOfRequestedRefundInvoiceItemForApprovalQueryHandler getListOfRequestedRefundInvoiceItemForApprovalQueryHandler, IProcessRefundRequestCommandHandler processRefundRequestCommandHandler)
        {
            _getInvoiceForRefundQueryHandler = getInvoiceForRefundQueryHandler;
            _getListOfInvoiceItemsForRefundHandler = getListOfInvoiceItemsForRefundHandler;
            _getWalletFundingRequestsQueryHandler = getWalletFundingRequestsQueryHandler;
            _approveWalletFundingRequestsHandler = approveWalletFundingRequestsHandler;
            _createWalletRefundRequestHandler = createWalletRefundRequestHandler;
            _getListOfRequestedRefundInvoiceItemForApprovalQueryHandler = getListOfRequestedRefundInvoiceItemForApprovalQueryHandler;
            _getAllRefundRequestQueryHandler = getAllRefundRequestQueryHandler; 
            _processRefundRequestCommandHandler = processRefundRequestCommandHandler;
        }


        public async Task<PagedResultDto<RefundRequestListQueryResponse>> GetAllRefundRequest(
            RefundRequestListQueryRequest request) => await _getAllRefundRequestQueryHandler
            .Handle(request, GetCurrentUserFacilityId());
        
        public async Task<List<GetInvoicesForRefundQueryResponse>> GetInvoicesForRefund(
            GetInvoicesForRefundQueryRequest request)
        {
            return await _getInvoiceForRefundQueryHandler.Handle(request);
        }

        /// <summary>
        /// Create wallet refund request for approval
        /// </summary>
        /// <param name="request"></param>
        public async Task CreateWalletRefundRequest(InvoiceWalletRefundRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _createWalletRefundRequestHandler.Handle(request, facilityId);
        }

        /// <summary>
        /// Get invoices and wallet funding requests summed up.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WalletFundingResponseDto> GetWalletFundingRequests(WalletFundingRequestsDto request)
        {
            return await _getWalletFundingRequestsQueryHandler.Handle(request);
        }

        /// <summary>
        /// Approve all wallet funding requests.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ApproveWalletFundingRequests(WalletFundingRequestDto request)
        {
            await _approveWalletFundingRequestsHandler.Handle(request, AbpSession.TenantId.GetValueOrDefault());
        }

        /// <summary>
        /// Get list of invoice items for refund
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<RefundInvoiceQueryResponse>> GetListOfInvoiceItemsForRefund(
            RefundInvoiceQueryRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getListOfInvoiceItemsForRefundHandler.Handle(request, facilityId);
        }

        public async Task<List<RefundInvoiceQueryResponse>> GetRefundRequestItemsForApproval(int patientId)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getListOfRequestedRefundInvoiceItemForApprovalQueryHandler.Handle(patientId, facilityId);
        }
        /// <summary>
        /// Process pending refund request
        /// </summary>
        /// <param name="request"></param>
        public async Task ProcessRefundRequest(ProcessRefundRequestCommand request)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _processRefundRequestCommandHandler.Handle(request, facilityId);
        }
        

    }
}
