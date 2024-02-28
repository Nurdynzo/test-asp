using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// A service to access Identity types
    /// </summary>
    public class IdentificationTypeAppService : IIdentificationTypeAppService
    {
        /// <summary>
        /// A method to access Identity types
        /// </summary>
        /// <returns></returns>
        public List<string> GetIdentificationTypes()
        {
            return Enum
                .GetValues<IdentificationType>()
                .AsEnumerable()
                .Select(x => x.ToString())
                .ToList();
        }
        public List<IdentificationTypeDto> GetIdentificationTypesWithLabel()
        {
            return Enum
                .GetValues<IdentificationType>()
                .AsEnumerable()
                .Select(x => new IdentificationTypeDto(){
                    Value = x.ToString(),
                    Label =  Regex.Replace(x.ToString(), "([a-z])([A-Z])", "$1 $2")
                        })
                .ToList();
            
        }
    }
}