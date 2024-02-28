using Abp.Dependency;
using Plateaumed.EHR.PatientWallet.Dtos.WalletFunding;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientWallet.Abstractions
{
    /// <summary>
    /// Handler to get all invoices for wallet funding
    /// </summary>
    public interface IGetWalletFundingRequestsQueryHandler : ITransientDependency
    {

        /// <summary>
        /// Method to handle query to get all invoices for wallet funding
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
       Task<WalletFundingResponseDto> Handle(WalletFundingRequestsDto request);
    }
}
