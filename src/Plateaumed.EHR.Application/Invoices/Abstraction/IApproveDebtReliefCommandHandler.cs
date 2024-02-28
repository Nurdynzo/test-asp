using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.InvoiceRelief;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IApproveDebtReliefCommandHandler : ITransientDependency
    {
        Task Handle(ApproveDebtReliefRequestDto request);
    }
}
