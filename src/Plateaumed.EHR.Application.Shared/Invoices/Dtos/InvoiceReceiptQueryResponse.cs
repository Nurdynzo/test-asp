using System.Collections.Generic;

namespace Plateaumed.EHR.Invoices.Dtos;

public class InvoiceReceiptQueryResponse
{
   public List<GetMostRecentBillResponse> InvoiceItems { get; set; }
   public List<GetMostRecentBillResponse> ReceiptItems { get; set; }
}