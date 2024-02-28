using Plateaumed.EHR.Dto;
using Plateaumed.EHR.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Reports.PaymentTransactions.Exporting
{
    public interface ITransactionsExporter 
    {
        FileDto ExportReportToFile(string tenantName, List<PaymentTransactionDisplayResponseDto> transactions);
    }
}
