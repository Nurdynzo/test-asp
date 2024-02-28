using Plateaumed.EHR.Invoices.Dtos.Analytics;
using System;

namespace Plateaumed.EHR.Analytics
{
    public static class AnalyticsUtility
    {
        public static GetTimeRangeDto GetTimeRange(this AnalyticsEnum selectionMode)
        {
            DateTime beforeStartDate;
            DateTime startDate;
            DateTime endDate;
            switch (selectionMode)
            {
                case AnalyticsEnum.ThisYear:
                    startDate = new DateTime(DateTime.Today.Year, 1, 1);
                    beforeStartDate = startDate.AddYears(1);
                    endDate = new DateTime(DateTime.Today.Year, 12, 31).AddDays(1).AddTicks(-1);
                    break;
                case AnalyticsEnum.ThisWeek:
                    DateTime today = DateTime.Today;
                    int diff = today.DayOfWeek - DayOfWeek.Sunday;
                    startDate = today.AddDays(-diff);
                    beforeStartDate = startDate.AddDays(-7);
                    endDate = startDate.AddDays(6);
                    break;
                case AnalyticsEnum.ThisMonth:
                    startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    beforeStartDate = startDate.AddMonths(-1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                    break;
                default:
                    startDate = DateTime.Today;
                    beforeStartDate = DateTime.Today.AddDays(-1);
                    endDate = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;
            }

            return new GetTimeRangeDto
            {
                StartDate = startDate,
                EndDate = endDate,
                BeforeStartDate = beforeStartDate,
            };
        } 
    }
}
