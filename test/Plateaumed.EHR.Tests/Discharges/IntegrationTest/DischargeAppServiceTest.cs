using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Discharges;
using Plateaumed.EHR.DoctorDischarge;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Discharges.IntegrationTest
{
    [Trait("Category", "Integration")]
    public class DischargeAppServiceTest : AppTestBase
    {
        private readonly IDischargeAppService _dischargeAppService;
        public DischargeAppServiceTest()
        {
            _dischargeAppService = Resolve<IDischargeAppService>();
        }

        [Fact]
        public async Task DischargeAppService_Create_Normal_Discharge_Should_Return_Success()
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
                var appointment = GetPatientAppointment(patient);
                context.PatientAppointments.Add(appointment);
                var encounter = context.PatientEncounters.Add(new PatientEncounter
                {
                    PatientId = patient.Id
                }).Entity;

                var medications = GetPatientMedications(patient);
                context.Medications.Add(medications);
                var planItems = GetPlanItems(patient);
                context.PlanItems.Add(planItems);


                context.SaveChanges();
                encounterId = encounter.Id;
            });

            var command = Util.Common.GetNormalDischargeRequest(patientId);
            command.EncounterId = encounterId;
            //act
            var discharge = await _dischargeAppService.CreateOrEditNormalDischarge(command);
            var dischargeId = discharge != null ? discharge.Id : 0;
            //assert
            dischargeId.ShouldBeGreaterThan(0);

        }

        private static PatientAppointment GetPatientAppointment(Patient patient)
        {
            return new PatientAppointment()
            {
                Type = AppointmentType.Walk_In,
                Title = "Walk In",
                PatientFk = patient
            };
        }

        private static AllInputs.PlanItems GetPlanItems(Patient patient)
        {
            return new AllInputs.PlanItems
            {
                Stamp = 1,
                PatientId = patient.Id,
                Patient = patient,
                PlanItemsSnowmedIds = new List<string>(),
                Description = "Sample Description"
            };
        }
        private static AllInputs.Medication GetPatientMedications(Patient patient)
        {
            return new AllInputs.Medication
            {
                //TenantId = 1,
                PatientId = patient.Id,
                Patient = patient,
                ProductId = 1,
                ProductName = "Sample Product",
                ProductSource = "Souce",
                DoseUnit = "400MG",
                Frequency = "Daily",
                Duration = "3 days",
                Direction = "2X Daily",
                Note = "Sample note"
            };
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