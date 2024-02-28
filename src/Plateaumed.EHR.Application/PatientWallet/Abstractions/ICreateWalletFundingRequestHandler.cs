using Abp.Dependency;
using System.Threading.Tasks;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;

namespace Plateaumed.EHR.PatientWallet.Abstractions
{
    /// <summary>
    /// handler for creating wallet funding request
    /// </summary>
    public interface ICreateWalletFundingRequestHandler : ITransientDependency
    {
        /// <summary>
        /// handles the creation of wallet funding request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="facilityId"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task Handle(WalletFundingRequestDto request, long facilityId, int tenantId);
    }
}
