using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Analytics
{
    public enum AnalyticsEnum
    {
        [Description("Today")]
        Today,

        [Description("This week")]
        ThisWeek,

        [Description("This month")]
        ThisMonth,

        [Description("This year")]
        ThisYear
    }
}
