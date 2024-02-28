using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet.Abstractions;

public interface IProcessRefundRequestCommandHandler: ITransientDependency
{
    Task Handle(ProcessRefundRequestCommand request, long facilityId);
}