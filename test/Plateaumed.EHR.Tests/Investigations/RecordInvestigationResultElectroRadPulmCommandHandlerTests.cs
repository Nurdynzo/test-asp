using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class RecordInvestigationResultElectroRadPulmCommandHandlerTests : AppTestBase
    {
        private readonly IAbpSession _abpSession;

        private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();

        public RecordInvestigationResultElectroRadPulmCommandHandlerTests() => _abpSession = Resolve<IAbpSession>();

        [Fact]
        public async Task Handle_GivenValidValues_ShouldRecordInvestigationResult()
        {
            LoginAsDefaultTenantAdmin();

            var command = new ElectroRadPulmInvestigationResultRequestDto
            {
                PatientId = 1,
                InvestigationId = 1,
                InvestigationRequestId = 1,
                EncounterId = 5,                
                ResultDate = new DateOnly(2021, 1, 1),              
                ResultTime = new TimeOnly(1, 1, 1),                
                Conclusion = "Septic",               
                ReviewerId = 1,
                ImageFiles = GetFormFiles()
            };

            ElectroRadPulmInvestigationResult savedResult = null;
            var repository = Substitute.For<IRepository<ElectroRadPulmInvestigationResult, long>>();
            repository.InsertAsync(Arg.Do<ElectroRadPulmInvestigationResult>(result => savedResult = result)).ReturnsForAnyArgs(new ElectroRadPulmInvestigationResult { Id = 1 });

            var handler = CreateHandler(repository);
            // Act
            await handler.Handle(command, 1, true);
            await _unitOfWork.Received(1).Current.SaveChangesAsync();

            Assert.NotNull(savedResult);
            Assert.Equal(command.PatientId, savedResult.PatientId);
            Assert.Equal(command.InvestigationId, savedResult.InvestigationId);
            Assert.Equal(command.EncounterId, savedResult.EncounterId);
        }

        private RecordInvestigationResultElectroRadPulmCommandHandler CreateHandler(IRepository<ElectroRadPulmInvestigationResult, long> repository,
            IEncounterManager encounterManager = null)
        {
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(Arg.Any<long>()).Returns(new Patient());

            var investigation = Substitute.For<IRepository<Investigation, long>>();
            investigation.GetAsync(Arg.Any<long>()).Returns(new Investigation
            {
                Id = 1,
                Name = "Electrolyte",
                Type = "Electrophysiology"
            });

            var investigationRequestRepository = Substitute.For<IRepository<InvestigationRequest, long>>();
            investigationRequestRepository.GetAsync(Arg.Any<long>()).Returns(new InvestigationRequest
            {
                Id = 1,
                InvestigationId = 1,
                InvestigationStatus = InvestigationStatus.ReportReady,
                PatientId = 1,
                Investigation = new Investigation
                {
                    Id = 1,
                    Name = "Electrolyte",
                    Type = "Electrophysiology"
                }
            });

            var reviewer = Substitute.For<IRepository<InvestigationResultReviewer, long>>();
            reviewer.GetAsync(Arg.Any<long>()).Returns(new InvestigationResultReviewer
            {
                InvestigationResultId = 1,
                ApproverId = 1,
                TenantId = 1,
                FacilityId = 1
            });

            var staffMember = Substitute.For<IRepository<StaffMember, long>>();
            staffMember.GetAsync(Arg.Any<long>()).Returns(new StaffMember
            {
                Id = 1,
                UserId = 1
            });

            //IRepository<ElectroRadPulmInvestigationResultImages, long> _resultImages
            var resultImages = Substitute.For<IRepository<ElectroRadPulmInvestigationResultImages, long>>();
            resultImages.GetAsync(Arg.Any<long>()).Returns(new ElectroRadPulmInvestigationResultImages { ElectroRadPulmInvestigationResultId = 1, Id = 1 });

            var handler = new RecordInvestigationResultElectroRadPulmCommandHandler(repository, investigationRequestRepository, patientRepository,
                investigation, encounterManager ?? Substitute.For<IEncounterManager>(),
                _abpSession, _unitOfWork, staffMember, reviewer, resultImages, null);

            return handler;
        }

        private static List<IFormFile> GetFormFiles()
        {
            return new List<IFormFile>()
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
            };
        }
    }
}

