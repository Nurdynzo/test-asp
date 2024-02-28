using System;
using System.Collections.Generic;
using Plateaumed.EHR.Patients;
using System.Linq;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access genotype
    /// </summary>
    public class GenoTypeAppService : IGenotypeAppService
    {
        /// <summary>
        /// A method to access genotype
        /// </summary>
        /// <returns></returns>
        public List<string> GetGenotypes()
        {
            return Enum
                    .GetValues<BloodGenotype>()
                    .AsEnumerable()
                    .Select(b => b.ToString())
                    .ToList();
        }

        public List<string> GetGenotypesBySearch(string searchText)
        {
            return Enum.GetValues<BloodGenotype>()
                .AsEnumerable()
                .Select(b => b.ToString())
                .Where(b => b.ToLower().Contains(searchText.ToLower()))
                .ToList();
        }
    }
}
