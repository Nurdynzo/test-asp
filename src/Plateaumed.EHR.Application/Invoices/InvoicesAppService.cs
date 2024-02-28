using Plateaumed.EHR.Patients;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Invoices.Dtos;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.PatientWallet.Abstractions;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Invoices.Dtos.PatientWithUnpaidInvoiceItemsDtos;
using Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using Plateaumed.EHR.Invoices.Dtos.PatientWithInvoiceItemsDtos;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.Invoices
{
    [AbpAuthorize(AppPermissions.Pages_Invoices)]
    public class InvoicesAppService : EHRAppServiceBase, IInvoicesAppService
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<Patient, long> _lookupPatientRepository;
        private readonly IRepository<PatientAppointment, long> _lookupPatientAppointmentRepository;
        private readonly IGenerateInvoiceCommandHandler _generateInvoiceCommandHandler;
        private readonly IGetInvoiceItemsForPricingQuery _getInvoiceItemsForPricingQuery;
        private readonly ICreateNewInvoiceCommandHandler _createNewInvoiceCommandHandler;
        private readonly IGetMostRecentBillQueryHandler _getMostRecentBillQueryHandler;
        private readonly IGetPaymentSummaryQueryHandler _getPaymentSummaryQueryHandler;
        private readonly IConvertProformaIntoInvoiceCommandHandler _convertProformaIntoInvoiceCommandHandler;
        private readonly IGetInvoicePaymentBillListQueryHandler _getInvoicePaymentBillListQueryHandler;
        private readonly IUpdateInvoiceCommandHandler _updateInvoiceCommandHandler;
        private readonly IGetPaymentLandingListHeader _getPaymentLandingListHeader;
        private readonly IGetPaymentExpandableQueryHandler _getPaymentExpandableQueryHandler;
        private readonly IGetInvoiceAndInvoiceItemsByIdQueryHandler _getInvoiceAndInvoiceItemsByIdQueryHandler;
        private readonly IGetAllUnpaidInvoicesQueryHandler _getAllUnpaidInvoicesQueryHandler;
        private readonly IGetTotalPaymentSummaryByPatientIdQuery _getTotalPaymentSummaryByPatientIdQuery;
        private readonly IPayInvoicesCommandHandler _payInvoicesCommandHandler;
        private readonly ICreateWalletFundingRequestHandler _createWalletFundingRequestHandler;
        private readonly IGetPatientsWithInvoiceItemsQueryHandler _getPatientsWithUnpaidInvoiceItems;
        private readonly IGetAllProformaInvoiceQueryHandler _getAllProformaInvoiceQueryHandler;
        private readonly IGetPatientInvoicesAndWalletTransactionsQueryHandler _getPatientInvoicesAndWalletTransactionsQueryHandler;
        private readonly IGetInvoiceForCancelRequestQueryHandler _getInvoiceForCancelRequestQueryHandler;
        private readonly IGetCancelRequestForApprovalQueryHandler _getCancelRequestForApprovalQueryHandler;
        private readonly IProcessInvoiceCancelRequestCommandHandler _processInvoiceCancelRequestCommandHandler;
        private readonly IPaymentLandingListQueryHandler _paymentLandingListQueryHandler;
        private readonly ICreateCancelInvoiceCommandHandler _createCancelInvoiceCommandHandler;
        private readonly IGetItemsToApplyReliefQueryHandler _getItemsToApplyReliefQueryHandler;
        private readonly IApproveDebtReliefCommandHandler _approveDebtReliefCommandHandler;
        private readonly IGetEditedInvoiceQueryHandler _getEditedInvoiceQueryHandler;
        private readonly IGetEditedInvoiceForDownloadQueryHandler _getEditedInvoiceForDownloadQueryHandler;
        private readonly IGetEditedInvoiceItemsQueryHandler _getEditedInvoiceItemsQueryHandler;
        private readonly IMarkAsClearedCommandHandler _markAsClearedCommandHandler;
        private readonly ICreateInvestigationInvoiceCommandHandler _createNewInvestigationInvoiceCommandHandler;
        private readonly IGetInvoicesToApplyCreditRequestHandler _getInvoicesToApplyCreditRequestHandler;

        public InvoicesAppService(
            IRepository<Invoice, long> invoiceRepository,
            IRepository<Patient, long> lookupPatientRepository,
            IRepository<PatientAppointment, long> lookupPatientAppointmentRepository,
            IGenerateInvoiceCommandHandler generateInvoiceCommandHandler,
            IGetInvoiceItemsForPricingQuery getInvoiceItemsForPricingQuery,
            ICreateNewInvoiceCommandHandler createNewInvoiceCommandHandler,
            IGetPaymentExpandableQueryHandler getPaymentExpandableQueryHandler,
            IGetPaymentLandingListHeader getPaymentLandingListHeader,
            IGetMostRecentBillQueryHandler getMostRecentBillQueryHandler,
            IConvertProformaIntoInvoiceCommandHandler convertProformaIntoInvoiceCommandHandler,
            IGetPaymentSummaryQueryHandler getPaymentSummaryQueryHandler,
            IGetInvoicePaymentBillListQueryHandler getInvoicePaymentBillListQueryHandler,
            IUpdateInvoiceCommandHandler updateInvoiceCommandHandler,
            IPayInvoicesCommandHandler payInvoicesCommandHandler,
            ICreateWalletFundingRequestHandler createWalletFundingRequestHandler,
            IGetInvoiceAndInvoiceItemsByIdQueryHandler getInvoiceAndInvoiceItemsByIdQueryHandler,
            IGetAllUnpaidInvoicesQueryHandler getAllUnpaidInvoicesQueryHandler,
            IGetPatientsWithInvoiceItemsQueryHandler getPatientsWithUnpaidInvoiceItems,
            IGetPatientInvoicesAndWalletTransactionsQueryHandler getPatientInvoicesAndWalletTransactionsQueryHandler,
            IGetTotalPaymentSummaryByPatientIdQuery getTotalPaymentSummaryByPatientIdQuery,
            IGetAllProformaInvoiceQueryHandler getAllProformaInvoiceQueryHandler,
            IGetInvoiceForCancelRequestQueryHandler getInvoiceForCancelRequestQueryHandler,
            IGetCancelRequestForApprovalQueryHandler getCancelRequestForApprovalQueryHandler,
            IProcessInvoiceCancelRequestCommandHandler processInvoiceCancelRequestCommandHandler,
            ICreateCancelInvoiceCommandHandler createCancelInvoiceCommandHandler,
            IGetItemsToApplyReliefQueryHandler getItemsToApplyReliefQueryHandler,
            IApproveDebtReliefCommandHandler approveDebtReliefCommandHandler,
            IPaymentLandingListQueryHandler paymentLandingListQueryHandler,
            IGetEditedInvoiceQueryHandler getEditedInvoiceQueryHandler,
            IGetEditedInvoiceItemsQueryHandler getEditedInvoiceItemsQueryHandler,
            IGetEditedInvoiceForDownloadQueryHandler getEditedInvoiceForDownloadQueryHandler,
            IMarkAsClearedCommandHandler markAsClearedCommandHandler,
            ICreateInvestigationInvoiceCommandHandler createNewInvestigationInvoiceCommandHandler,
            IGetInvoicesToApplyCreditRequestHandler getInvoicesToApplyCreditRequestHandler)
        {
            _invoiceRepository = invoiceRepository;
            _lookupPatientRepository = lookupPatientRepository;
            _lookupPatientAppointmentRepository = lookupPatientAppointmentRepository;
            _generateInvoiceCommandHandler = generateInvoiceCommandHandler;
            _getInvoiceItemsForPricingQuery = getInvoiceItemsForPricingQuery;
            _createNewInvoiceCommandHandler = createNewInvoiceCommandHandler;
            _getMostRecentBillQueryHandler = getMostRecentBillQueryHandler;
            _getPaymentSummaryQueryHandler = getPaymentSummaryQueryHandler;
            _getInvoicePaymentBillListQueryHandler = getInvoicePaymentBillListQueryHandler;
            _updateInvoiceCommandHandler = updateInvoiceCommandHandler;
            _payInvoicesCommandHandler = payInvoicesCommandHandler;
            _convertProformaIntoInvoiceCommandHandler = convertProformaIntoInvoiceCommandHandler;
            _createWalletFundingRequestHandler = createWalletFundingRequestHandler;
            _getInvoiceAndInvoiceItemsByIdQueryHandler = getInvoiceAndInvoiceItemsByIdQueryHandler;
            _getAllUnpaidInvoicesQueryHandler = getAllUnpaidInvoicesQueryHandler;
            _getPatientInvoicesAndWalletTransactionsQueryHandler = getPatientInvoicesAndWalletTransactionsQueryHandler;
            _getTotalPaymentSummaryByPatientIdQuery = getTotalPaymentSummaryByPatientIdQuery;
            _getAllProformaInvoiceQueryHandler = getAllProformaInvoiceQueryHandler;
            _getPatientsWithUnpaidInvoiceItems = getPatientsWithUnpaidInvoiceItems;
            _getInvoiceForCancelRequestQueryHandler = getInvoiceForCancelRequestQueryHandler;
            _getCancelRequestForApprovalQueryHandler = getCancelRequestForApprovalQueryHandler;
            _processInvoiceCancelRequestCommandHandler = processInvoiceCancelRequestCommandHandler;
            _createCancelInvoiceCommandHandler = createCancelInvoiceCommandHandler;
            _getPaymentExpandableQueryHandler = getPaymentExpandableQueryHandler;
            _getPaymentLandingListHeader = getPaymentLandingListHeader;
            _paymentLandingListQueryHandler = paymentLandingListQueryHandler;
            _getItemsToApplyReliefQueryHandler = getItemsToApplyReliefQueryHandler;
            _approveDebtReliefCommandHandler = approveDebtReliefCommandHandler;
            _getEditedInvoiceQueryHandler = getEditedInvoiceQueryHandler;
            _getEditedInvoiceItemsQueryHandler = getEditedInvoiceItemsQueryHandler;
            _getEditedInvoiceForDownloadQueryHandler = getEditedInvoiceForDownloadQueryHandler;
            _markAsClearedCommandHandler = markAsClearedCommandHandler;
            _createNewInvestigationInvoiceCommandHandler = createNewInvestigationInvoiceCommandHandler;
            _getInvoicesToApplyCreditRequestHandler = getInvoicesToApplyCreditRequestHandler;
        }

        public async Task<PagedResultDto<GetInvoiceForViewDto>> GetAll(GetAllInvoicesInput input)
        {

            var filteredInvoices = _invoiceRepository.GetAll()
                .Include(e => e.PatientFk)
                .ThenInclude(e => e.PatientCodeMappings)
                .Include(e => e.PatientAppointmentFk)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.InvoiceId.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.InvoiceIdFilter), e => e.InvoiceId.Contains(input.InvoiceIdFilter))
                .WhereIf(input.MinTimeOfInvoicePaidFilter != null, e => e.TimeOfInvoicePaid >= input.MinTimeOfInvoicePaidFilter)
                .WhereIf(input.MaxTimeOfInvoicePaidFilter != null, e => e.TimeOfInvoicePaid <= input.MaxTimeOfInvoicePaidFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.PatientPatientCodeFilter),
                    e => e.PatientFk != null && e.PatientFk.PatientCodeMappings.Count != 0 &&
                         e.PatientFk.PatientCodeMappings.Any(x => x.PatientCode == input.PatientPatientCodeFilter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.PatientAppointmentTitleFilter), e => e.PatientAppointmentFk != null && e.PatientAppointmentFk.Title == input.PatientAppointmentTitleFilter);

            var pagedAndFilteredInvoices = filteredInvoices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var invoices = from o in pagedAndFilteredInvoices
                           join o1 in _lookupPatientRepository.GetAll() on o.PatientId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookupPatientAppointmentRepository.GetAll() on o.PatientAppointmentId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           select new
                           {

                               o.InvoiceId,
                               o.TimeOfInvoicePaid,
                               Id = o.Id,
                               PatientPatientCode =
                                   s1 == null || s1.PatientCodeMappings == null || s1.PatientCodeMappings.Count == 0
                                       ? ""
                                       : s1.PatientCodeMappings.FirstOrDefault().PatientCode.ToString(),
                               PatientAppointmentTitle = s2 == null || s2.Title == null ? "" : s2.Title.ToString()
                           };

            var totalCount = await filteredInvoices.CountAsync();

            var dbList = await invoices.ToListAsync();
            var results = new List<GetInvoiceForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetInvoiceForViewDto()
                {
                    Invoice = new InvoiceDto
                    {

                        InvoiceId = o.InvoiceId,
                        TimeOfInvoicePaid = o.TimeOfInvoicePaid,
                        Id = o.Id,
                    },
                    PatientPatientCode = o.PatientPatientCode,
                    PatientAppointmentTitle = o.PatientAppointmentTitle
                };

                results.Add(res);
            }

            return new PagedResultDto<GetInvoiceForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_Invoices_Edit)]
        public async Task<GetInvoiceForEditOutput> GetInvoiceForEdit(EntityDto<long> input)
        {
            var invoice = await _invoiceRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvoiceForEditOutput
            {
                Invoice = ObjectMapper.Map<CreateOrEditInvoiceDto>(invoice)
            };

            if (output.Invoice.PatientId != 0)
            {
                var _lookupPatient = await _lookupPatientRepository.GetAll().Include(x => x.PatientCodeMappings)
                    .FirstOrDefaultAsync(x => x.Id == output.Invoice.PatientId);
                output.PatientPatientCode = _lookupPatient?.PatientCodeMappings.FirstOrDefault()?.PatientCode;
            }

            if (output.Invoice.PatientAppointmentId != null)
            {
                var _lookupPatientAppointment = await _lookupPatientAppointmentRepository.FirstOrDefaultAsync((long)output.Invoice.PatientAppointmentId);
                output.PatientAppointmentTitle = _lookupPatientAppointment?.Title?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvoiceDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Invoices_Create)]
        protected virtual async Task Create(CreateOrEditInvoiceDto input)
        {
            var invoice = ObjectMapper.Map<Invoice>(input);

            if (AbpSession.TenantId != null)
            {
                invoice.TenantId = (int?)AbpSession.TenantId;
            }

            await _invoiceRepository.InsertAsync(invoice);

        }

        [AbpAuthorize(AppPermissions.Pages_Invoices_Edit)]
        protected virtual async Task Update(CreateOrEditInvoiceDto input)
        {
            var invoice = await _invoiceRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, invoice);

        }

        [AbpAuthorize(AppPermissions.Pages_Invoices_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            await _invoiceRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Invoices)]
        public async Task<PagedResultDto<InvoicePatientLookupTableDto>> GetAllPatientForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookupPatientRepository.GetAll()
                .Include(x => x.PatientCodeMappings)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.PatientCodeMappings != null && e.PatientCodeMappings.Any(x => x.PatientCode.Contains(input.Filter))
                );

            var totalCount = await query.CountAsync();

            var patientList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvoicePatientLookupTableDto>();
            foreach (var patient in patientList)
            {
                lookupTableDtoList.Add(new InvoicePatientLookupTableDto
                {
                    Id = patient.Id,
                    DisplayName = patient.PatientCodeMappings.FirstOrDefault()?.PatientCode
                });
            }

            return new PagedResultDto<InvoicePatientLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Invoices)]
        public async Task<PagedResultDto<InvoicePatientAppointmentLookupTableDto>> GetAllPatientAppointmentForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookupPatientAppointmentRepository.GetAll().WhereIf(
                !string.IsNullOrWhiteSpace(input.Filter),
                e => e.Title != null && e.Title.Contains(input.Filter)
            );

            var totalCount = await query.CountAsync();

            var patientAppointmentList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvoicePatientAppointmentLookupTableDto>();
            foreach (var patientAppointment in patientAppointmentList)
            {
                lookupTableDtoList.Add(new InvoicePatientAppointmentLookupTableDto
                {
                    Id = patientAppointment.Id,
                    DisplayName = patientAppointment.Title?.ToString()
                });
            }

            return new PagedResultDto<InvoicePatientAppointmentLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        /// <summary>
        /// Generate Invoice Code
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Invoices)]
        [HttpGet]
        public async Task<string> GenerateInvoiceCode()
        {
            return await _generateInvoiceCommandHandler.Handle(new GenerateInvoiceCommand()
            {
                TenantId = AbpSession.TenantId.GetValueOrDefault()
            });
        }

        public async Task ConvertProformaIntoInvoice(ProformaToNewInvoiceRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _convertProformaIntoInvoiceCommandHandler.Handle(request, facilityId);
        }
        /// <summary>
        /// Get list of invoice items for pricing
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Invoices)]
        public async Task<PagedResultDto<GetInvoiceItemPricingResponse>> GetInvoiceItemsForPricing(GetInvoiceItemPricingRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getInvoiceItemsForPricingQuery.Handle(request,facilityId);
        }
        /// <summary>
        /// Create new invoice
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Invoices_Create)]
        public async Task<CreateNewInvoiceCommand> CreateNewInvoice(CreateNewInvoiceCommand command)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _createNewInvoiceCommandHandler.Handle(command, facilityId);
        }

        /// <summary>
        /// Create New Invoice for Investigation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Invoices_Create)]
        public async Task<CreateNewInvestigationInvoiceCommand> CreateNewInvestigationInvoice(CreateNewInvestigationInvoiceCommand command)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _createNewInvestigationInvoiceCommandHandler.Handle(command, facilityId);
        }

        /// <summary>
        /// Get most recent bill
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Invoices)]
        public async Task<GetMostRecentBillResponse> GetMostRecentBill(long patientId)
        {
            return await _getMostRecentBillQueryHandler.Handle(patientId);
        }

        /// <summary>
        /// Get payment summary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPaymentSummaryQueryResponse>> GetPaymentSummary(
            GetPaymentSummaryQueryRequest request)
        {
            return await _getPaymentSummaryQueryHandler.Handle(request);
        }

        /// <summary>
        /// Get payment bills by patient id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<InvoiceReceiptQueryResponse> GetPaymentBillsByPatientId(GetInvoicePaymentBillListQueryRequest request)
        {
            return await _getInvoicePaymentBillListQueryHandler.Handle(request);
        }

        /// <summary>
        /// Get all unpaid invoices
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UnpaidInvoicesResponse> GetAllUnpaidInvoices(UnpaidInvoicesRequest request)
        {
            return await _getAllUnpaidInvoicesQueryHandler.Handle(request);
        }

        /// <summary>
        /// Update invoice with the invoice id
        /// </summary>
        /// <param name="request"></param>
        public async Task UpdateInvoiceById(UpdateNewInvoiceRequest request)
        {
            await _updateInvoiceCommandHandler.Handle(request);
        }

        /// <summary>
        /// Get invoice and invoice items by id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public async Task<GetInvoiceQueryResponse> GetInvoiceById(long invoiceId)
        {
            return await _getInvoiceAndInvoiceItemsByIdQueryHandler.Handle(invoiceId);
        }

        /// <summary>
        /// Fund wallet or Make payment for invoices
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task FundAndFinalize(WalletFundingRequestDto request)
        {
            var facilityId = GetCurrentUserFacilityId();

            if (request.AmountToBeFunded == null || request.AmountToBeFunded?.Amount < 0)
            {
                await _payInvoicesCommandHandler.Handle(request);
                return;
            }


            await _createWalletFundingRequestHandler.Handle(request, facilityId, AbpSession.TenantId.GetValueOrDefault());
        }

        /// <summary>
        /// Get all patients with unpaid invoice items
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PatientsWithInvoiceItemsResponse>> GetPatientsWithInvoiceItems(PatientsWithInvoiceItemsRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getPatientsWithUnpaidInvoiceItems.Handle(request, facilityId);
        }

        /// <summary>
        /// Get total payment summary by patient id
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>

        public async Task<GetPatientTotalSummaryQueryResponse> GetPatientTotalSummaryHeader(long patientId)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getTotalPaymentSummaryByPatientIdQuery.Handle(patientId, facilityId);
        }

        /// <summary>
        /// Get a patient's invoices and wallet transactions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PatientInvoicesAndWalletTransactionsResponse>> GetPatientInvoicesAndWalletTransactions(PatientInvoicesAndWalletTransactionsRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getPatientInvoicesAndWalletTransactionsQueryHandler.Handle(request, facilityId);
        }

        /// <summary>
        /// Get invoice for cancel
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<List<GetInvoiceForCancelQueryResponse>> GetInvoiceForCancel(long patientId)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getInvoiceForCancelRequestQueryHandler.Handle(patientId, facilityId);
        }

        /// <summary>
        /// Get cancel request for approval
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<List<GetInvoiceForCancelQueryResponse>> GetCancelRequestForApproval(long patientId)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getCancelRequestForApprovalQueryHandler.Handle(patientId, facilityId);
        }

        /// <summary>
        /// Process cancel request for approval or reject
        /// </summary>
        /// <param name="request"></param>
        public async Task ProcessPendingCancelRequest(ApproveCancelInvoiceCommand request)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _processInvoiceCancelRequestCommandHandler.Handle(request, facilityId);
        }

        /// <summary>
        /// Get payment landing list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPaymentLadingListQueryResponse>> GetPaymentLandingList(
            PaymentLandingListFilterRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _paymentLandingListQueryHandler.Handle(request, facilityId);

        }
        public async Task<GetAllProformaInvoiceQueryResponse> GetAllProformaInvoiceByPatientId(long patientId)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getAllProformaInvoiceQueryHandler.Handle(patientId, facilityId);
        }

        /// <summary>
        /// Create new cancel invoice request for approval
        /// </summary>
        /// <param name="command"></param>
        public async Task CreateCancelInvoice(CreateCancelInvoiceCommand command)
        {
            var facilityId = GetCurrentUserFacilityId();
            await _createCancelInvoiceCommandHandler.Handle(command, facilityId);
        }

        /// <summary>
        /// Get payment expandable query 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetPaymentExpandableQueryResponse>> GetPaymentExpandable(
            GetPaymentExpandableQueryRequest request)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getPaymentExpandableQueryHandler.Handle(request, facilityId);
        }

        /// <summary>
        /// Get total payment summary header
        /// </summary>
        /// <returns></returns>
        public async Task<GetPatientTotalSummaryQueryResponse> GetPaymentLandingListHeader()
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _getPaymentLandingListHeader.Handle(facilityId);
        }

        /// <summary>
        /// Get all the invoice items to apply relief
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<List<ApplyReliefInvoiceViewDto>> GetInvoiceItemsToApplyDebtRelief(long patientId)
        {
            return await _getItemsToApplyReliefQueryHandler.Handle(patientId);
        }

        /// <summary>
        /// Apply debt relief to selected invoice
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ApplyDebtReliefToInvoice(ApproveDebtReliefRequestDto request)
        {
            request.FacilityId = GetCurrentUserFacilityId();
            await _approveDebtReliefCommandHandler.Handle(request);
        }


        /// <summary>
        /// Get a list of edited invoices with date range
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GetEditedInvoiceResponseDto>> GetEditedInvoices(GetEditedInvoiceRequestDto request)
        {
            return await _getEditedInvoiceQueryHandler.Handle(request,GetCurrentUserFacilityId());
        }


        /// <summary>
        /// Get a list of edited invoices items per invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public async Task<List<GetEditedInvoiceItemResponseDto>> GetEditedInvoiceItems(long invoiceId)
        {
            return await _getEditedInvoiceItemsQueryHandler.Handle(invoiceId);
        }

        /// <summary>
        /// Get edited invoice for download
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetEditedInvoiceForDownloadResponse>> GetEditedInvoiceForDownload(GetEditedInvoiceForDownloadRequest request) =>
            await _getEditedInvoiceForDownloadQueryHandler.Handle(request, GetCurrentUserFacilityId());

        /// <summary>
        /// Mark invoice as cleared 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public async Task MarkInvoiceAsCleared(long invoiceId)
        {
            await _markAsClearedCommandHandler.Handle(invoiceId);
        }

        public async Task<List<GetInvoiceToApproveCrediteDto>> GetInvoiceToApplyCredit(long patienId)
        {
            return await _getInvoicesToApplyCreditRequestHandler.Handle(patienId);
        }
    }
}
