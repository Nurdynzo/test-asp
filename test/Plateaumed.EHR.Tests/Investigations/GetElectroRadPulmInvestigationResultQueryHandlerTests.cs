using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetElectroRadPulmInvestigationResultQueryHandlerTests
	{
        [Fact]
        public async Task HandleGivenValidDetailsShouldReturnInvestigationResultAndImages()
        {
            var repository = Substitute.For<IRepository<ElectroRadPulmInvestigationResult, long>>();
            repository.GetAll().Returns(GetData().BuildMock());

            var patient = Substitute.For<IRepository<Patient, long>>();
            patient.GetAll().Returns(GetPatients().BuildMock());

            var investigationRequests = Substitute.For<IRepository<InvestigationRequest, long>>();
            investigationRequests.GetAll().Returns(GetInvestigationRequest().BuildMock());
            var handler = new GetElectroRadPulmInvestigationResultQueryHandler(repository, patient, investigationRequests);

            var result = await handler.Handle(1, 1);

            result.PatientId.ShouldBe(1);
            result.InvestigationRequestId.ShouldBe(1);
        }

        private IQueryable<ElectroRadPulmInvestigationResult> GetData()
        {
            return new List<ElectroRadPulmInvestigationResult>()
            {
                new ElectroRadPulmInvestigationResult
                {
                    Id = 1,
                    PatientId = 1,
                    InvestigationRequestId = 1,
                    ResultImages = GetImages(),
                    Investigation = new Investigation
                    {
                        Id = 1,
                        Name = "Test",
                        Type = "Test"
                    }
                }
            }.AsQueryable();
        }

        private IQueryable<InvestigationRequest> GetInvestigationRequest()
        {
            return new List<InvestigationRequest>()
            {
                new InvestigationRequest
                {
                    Id = 1,
                    PatientId = 1,                    
                }
            }.AsQueryable();
        }

        private List<ElectroRadPulmInvestigationResultImages> GetImages()
        {
            return new List<ElectroRadPulmInvestigationResultImages>()
            {
                new ElectroRadPulmInvestigationResultImages
                {
                    Id = 1,
                    ElectroRadPulmInvestigationResultId= 1,
                    ImageUrl = "https://1",
                    FileId = "1",
                    FileName ="MeAndYou"
                }
            };
        }

        private IQueryable<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new Patient{ Id = 1}
            }.AsQueryable();
        }
    }
}

