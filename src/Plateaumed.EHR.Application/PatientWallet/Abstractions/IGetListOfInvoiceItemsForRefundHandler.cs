using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;

namespace Plateaumed.EHR.PatientWallet.Abstractions;

public interface IGetListOfInvoiceItemsForRefundHandler: ITransientDependency
{
    Task<List<RefundInvoiceQueryResponse>> Handle(RefundInvoiceQueryRequest request, long facilityId);
}