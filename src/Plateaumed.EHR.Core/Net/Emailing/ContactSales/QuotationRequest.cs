using System.Collections.Generic;

namespace Plateaumed.EHR.Net.Emailing.ContactSales;

public class QuoteItem
{
    public bool Value { get; set; }
    public string Name { get; set; }
}

public class QuotationRequest
{
    public List<QuoteItem> Quotation { get; set; }
    public string Comment { get; set; }
}
