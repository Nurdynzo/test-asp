using System;

namespace Plateaumed.EHR.Invoices.Dtos;

public class PaymentAllInputResponse
{
    public long Id { get; set; }
    public string VisitName { get; set; }
    public DateTime DateVisited { get; set; }
}