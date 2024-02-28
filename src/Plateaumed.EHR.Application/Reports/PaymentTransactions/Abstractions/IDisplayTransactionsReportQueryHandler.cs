using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using Plateaumed.EHR.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Reports.PaymentTransactions.Abstractions
{
    public interface IDisplayTransactionsReportQueryHandler : ITransientDependency
    {
        Task<List<DisplayPaymentTransactionsResponseDto>> Handle(DownloadPaymentActivityRequest request, long facilityId);
    }
}
