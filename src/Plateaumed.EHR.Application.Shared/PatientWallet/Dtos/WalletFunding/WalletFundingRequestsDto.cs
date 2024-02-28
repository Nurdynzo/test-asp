using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.PatientWallet.Dtos.WalletFunding
{
    public class WalletFundingRequestsDto
    {
        [Required]
        public long PatientId { get; set; }
    }
}
