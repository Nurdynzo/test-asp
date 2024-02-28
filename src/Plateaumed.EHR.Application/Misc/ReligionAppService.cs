using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access religions
    /// </summary>
    public class ReligionAppService : IReligionAppService
    {
        /// <summary>
        /// A method to access religions
        /// </summary>
        /// <returns></returns>
        public List<string> GetReligions()
        {
            return Enum
                    .GetValues<Religion>()
                    .AsEnumerable()
                    .Select(r => r.ToString())
                    .ToList();
        }
    }
}
