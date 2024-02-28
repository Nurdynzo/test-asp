using Abp.Timing.Timezone;
using Plateaumed.EHR.DataExporting.Excel.NPOI;
using Plateaumed.EHR.Dto;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Reports.PaymentTransactions.Exporting
{
    public class TransactionsExporter : NpoiExcelExporterBase , ITransactionsExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        public TransactionsExporter(ITempFileCacheManager tempFileCacheManager,
            ITimeZoneConverter timeZoneConverter)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
        }

        public FileDto ExportReportToFile(string tenantName, List<PaymentTransactionDisplayResponseDto> transactions)
        {
            int index = 0;
            var fileHeaderName = tenantName;
            return CreateExcelPackage(
                $"Payment_Transactions_{tenantName}.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.CreateSheet(("All_Transactions_"));

                    AddHeader(sheet,
                        ("S/N"),
                        ("PATIENT ID"),
                        ("PATIENT NAME"),
                        ("AGE"),
                        ("GENDER"),
                        ("SERVICE CENTER"),
                        ("INVOICE TOTAL (₦)"),
                        ("AMOUNT PAID (₦)"),
                        ("OUTSTANDING AMOUNT (₦)"),
                        ("WALLET TOP-UPS TOTAL (₦)"),
                        ("INVOICE NO"),
                        ("TRANSACTION TYPE"),
                        ("TRANSACTION ACTION"),
                        ("DATE")
                        );


                    AddObjects(sheet, transactions,
                        _=> ++index,
                        _ => _.PatientId,
                        _ => _.PatientName,
                        _ => _.Age,
                        _ => _.Gender,
                        _ => _.ServiceCenter,
                        _ => _.InvoiceTotal,
                        _ => _.AmountPaid,
                        _ => _.OutStandingAmount,
                        _ => _.WalletTopUpTotal,
                        _ => _.InvoiceNo,
                        _ => _.TransactionType,
                        _ => _.TransactionAction,
                        _ => _.TransactionDate);


                    /*for (var i = 2; i <= transactions.Count + 3; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[8], "yyyy-mm-dd");
                    }*/

                    var newRow = sheet.CreateRow(transactions.Count + 1);
                    newRow.CreateCell(0).SetCellValue("Total");
                    newRow.CreateCell(6).SetCellValue(transactions.Sum(x => x.InvoiceTotal).ToString());
                    newRow.CreateCell(7).SetCellValue(transactions.Sum(x => x.AmountPaid).ToString());
                    newRow.CreateCell(8).SetCellValue(transactions.Sum(x => x.OutStandingAmount).ToString());
                    newRow.CreateCell(9).SetCellValue(transactions.Sum(x => x.WalletTopUpTotal).ToString());
                    for (var i = 0; i < transactions.Count; i++)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                });
        }
    }
}
