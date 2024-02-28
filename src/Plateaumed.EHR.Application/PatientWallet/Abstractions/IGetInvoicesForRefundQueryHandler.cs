using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet.Abstractions;

public interface IGetInvoicesForRefundQueryHandler: ITransientDependency
{
    Task<List<GetInvoicesForRefundQueryResponse>> Handle(GetInvoicesForRefundQueryRequest request);
}