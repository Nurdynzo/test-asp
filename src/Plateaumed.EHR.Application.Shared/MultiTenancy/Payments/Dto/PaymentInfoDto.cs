using Plateaumed.EHR.Editions.Dto;

namespace Plateaumed.EHR.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < EHRConsts.MinimumUpgradePaymentAmount;
        }
    }
}
