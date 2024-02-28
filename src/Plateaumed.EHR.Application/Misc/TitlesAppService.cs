using System;
using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access titles
    /// </summary>
    public class TitlesAppService : ITitlesAppService
    {
        /// <summary>
        /// A method to access titles
        /// </summary>
        /// <returns></returns>
        public List<string> GetTitles()
        {
            return Enum
                .GetValues<TitleType>()
                .AsEnumerable()
                .Select(x => x.ToString())
                .ToList();
        }
    }
}
