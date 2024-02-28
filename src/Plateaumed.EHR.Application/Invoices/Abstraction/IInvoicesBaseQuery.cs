using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using System.Linq;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IInvoicesBaseQuery : ITransientDependency
    {

        public IQueryable<UnpaidInvoiceDto> GetPatientUnpaidInvoicesBaseQuery(long patientId, InvoiceItemStatus status = InvoiceItemStatus.Unpaid); 
    }
}
