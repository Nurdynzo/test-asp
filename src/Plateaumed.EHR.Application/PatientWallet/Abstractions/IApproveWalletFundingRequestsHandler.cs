using Abp.Dependency;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.PatientWallet.Abstractions
{
    /// <summary>
    /// Handler to approve all wallet funding requests.
    /// </summary>
    public interface IApproveWalletFundingRequestsHandler : ITransientDependency
    {

        /// <summary>
        /// Method to handle approval of wallet funding requests.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task Handle(WalletFundingRequestDto request, int tenantId);
    }
}
