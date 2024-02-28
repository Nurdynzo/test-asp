using Plateaumed.EHR.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Misc
{
    public class ServiceCenterAppService: IServiceCenterAppService
    {
        /// <summary>
        /// A method to access service centers
        /// </summary>
        /// <returns></returns>
        public List<string> GetServiceCenters()
        {
            return Enum
                .GetValues<ServiceCentreType>()
                .AsEnumerable()
                .Select(x => x.ToString())
                .ToList();
        }
    }
}
