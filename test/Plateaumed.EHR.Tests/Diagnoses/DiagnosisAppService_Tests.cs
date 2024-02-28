using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Diagnoses
{
    public class DiagnosisAppService_Tests : AppTestBase
    {
        private readonly IDiagnosisAppService _diagnosisAppService;

        public DiagnosisAppService_Tests()
        {
            LoginAsDefaultTenantAdmin();
            _diagnosisAppService = Resolve<IDiagnosisAppService>();
        }

        [Fact]
        public async Task Create_GivenValidDiagnosis_ShouldSave()
        {
            // Arrange
            const int tenantId = 1;
            
            var (patient, encounter) = UsingDbContext(context =>
            {
                var patient = context.Patients.Add(GetPatient()).Entity;
                var encounter = context.PatientEncounters.Add(new PatientEncounter{Patient = patient}).Entity;
                return (patient, encounter);
            });

            var diagnosis = new TestDiagnosisBuilder(tenantId,  patient.Id)
                .WithNote("Test Note")
                .Build();

            var diagnosisDto = new CreateDiagnosisDto
            {
                PatientId = diagnosis.PatientId,
                Notes = diagnosis.Notes,
                Sctid = diagnosis.Sctid,
                SelectedDiagnoses = new List<DiagnosisItemDto>
                {
                    new DiagnosisItemDto { Name = "Malaria", Type = DiagnosisType.Clinical },
                    new DiagnosisItemDto { Name = "Epilepsy", Type = DiagnosisType.Differential },
                },
                EncounterId = encounter.Id
            };


            // Act
            await _diagnosisAppService.CreateDiagnosis(diagnosisDto);

            // Assert
            var createdDiagnosis = await UsingDbContextAsync(async context =>
            {
                return await context.Diagnosis.FirstOrDefaultAsync(d => d.PatientId == patient.Id);
            });

            createdDiagnosis.EncounterId.ShouldBe(encounter.Id);
        }

        private Patient GetPatient()
        {
            return new Patient
            {
                FirstName = "John",
                LastName = "Joe",
                PhoneNumber = "08060421709",
                EmailAddress = "test@example.com",
                DateOfBirth = DateTime.Now,
                GenderType = GenderType.Male,
                Title = TitleType.Mr
            };
        }
    }
}
