using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Plateaumed.EHR.CsvUpload.Abstraction;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.PriceSettings.Command;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.PriceSettings.UnitTest
{
    public class UploadPriceSettingsCommandHandlerTest
    {
        private readonly ICsvService _csvServiceMock = Substitute.For<ICsvService>();
        private readonly IAbpSession _abpSessionMock = Substitute.For<IAbpSession>();
        private readonly IRepository<ItemPricing,long> _itemPricingRepositoryMock
            = Substitute.For<IRepository<ItemPricing, long>>();

        [Fact]
        public async Task Handle_Upload_CSV_With_Null_File_Should_Throw_Exception()
        {
            //arrange
            MockDependencies();
            var handler = new UploadPriceSettingsCommandHandler(_itemPricingRepositoryMock, _abpSessionMock, _csvServiceMock);
            var request = new UpdatePricingCommandRequest
            {
                FacilityId = 1,
                FormFile = null
            };
            //act
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            //assert
            message.Message.ShouldBe("File is required");

        }
        [Fact]
        public async Task Handle_Upload_CSV_With_Wrong_File_Type_Should_Throw_Exception()
        {
            //arrange
            MockDependencies();
            var handler = new UploadPriceSettingsCommandHandler(_itemPricingRepositoryMock, _abpSessionMock, _csvServiceMock);
            var request = new UpdatePricingCommandRequest
            {
                FacilityId = 1,
                FormFile = MockFile.CreateInstance("file.txt")
            };
            //act
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            //assert
            message.Message.ShouldBe("Invalid file format. Only CSV files are allowed");
        }
        [Fact]
        public async Task Handle_Upload_CSV_With_Larger_File_Format_Should_Throw_Exception()
        {
            //arrange
            MockDependencies();
            var handler = new UploadPriceSettingsCommandHandler(_itemPricingRepositoryMock, _abpSessionMock, _csvServiceMock);
            var request = new UpdatePricingCommandRequest
            {
                FacilityId = 1,
                FormFile = MockFile.CreateInstance("file.csv",length:4 * 1024 * 1024)
            };
            //act
            var message = await Should.ThrowAsync<UserFriendlyException>(async () => await handler.Handle(request));
            //assert
            message.Message.ShouldBe("File size must be less than 3MB");

        }
        [Fact]
        public async Task Handle_Upload_CSV_Valid_Request_Should_Return_Success()
        {
            //arrange
            MockDependencies();
            var handler = new UploadPriceSettingsCommandHandler(_itemPricingRepositoryMock, _abpSessionMock, _csvServiceMock);
            var request = new UpdatePricingCommandRequest
            {
                FacilityId = 1,
                FormFile = MockFile.CreateInstance("file.csv")
            };
            //act
            await handler.Handle(request);
            //assert
            await _itemPricingRepositoryMock.Received(1).InsertAsync(Arg.Any<ItemPricing>());
        }
        private void MockDependencies()
        {

            _csvServiceMock.ProcessCsvFile<PricingCsvDto>(Arg.Any<IFormFile>())
                .Returns(new List<PricingCsvDto>()
                {
                    new()
                    {
                        Name = "test",
                        Amount = 100,
                        Currency = "NGN",
                        ItemId = "1",
                        PricingType = "GeneralPricing",
                        SubCategory = "test",
                        PricingCategory = "Consultation"
                    }

                });
            _abpSessionMock.TenantId.Returns(1);
        }

    }
}
