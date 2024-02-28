using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    public class FacilityBanksAppService_Tests : AppTestBase
    {
        private readonly IFacilityBanksAppService _facilityBanksAppService;

        public FacilityBanksAppService_Tests()
        {
            LoginAsDefaultTenantAdmin();
            _facilityBanksAppService = Resolve<IFacilityBanksAppService>();
        }

        [Fact]
        public async Task CreateFacilityBankDetails_GivenValidData_ShouldSave()
        {
            // Arrange

            var facilityBank2 = new TestFacilityBankDetailsBuilder()
                .WithBankName("TestBank2")
                .WithBankAccountHolder("TestAccountHolder2")
                .WithBankAccountNumber("1234567")
                .IsDefault(false)
                .IsActive(true)
                .WithFacilityId(1)
                .Build();

            var facilityBank = new CreateOrEditBankRequest
            {
                Id = null,
                BankName = "TestBank2",
                BankAccountHolder = "TestAccount",
                BankAccountNumber = "123456789",
                IsActive = true,
                IsDefault = false,
                FacilityId = 1,
            };

            // Act
            await _facilityBanksAppService.CreateOrEdit(facilityBank);

            // Assert
            var createdBankDetails = await UsingDbContextAsync(async context =>
            {
                return await context.FacilityBanks.FirstOrDefaultAsync(x => x.BankName == "TestBank2");
            });

            createdBankDetails.BankName.ShouldBe("TestBank2");
        }

        [Fact]
        public async Task CreateFacilityBankDetails_GivenMultipleValidData_ShouldSaveMultipleBanks()
        {
            // Arrange
            var facilityBank1 = new CreateOrEditBankRequest
            {
                Id = null,
                BankName = "TestBank1",
                BankAccountHolder = "TestAccountHolder1",
                BankAccountNumber = "1234567890",
                IsActive = true,
                IsDefault = false,
                FacilityId = 1,
            };

            var facilityBank2 = new CreateOrEditBankRequest
            {
                Id = null,
                BankName = "TestBank2",
                BankAccountHolder = "TestAccountHolder2",
                BankAccountNumber = "1234567891",
                IsActive = true,
                IsDefault = false,
                FacilityId = 1,
            };

            var facilityBank3 = new CreateOrEditBankRequest
            {
                Id = null,
                BankName = "TestBank3",
                BankAccountHolder = "TestAccountHolder3",
                BankAccountNumber = "1234567892",
                IsActive = true,
                IsDefault = true,
                FacilityId = 1,
            };

            var banksToCreate = new List<CreateOrEditBankRequest> { facilityBank1, facilityBank2, facilityBank3 };

            // Act
            await _facilityBanksAppService.CreateOrEditMultipleBanks(banksToCreate);

            // Assert
            var createdBankDetails = new List<FacilityBank>();

            await UsingDbContextAsync(async context =>
            {
                foreach (var bank in banksToCreate)
                {
                    var createdBank = await context.FacilityBanks.FirstOrDefaultAsync(x => x.BankName == bank.BankName);
                    createdBank.ShouldNotBeNull();
                    createdBankDetails.Add(createdBank);
                }
            });

            createdBankDetails.Count.ShouldBe(banksToCreate.Count);
            foreach (var createdBank in createdBankDetails)
            {
                banksToCreate.ShouldContain(bank =>
                    bank.BankName == createdBank.BankName &&
                    bank.BankAccountHolder == createdBank.BankAccountHolder &&
                    bank.BankAccountNumber == createdBank.BankAccountNumber &&
                    bank.IsActive == createdBank.IsActive &&
                    bank.FacilityId == createdBank.FacilityId);
            }
        }
    }
}
