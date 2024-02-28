using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Plateaumed.EHR.Facilities.Dto;
using Plateaumed.EHR.Facilities.Handler;
using Plateaumed.EHR.Upload.Abstraction;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    [Trait("Category", "Unit")]
    public class UploadFacilityLogoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidFile_ShouldGenerateGuidAndUpload()
        {
            // Arrange
            var request = new UploadFacilityLogoRequest { File = Substitute.For<IFormFile>() };

            Guid? logoId = null;
            var uploadService = Substitute.For<IUploadService>();
            await uploadService.UploadFile(Arg.Do<Guid>(x => logoId = x), 
                Arg.Any<Stream>(),
                Arg.Any<IDictionary<string, string>>());

            var handler = new UploadFacilityLogoCommandHandler(uploadService);
            // Act
            await handler.Handle(request);
            // Assert
            logoId.ShouldNotBe(null);
            logoId.ShouldNotBe(Guid.Empty);
        }

        [Fact]
        public async Task Handle_GivenValidFile_ShouldUploadFileWithMetaData()
        {
            // Arrange
            const string contentType = "image/png";
            const string filename = "test.png";
            var fileStream = new MemoryStream();

            var formFile = CreateFormFile(contentType, fileStream, filename);

            var request = new UploadFacilityLogoRequest { File = formFile };

            Stream stream = null;
            IDictionary<string, string> tags = null;

            var uploadService = Substitute.For<IUploadService>();
            await uploadService.UploadFile(Arg.Any<Guid>(),
                Arg.Do<Stream>(x => stream = x),
                Arg.Do<IDictionary<string, string>>(x => tags = x));

            var handler = new UploadFacilityLogoCommandHandler(uploadService);
            // Act
            await handler.Handle(request);
            // Assert
            stream.ShouldBe(fileStream);
            tags["FileType"].ShouldBe(contentType);
            tags["DocumentType"].ShouldBe("Facility Logo");
            tags["FileName"].ShouldBe(formFile.FileName);
        }

        private static IFormFile CreateFormFile(string contentType, MemoryStream fileStream, string filename)
        {
            var formFile = Substitute.For<IFormFile>();
            formFile.ContentType.Returns(contentType);
            formFile.OpenReadStream().Returns(fileStream);
            formFile.FileName.Returns(filename);
            return formFile;
        }
    }
}
