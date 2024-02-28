using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    /// <summary>
    /// Handler to get all unpaid invoices
    /// </summary>
    public interface IGetAllUnpaidInvoicesQueryHandler : ITransientDependency
    {
        /// <summary>
        /// A method to handle query to get all unpaid invoices
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UnpaidInvoicesResponse> Handle(UnpaidInvoicesRequest request);

    }
}
