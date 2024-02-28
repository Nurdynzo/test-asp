using Abp.Application.Services;
using System.Collections.Generic;

namespace Plateaumed.EHR.Misc
{
    public interface IPaymentTypesAppService : IApplicationService
    {
        List<string> GetPaymentTypes();
    }
}
