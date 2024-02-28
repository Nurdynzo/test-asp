using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plateaumed.EHR.MultiTenancy.HostDashboard.Dto;

namespace Plateaumed.EHR.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}