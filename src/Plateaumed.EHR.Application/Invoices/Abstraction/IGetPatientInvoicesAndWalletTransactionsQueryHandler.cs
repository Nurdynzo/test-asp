using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Invoices.Dtos.PatientInvoicesAndWalletTransactionsDtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Invoices.Abstraction
{
    public interface IGetPatientInvoicesAndWalletTransactionsQueryHandler : ITransientDependency
    {
        Task<PagedResultDto<PatientInvoicesAndWalletTransactionsResponse>> Handle(PatientInvoicesAndWalletTransactionsRequest request, long facilityId);
    }
}
