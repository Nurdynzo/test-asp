using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.PatientWithUnpaidInvoiceItemsDtos;
using System.Threading.Tasks;
using Plateaumed.EHR.Invoices.Dtos.PatientWithInvoiceItemsDtos;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    /// <summary>
    /// Handler to get all patients with unpaid invoice items
    /// </summary>
    public interface IGetPatientsWithInvoiceItemsQueryHandler : ITransientDependency
    {
        /// <summary>
        /// Method to get all patients with unpaid invoice items 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        Task<PagedResultDto<PatientsWithInvoiceItemsResponse>> Handle(PatientsWithInvoiceItemsRequest request, long facilityId);
    }
}
