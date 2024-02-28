using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access marital statuses
    /// </summary>
    public class MaritalStatusAppService : IMaritalStatusAppService
    {
        /// <summary>
        /// A method to access marital statuses
        /// </summary>
        /// <returns></returns>
        public List<string> GetMaritalStatuses()
        {
            return Enum
                .GetValues<MaritalStatus>()
                .AsEnumerable()
                .Select(m => m.ToString())
                .ToList();
        }
    }
}
