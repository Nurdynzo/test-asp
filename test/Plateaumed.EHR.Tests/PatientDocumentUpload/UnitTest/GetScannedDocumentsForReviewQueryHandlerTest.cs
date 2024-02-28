using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PatientDocument.Query;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest;

[Trait("Category", "Unit")]
public class GetScannedDocumentsForReviewQueryHandlerTest
{
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepositoryMock 
        = Substitute.For<IRepository<PatientCodeMapping,long>>();
    private readonly IRepository<Patient,long> _patientRepositoryMock = Substitute.For<IRepository<Patient,long>>();
    private readonly IRepository<PatientScanDocument,long> _patientScanDocumentRepositoryMock = Substitute.For<IRepository<PatientScanDocument,long>>();
    private readonly IAbpSession _abpSession= Substitute.For<IAbpSession>();

    
    [Fact]
    public async Task Handle_ShouldReturnListOfScannedDocumentsForReview()
    {
        // Arrange
        _patientCodeMappingRepositoryMock.GetAll().Returns(GetPatientCodeMappingsAsQueryable().BuildMock());
        _patientRepositoryMock.GetAll().Returns(GetPatientsAsQueryable().BuildMock());
        _patientScanDocumentRepositoryMock.GetAll().Returns(GetPatientScanDocumentsAsQueryable().BuildMock());
        _abpSession.UserId.Returns(1);
        var handler = new GetScannedDocumentsForReviewQueryHandler(
            _patientScanDocumentRepositoryMock,
            _patientRepositoryMock,
            _abpSession,
            _patientCodeMappingRepositoryMock);
        // Act
        var result = await handler.Handle();

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
        result[0].PatientFullName.ShouldBe("TestFirstName1 TestLastName1");
        result[0].PatientCode.ShouldBe("TestCode1");
        result[0].Gender.ShouldBe("Male");
        result[0].IsApproved.ShouldBeNull();
    }

    private IQueryable<PatientCodeMapping> GetPatientCodeMappingsAsQueryable()
    {
        return new List<PatientCodeMapping>()
        {
            new()
            {
                Id = 1,
                PatientCode = "TestCode1",
                PatientId = 1
            }
        }.AsQueryable();
    }

    private IQueryable<Patient> GetPatientsAsQueryable()
    {
        return new List<Patient>()
        {
            new()
            {
                Id = 1,
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                DateOfBirth = DateTime.Now,
                GenderType = GenderType.Male,
                ProfilePictureId = Guid.NewGuid().ToString()
            },
        }.AsQueryable();
    }

    private IQueryable<PatientScanDocument> GetPatientScanDocumentsAsQueryable()
    {
        return new List<PatientScanDocument>
        {
            new()
            {
                Id = 1,
                PatientCode = "TestCode1",
                FileId = Guid.NewGuid(),
                ReviewerId = 1,
                IsApproved = null
            },
            new()
            {
                Id = 2,
                PatientCode = "TestCode1",
                FileId = Guid.NewGuid(),
                ReviewerId = 1,
                IsApproved = null
            }
        }.AsQueryable();
    }


}


