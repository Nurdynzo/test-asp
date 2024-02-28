using Plateaumed.EHR.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Test.Base.TestData
{
    public class TestFacilityBankDetailsBuilder
    {
        private string _bankName;
        private string _bankAccountHolder;
        private string _bankAccountNumber;
        private bool _isDefault;
        private bool _isActive;
        private long _facilityId;

        public TestFacilityBankDetailsBuilder WithBankName(string bankName)
        {
            _bankName = bankName;
            return this;
        }

        public TestFacilityBankDetailsBuilder WithBankAccountHolder(string bankAccountHolder)
        {
            _bankAccountHolder = bankAccountHolder;
            return this;
        }

        public TestFacilityBankDetailsBuilder WithBankAccountNumber(string bankAccountNumber)
        {
            _bankAccountNumber = bankAccountNumber;
            return this;
        }

        public TestFacilityBankDetailsBuilder IsDefault(bool isDefault)
        {
            _isDefault = isDefault;
            return this;
        }

        public TestFacilityBankDetailsBuilder IsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public TestFacilityBankDetailsBuilder WithFacilityId(long facilityId)
        {
            _facilityId = facilityId;
            return this;
        }

        public FacilityBank Build()
        {
            return new FacilityBank
            {
                BankName = _bankName,
                BankAccountHolder = _bankAccountHolder,
                BankAccountNumber = _bankAccountNumber,
                IsDefault = _isDefault,
                IsActive = _isActive,
                FacilityId = _facilityId
            };
        }
    }

}
