using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Reports.PaymentTransactions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Reports.PaymentTransactions
{
    [AbpAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class TransactionsExportService : EHRAppServiceBase, ITransactionsExportService
    {
        private readonly IDownloadPaymentTransactionQueryHelper _queryHelper;
        private readonly IGetCurrentUserFacilityIdQueryHandler _getCurrentUserFacilityIdQueryHandler;
        private readonly IDisplayTransactionsReportQueryHandler _displayTransactionsReportHandler;



        public TransactionsExportService(IDownloadPaymentTransactionQueryHelper queryHelper,
           IGetCurrentUserFacilityIdQueryHandler getCurrentUserFacilityIdQueryHandler,
           IDisplayTransactionsReportQueryHandler displayTransactionsReportHandler)
        {
            _queryHelper = queryHelper;
            _getCurrentUserFacilityIdQueryHandler = getCurrentUserFacilityIdQueryHandler;
            _displayTransactionsReportHandler = displayTransactionsReportHandler;
        }
        public async Task<FileResult> GetReport(DownloadPaymentActivityRequest input)
        {
            var facilityId = GetCurrentUserFacilityId();
            var data = await _queryHelper.Handle(input, facilityId);
            var stream = new MemoryStream(data.FileContent);
            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = $"Payment_Trasaction_{DateTime.Now}_Report.xlsx"
            };
        }

        public async Task<List<DisplayPaymentTransactionsResponseDto>> GetReportToDisplay(DownloadPaymentActivityRequest input)
        {
            var facilityId = GetCurrentUserFacilityId();
            return await _displayTransactionsReportHandler.Handle(input, facilityId);
        }
    }
}
