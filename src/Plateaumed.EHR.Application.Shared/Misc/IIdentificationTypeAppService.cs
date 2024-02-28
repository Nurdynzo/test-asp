using System.Collections.Generic;
using Abp.Application.Services;
using Plateaumed.EHR.Misc.Dtos;

namespace Plateaumed.EHR.Misc
{
    public interface IIdentificationTypeAppService : IApplicationService
    {

        public List<string> GetIdentificationTypes();

        List<IdentificationTypeDto> GetIdentificationTypesWithLabel();
    }
}

