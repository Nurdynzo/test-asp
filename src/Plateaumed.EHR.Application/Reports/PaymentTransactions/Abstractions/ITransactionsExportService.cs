using Abp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Reports.PaymentTransactions.Abstractions
{
    public interface ITransactionsExportService : IApplicationService
    {
        Task<FileResult> GetReport(DownloadPaymentActivityRequest input);
        Task<List<DisplayPaymentTransactionsResponseDto>> GetReportToDisplay(DownloadPaymentActivityRequest input);
    }
}
