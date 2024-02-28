using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Invoices.Dtos.Analytics
{
    public class GetCountAnalyticsResponseDto
    {
        public int TotalCount { get; set; }
        public decimal PercentageIncreaseOrDecrease { get; set; }
        public bool Increased { get; set; }
    }
}
