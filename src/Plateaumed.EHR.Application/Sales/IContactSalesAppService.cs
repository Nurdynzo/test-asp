using System.Threading.Tasks;
using Plateaumed.EHR.Net.Emailing.ContactSales;
//using Plateaumed.EHR.Sales.Dto;

namespace Plateaumed.EHR.Sales
{
    public interface IContactSalesAppService
    {
        Task RequestForQuotation(QuotationRequest request);
    }
}