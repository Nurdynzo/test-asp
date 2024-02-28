using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.Misc
{
    public interface IServiceCenterAppService: IApplicationService
    {
        public List<string> GetServiceCenters();
    }
}
