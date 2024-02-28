using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet
{
    public interface IPatientWalletAppService: IApplicationService
    {
        public Task<List<GetInvoicesForRefundQueryResponse>> GetInvoicesForRefund(
            GetInvoicesForRefundQueryRequest request);
        
        Task<List<RefundInvoiceQueryResponse>> GetListOfInvoiceItemsForRefund(
            RefundInvoiceQueryRequest request);
        
        Task<WalletFundingResponseDto> GetWalletFundingRequests(WalletFundingRequestsDto request);

        Task ApproveWalletFundingRequests(WalletFundingRequestDto request);

        public Task CreateWalletRefundRequest(InvoiceWalletRefundRequest request);

        Task<List<RefundInvoiceQueryResponse>> GetRefundRequestItemsForApproval(int patientId);

        Task ProcessRefundRequest(ProcessRefundRequestCommand request);
        
        Task<PagedResultDto<RefundRequestListQueryResponse>> GetAllRefundRequest(RefundRequestListQueryRequest request);
    }
    
}
