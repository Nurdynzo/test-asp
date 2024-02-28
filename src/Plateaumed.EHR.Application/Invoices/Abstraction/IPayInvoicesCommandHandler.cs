using Abp.Dependency;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IPayInvoicesCommandHandler: ITransientDependency
    {

        Task Handle(WalletFundingRequestDto request);

    }
}
