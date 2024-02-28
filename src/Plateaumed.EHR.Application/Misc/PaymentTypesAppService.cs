using System;
using System.Collections.Generic;
using System.Linq;

namespace Plateaumed.EHR.Misc
{
    /// <summary>
    /// Payment Types service
    /// </summary>
    public class PaymentTypesAppService : IPaymentTypesAppService
    {
        /// <summary>
        /// An api to return payment types supported
        /// </summary>
        /// <returns></returns>
        public List<string> GetPaymentTypes()
        {
            return new List<string>() { PaymentTypes.Wallet.ToString() };
        }
    }
}
