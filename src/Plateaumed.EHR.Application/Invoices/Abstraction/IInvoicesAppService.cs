using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Invoices.Dtos.PatientWithUnpaidInvoiceItemsDtos;
using Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos;
using Plateaumed.EHR.Invoices.Dtos.PatientWithInvoiceItemsDtos;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IInvoicesAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoiceForViewDto>> GetAll(GetAllInvoicesInput input);

        Task<GetInvoiceForEditOutput> GetInvoiceForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditInvoiceDto input);

        Task<GetPatientTotalSummaryQueryResponse> GetPaymentLandingListHeader();

        Task Delete(EntityDto<long> input);

        Task<PagedResultDto<InvoicePatientLookupTableDto>> GetAllPatientForLookupTable(GetAllForLookupTableInput input);

        Task<PagedResultDto<InvoicePatientAppointmentLookupTableDto>> GetAllPatientAppointmentForLookupTable(GetAllForLookupTableInput input);
        Task<string> GenerateInvoiceCode();

        Task<PagedResultDto<GetInvoiceItemPricingResponse>> GetInvoiceItemsForPricing(
            GetInvoiceItemPricingRequest request);

        Task<CreateNewInvoiceCommand> CreateNewInvoice(CreateNewInvoiceCommand command);

        Task<PagedResultDto<GetPaymentExpandableQueryResponse>> GetPaymentExpandable(
            GetPaymentExpandableQueryRequest request);

        Task<InvoiceReceiptQueryResponse> GetPaymentBillsByPatientId(GetInvoicePaymentBillListQueryRequest request);

        Task<UnpaidInvoicesResponse> GetAllUnpaidInvoices(UnpaidInvoicesRequest request);

        Task ConvertProformaIntoInvoice(ProformaToNewInvoiceRequest request);

        Task<GetInvoiceQueryResponse> GetInvoiceById(long invoiceId);

        Task FundAndFinalize(WalletFundingRequestDto request);

        Task<GetAllProformaInvoiceQueryResponse> GetAllProformaInvoiceByPatientId(long patientId);

        Task<PagedResultDto<PatientInvoicesAndWalletTransactionsResponse>> GetPatientInvoicesAndWalletTransactions(PatientInvoicesAndWalletTransactionsRequest request);

        Task<GetPatientTotalSummaryQueryResponse> GetPatientTotalSummaryHeader(long patientId);

        Task<PagedResultDto<PatientsWithInvoiceItemsResponse>> GetPatientsWithInvoiceItems(PatientsWithInvoiceItemsRequest request);

        Task<List<GetInvoiceForCancelQueryResponse>> GetInvoiceForCancel(long patientId);

        Task ProcessPendingCancelRequest(ApproveCancelInvoiceCommand request);

        Task CreateCancelInvoice(CreateCancelInvoiceCommand command);
        Task<List<ApplyReliefInvoiceViewDto>> GetInvoiceItemsToApplyDebtRelief(long invoiceId);
        Task ApplyDebtReliefToInvoice(ApproveDebtReliefRequestDto request);

        Task<PagedResultDto<GetPaymentLadingListQueryResponse>> GetPaymentLandingList(
            PaymentLandingListFilterRequest request);

        Task<List<GetEditedInvoiceForDownloadResponse>> GetEditedInvoiceForDownload(GetEditedInvoiceForDownloadRequest request);

        Task<PagedResultDto<GetEditedInvoiceResponseDto>> GetEditedInvoices(GetEditedInvoiceRequestDto request);
        Task<List<GetEditedInvoiceItemResponseDto>> GetEditedInvoiceItems(long invoiceId);
        Task MarkInvoiceAsCleared(long invoiceId);
        Task<List<GetInvoiceToApproveCrediteDto>> GetInvoiceToApplyCredit(long patienId);

    }
}