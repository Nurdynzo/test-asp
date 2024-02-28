using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos.Analytics
{
    public class GetAnalyticsResponseDto
    {
        public MoneyDto Total { get; set; }
        public MoneyDto Difference { get; set; }
        public decimal PercetageIncreaseOrDecrease { get; set; }
        public bool Increased { get; set; }
    }
}
