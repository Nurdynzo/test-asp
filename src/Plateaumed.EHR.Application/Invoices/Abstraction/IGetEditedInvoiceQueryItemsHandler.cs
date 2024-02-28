using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IGetEditedInvoiceItemsQueryHandler : ITransientDependency
    {
        Task<List<GetEditedInvoiceItemResponseDto>> Handle(long invoiceId);
    }
}
