using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IMarkAsClearedCommandHandler : ITransientDependency
    {
        Task Handle(long invoiceId);
    }
}
