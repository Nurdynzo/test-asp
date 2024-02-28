using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet.Abstractions;

public interface ICreateWalletRefundCommandHandler: ITransientDependency
{
    Task Handle(InvoiceWalletRefundRequest request, long facilityId);
}