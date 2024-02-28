using Plateaumed.EHR.Invoices.Dtos.UnpaidInvoicesDtos;
using System.Threading.Tasks;
using Plateaumed.EHR.Dto;
using Abp.Dependency;

namespace Plateaumed.EHR.Reports.PaymentTransactions.Abstractions
{
    public interface IDownloadPaymentTransactionQueryHelper : ITransientDependency
    {
        Task<FileDto> Handle(DownloadPaymentActivityRequest request, long facilityId);
    }
}
