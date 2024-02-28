using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
namespace Plateaumed.EHR.PatientWallet.Abstractions
{
    public interface IGetAllRefundRequestQueryHandler: ITransientDependency
    {
        Task<PagedResultDto<RefundRequestListQueryResponse>> Handle(
            RefundRequestListQueryRequest request,
            long facilityId);
    }
}