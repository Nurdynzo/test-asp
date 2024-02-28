using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access relationships
    /// </summary>
    public class RelationshipAppService : IRelationshipAppService
    {
        /// <summary>
        /// A method to access relationships
        /// </summary>
        /// <returns></returns>
        public List<string> GetRelationships()
        {
            return Enum
                  .GetValues<Relationship>()
                  .AsEnumerable()
                  .Select(r => r.ToString())
                  .ToList();
        }
    }
}
