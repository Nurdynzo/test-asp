using Abp.Authorization;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Net.Emailing.ContactSales;
//using Plateaumed.EHR.Sales.Dto;
using Plateaumed.EHR.Sales.Abstractions;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Sales
{
    [AbpAuthorize(AppPermissions.Pages_ContactSales)]
    public class ContactSalesAppService : EHRAppServiceBase, IContactSalesAppService
    {
        private readonly ISendSalesRFQCommandHandler _salesRFQCommandHandler;
        private readonly ISendUserRFQCommandHandler _userRFQCommandHandler;

        public ContactSalesAppService(
            ISendSalesRFQCommandHandler salesRFQCommandHandler,
            ISendUserRFQCommandHandler userRFQCommandHandler)
        {
            _salesRFQCommandHandler = salesRFQCommandHandler;
            _userRFQCommandHandler = userRFQCommandHandler;
        }

        [AbpAuthorize(AppPermissions.Pages_ContactSales_Create)]
        public async Task RequestForQuotation(QuotationRequest request)
        {
            await _salesRFQCommandHandler.Handle(request);
            await _userRFQCommandHandler.Handle(request);
        }
    }
}
