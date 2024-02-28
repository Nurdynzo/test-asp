using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IGetItemsToApplyReliefQueryHandler : ITransientDependency
    {
        Task<List<ApplyReliefInvoiceViewDto>> Handle(long invoiceId);
    }
}
