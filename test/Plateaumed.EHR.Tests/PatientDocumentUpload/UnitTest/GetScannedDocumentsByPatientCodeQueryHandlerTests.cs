using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientDocument.Query;
using Plateaumed.EHR.Patients;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientDocumentUpload.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetScannedDocumentsByPatientCodeQueryHandlerTests
    {
        private readonly IRepository<PatientScanDocument, long> _patientScanDocumentRepository = Substitute.For<IRepository<PatientScanDocument, long>>();

        [Fact]
        public async Task Handle_GivenPatientHasNoScannedDocument_ShouldReturnEmptyList()
        {

            //Arrange
            const string patientCode = "AAA-12346-MMM";
            MockDependencies();
            var handler = CreateHandlerInstance();

            // Act
            var response = await handler.Handle(patientCode);

            // Assert
            response.Count.ShouldBe(0);
        }
        
        [Fact]
        public async Task Handle_GivenPatientHasScannedDocument_ShouldReturnOnlyApprovedScannedDocuments()
        {

            //Arrange
            const string patientCode = "AAA-1234-MMM";
            MockDependencies();
            var handler = CreateHandlerInstance();

            // Act
            var response = await handler.Handle(patientCode);

            // Assert
            response.Count.ShouldBe(1);
        }


        private static IQueryable<PatientScanDocument> GetScannedDocuments() { 
            return new List<PatientScanDocument>(){

            new()
            {
                Id = 1,
                PatientCode = "AAA-1234-MMM",
                FileId = Guid.NewGuid(),
                ReviewerId = 1,
                IsApproved = true
            },
            new()
            {
                Id = 2,
                PatientCode = "AAA-1234-MMM",
                FileId = Guid.NewGuid(),
                ReviewerId = 1,
                IsApproved = null
            },
            new()
            {
                Id = 3,
                PatientCode = "AAA-1234-MMM",
                FileId = Guid.NewGuid(),
                ReviewerId = 1,
                IsApproved = false
            },
            }.AsQueryable();
        }

        private void MockDependencies() {
            _patientScanDocumentRepository.GetAll().Returns(GetScannedDocuments().BuildMock());
        }

        private GetScannedDocumentsByPatientCodeQueryHandler CreateHandlerInstance() {
            return new GetScannedDocumentsByPatientCodeQueryHandler(_patientScanDocumentRepository);
        }

    }
}
