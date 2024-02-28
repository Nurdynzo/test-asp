using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.PhysicalExaminations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PhysicalExaminations;

[Trait("Category", "Unit")]
public class UploadPatientPhysicalExamImagesCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenWrong_UploadFile()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientPhysicalExaminationImageFile, long>>();
        
        var testCreateData = CreateTest_RequestData();
        testCreateData.ImageFiles.Add(new FormFile(baseStream: new MemoryStream(new byte[0]), baseStreamOffset: 0, length: 0, "pdf", "test.pdf")
        {
            Headers = new HeaderDictionary(),
            ContentType = "pdf"
        });
        
        var handler = new UploadPatientPhysicalExamImagesCommandHandler(_unitOfWork, _objectMapper, null, repository);
        
        // Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(testCreateData, true));
        
        // Assert 
        exception.Message.ShouldBe("Only image files are allowed"); 
    }
    
    [Fact]
    public async Task Handle_GivenCorrect_Request()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientPhysicalExaminationImageFile, long>>();
        
        var testCreateData = CreateTest_RequestData();
        
        var handler = new UploadPatientPhysicalExamImagesCommandHandler(_unitOfWork, _objectMapper, null, repository);
        
        // Act
        PatientPhysicalExaminationImageFile patientPhysicalExamination = null;
        await repository.InsertAsync(Arg.Do<PatientPhysicalExaminationImageFile>(j => patientPhysicalExamination = j));
        
        await handler.Handle(testCreateData, true);
        
        // Assert 
        patientPhysicalExamination.PatientPhysicalExaminationId.ShouldBe(testCreateData.PatientPhysicalExaminationId);
    }

    private static UploadPatientPhysicalExamImageDto CreateTest_RequestData()
    {
        return new UploadPatientPhysicalExamImageDto()
        {
            PatientPhysicalExaminationId = 1,
            ImageFiles = new List<IFormFile>()
            {
                new FormFile(baseStream: new MemoryStream(new byte[0]), baseStreamOffset: 0, length: 0, "imageFile", "test.jpg")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                },
                new FormFile(baseStream: new MemoryStream(new byte[0]), baseStreamOffset: 0, length: 0, "imageFile", "profile_pics.jpg")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                }
            }
        };
    }
}