using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Invoices.Dtos;

public class GetMostRecentBillResponse: EntityDto<long>
{
    public string PaymentStatus { get; set; }

    [DataType(DataType.Currency)]
    public MoneyDto TotalAmount { get; set; }
    
    public string IssuedBy { get; set; }

    public DateTime IssuedOn { get; set; }  
    
    public string Notes { get; set; }
    
    public string InvoiceNo { get; set; }
    public List<MostRecentBillItems> Items { get; set; }
}