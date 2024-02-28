using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}