﻿using System.Collections.Generic;

namespace Plateaumed.EHR.DashboardCustomization
{
    public class Dashboard
    {
        public string DashboardName { get; set; }

        public List<Page> Pages { get; set; }
    }
}
