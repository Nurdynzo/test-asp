using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.IntakeOutputs;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.IntakeOuputs.IntegrationTest
{
    [Trait("Category", "Integration")]
    public class IntakeOutputAppServiceTest : AppTestBase
    {
        private readonly IIntakeOutputAppService _intakeOutputAppService;
        public IntakeOutputAppServiceTest()
        {
            _intakeOutputAppService = Resolve<IIntakeOutputAppService>();
        }

        [Fact]
        public async Task IntakeOutputAppService_Create_Normal_Intake_Should_Return_Success()
        {
            //arrange
            LoginAsDefaultTenantAdmin();
            long patientId = 0;
            long encounterId = 0;
            UsingDbContext(context =>
            {
                var patient = GetPatient();
                context.Patients.Add(patient);
                context.SaveChanges();
                patientId = patient.Id;
                var encounter = context.PatientEncounters.Add(new PatientEncounter{PatientId = patientId}).Entity;

                var medications = GetPatientMedications(patient);
                context.Medications.AddRange(medications);
                context.SaveChanges();
                encounterId = encounter.Id;
            });

            var command = Util.Common.GetCreateIntakeOutputRequest(patientId).
                FirstOrDefault(s => s.Type == ChartingType.INTAKE && s.Id==0);
            command.EncounterId = encounterId;

            //act
            var intakeResult = await _intakeOutputAppService.CreateOrEditIntake(command);
            var intakeId = intakeResult != null ? intakeResult.Id : 0;
            //assert
            intakeId.ShouldBeGreaterThan(0);

        }

        [Fact]
        public async Task IntakeOutputAppService_Create_Normal_Output_Should_Return_Success()
        {
            //arrange
            LoginAsDefaultTenantAdmin();
            long patientId = 0;
            long encounterId = 0;
            UsingDbContext(context =>
            {
                var patient = GetPatient();
                context.Patients.Add(patient);
                context.SaveChanges();
                patientId = patient.Id;
                var encounter = context.PatientEncounters.Add(new PatientEncounter { PatientId = patientId }).Entity;

                var medications = GetPatientMedications(patient);
                context.Medications.AddRange(medications);
                context.SaveChanges();
                encounterId = encounter.Id;
            });

            var command = Util.Common.GetCreateIntakeOutputRequest(patientId)
                .FirstOrDefault(s => s.Type == ChartingType.OUTPUT && s.Id == 0);
            command.EncounterId = encounterId;

            //act
            var intakeResult = await _intakeOutputAppService.CreateOrEditIntake(command);
            var intakeId = intakeResult != null ? intakeResult.Id : 0;
            //assert
            intakeId.ShouldBeGreaterThan(0);

        }
        [Fact]
        public async Task IntakeOutputAppService_Delete_Normal_IntakeOutput_Should_Return_Success()
        {
            //arrange
            LoginAsDefaultTenantAdmin();
            long patientId = 0;
            UsingDbContext(context =>
            {
                var patient = GetPatient();
                context.Patients.Add(patient);
                context.SaveChanges();
                patientId = patient.Id;

                var medications = GetPatientMedications(patient);
                context.Medications.AddRange(medications);


                var intakeOutput = GetPatientIntakeOutputCharting(patient);
                context.IntakeOutputChartings.AddRange(intakeOutput);
                context.SaveChanges();
            });

            //act
            var intakeResult = await _intakeOutputAppService.DeleteIntakeOrOutput(1);
            //assert
            intakeResult.ShouldBe(true);
        }
       
        private static List<AllInputs.IntakeOutputCharting> GetPatientIntakeOutputCharting(Patient patient)
        {
            var med = new List<AllInputs.IntakeOutputCharting>();
            med.Add(new AllInputs.IntakeOutputCharting
            {
                Id = 1,
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                SuggestedText = "Antibiotics",
                VolumnInMls = 33432,
                Type = IntakeOutputs.ChartingType.INTAKE
            });
            med.Add(new AllInputs.IntakeOutputCharting
            {
                Id = 2,
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                SuggestedText = "Urine",
                VolumnInMls = 6432,
                Type = IntakeOutputs.ChartingType.OUTPUT
            });

            return med;
        }
        private static List<AllInputs.Medication> GetPatientMedications(Patient patient)
        {
            var med = new List<AllInputs.Medication>();
            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Antibiotics",
                ProductSource = "Souce",
                DoseUnit = "infusion",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "2X Daily",
                Note = "Antibiotics infusion"
            });
            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Antifungal",
                ProductSource = "Souce",
                DoseUnit = "infusion",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "2X Daily",
                Note = "Antifungal infusion"
            });
            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Insulin",
                ProductSource = "Souce",
                DoseUnit = "infusion",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "2X Daily",
                Note = "Insulin infusion"
            }); 
            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Blood",
                ProductSource = "Souce",
                DoseUnit = "infusion",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "2X Daily",
                Note = "Blood infusion"
            });

            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Capsules",
                ProductSource = "Medication",
                DoseUnit = "400MG",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "2X Daily",
                Note = "Medications"
            });
            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Anitmalaria",
                ProductSource = "Medication",
                DoseUnit = "400MG",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "1X Daily",
                Note = "Tablet"
            });
            med.Add(new AllInputs.Medication
            {
                TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Antibiotic Tablet",
                ProductSource = "Medication",
                DoseUnit = "500MG",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "1X Daily",
                Note = "Tablet"
            });

            
            return med;
        }
        private static Patient GetPatient()
        {
            return new Patient
            {
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test@user.com",
                PhoneNumber = "1234567890",
                GenderType = GenderType.Female,
                DateOfBirth = DateTime.Now.AddYears(-40),
                UuId = Guid.NewGuid()
            };
        }
    }
}