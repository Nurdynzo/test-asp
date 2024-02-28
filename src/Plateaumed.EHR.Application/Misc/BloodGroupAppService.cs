using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access blood groups
    /// </summary>
    public class BloodGroupAppService : IBloodGroupAppService
    {
        /// <summary>
        /// A method to access blood groups
        /// </summary>
        /// <returns></returns>
        public List<string> GetBloodGroups()
        {
            return Enum
                    .GetValues<BloodGroup>()
                    .AsEnumerable()
                    .Select(b => b.ToString())
                    .ToList();
        }

        public List<string> GetBloodGroupsBySearch(string searchText)
        {
            return Enum
                .GetValues<BloodGroup>()
                .AsEnumerable()
                .Select(b => b.ToString())
                .Where(x => x.ToLower().Contains(searchText.ToLower()))
                .ToList();
        }
    }
}
