using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Patients.Query;
using Plateaumed.EHR.Staff;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.Patients.UnitTest
{
    [Trait("Category","Unit")]
    public class GetViewAllPatientInClinicQueryHandlerTest
    {
        private readonly IRepository<Patient, long> _patientRepositoryMock;
        private readonly IRepository<OrganizationUnitExtended, long> _organizationUnitRepositoryMock;
        private readonly IRepository<Invoice, long> _invoiceRepositoryMock;
        private readonly IRepository<PatientAppointment,long> _patientAppointmentRepositoryMock;
        private readonly IRepository<StaffMember,long> _staffMemberRepositoryMock;
        private readonly IRepository<User, long> _userRepositoryMock;
        private readonly IRepository<PatientEncounter,long> _patientEncounterRepositoryMock;

        private readonly IGetViewAllPatientInClinicQueryHandler _handler;
        public GetViewAllPatientInClinicQueryHandlerTest()
        {
            _patientRepositoryMock = Substitute.For<IRepository<Patient, long>>();
            _invoiceRepositoryMock = Substitute.For<IRepository<Invoice, long>>();
            _organizationUnitRepositoryMock = Substitute.For<IRepository<OrganizationUnitExtended, long>>();
            _patientAppointmentRepositoryMock = Substitute.For<IRepository<PatientAppointment, long>>();
            _staffMemberRepositoryMock = Substitute.For<IRepository<StaffMember, long>>();
            _patientEncounterRepositoryMock = Substitute.For<IRepository<PatientEncounter, long>>();

            _userRepositoryMock = Substitute.For<IRepository<User, long>>();
            _handler = new GetViewAllPatientInClinicQueryHandler(
                _patientRepositoryMock,
                _organizationUnitRepositoryMock,
                _invoiceRepositoryMock,
                _patientAppointmentRepositoryMock,
                _staffMemberRepositoryMock,
                _userRepositoryMock,
                _patientEncounterRepositoryMock);

        }
        [Fact]
        public async Task Handle_With_Default_Parameter_Return_Patient_For_Day()
        {
            //arrange
            MockData();
            var request = new AllPatientInClinicRequest
            {
                SearchText = null,
                SortFilter = null
            };
            // act
            var result = await _handler.Handle(request, 1);
            // assert
            result.Count.ShouldBe(2);
            result[0].FullName.ShouldBe("Akash Adam");
            result[0].AppointmentStatus.ShouldBe(AppointmentStatusType.Seen_Doctor);
            result[0].PaymentStatus.ShouldBe(PaymentStatus.Unpaid);
            result[0].AssignedDoctor.ShouldBe("Dr Test Test2");
            result[0].Clinic.ShouldBe("Test1");
            result[0].AppointmentId.ShouldBe(2);
            result[1].EncounterId.ShouldBe(1);
            result[1].PatientId.ShouldBe(1);
            
        }
        [Fact]
        public async Task Handle_With_Wrong_FacilityId_Return_Empty()
        {
            //arrange
            MockData();
            var request = new AllPatientInClinicRequest();
            // act
            var result = await _handler.Handle(request, 0);
            // assert
            result.Count.ShouldBe(0);

        }
        [Fact]
        public async Task Handle_With_Sort_Appointment_Status_Parameter_Should_Sort_By_Status()
        {
            //arrange
            MockData();
            var request = new AllPatientInClinicRequest
            {
                SortFilter = ViewAllPatientInClinicSortFilter.AppointmentStatus
            };
            // act
            var result = await _handler.Handle(request, 1);
            // assert
            result.Count.ShouldBe(2);
            result[0].AppointmentStatus.ShouldBe(AppointmentStatusType.Awaiting_Vitals);
            result[1].AppointmentStatus.ShouldBe(AppointmentStatusType.Seen_Doctor);
        }
        [Fact]
        public async Task Handle_With_Sort_Payment_Status_Parameter_Should_Sort_By_Status()
        {
            //arrange
            MockData();
            var request = new AllPatientInClinicRequest
            {
                SortFilter = ViewAllPatientInClinicSortFilter.PaymentStatus
            };
            // act
            var result = await _handler.Handle(request, 1);
            // assert
            result.Count.ShouldBe(2);
            result[0].PaymentStatus.ShouldBe(PaymentStatus.Paid);
            result[1].PaymentStatus.ShouldBe(PaymentStatus.Unpaid);
        }
        [Fact]
        public async Task Handle_With_Sort_By_Patient_Name_Should_Sort_By_Patient_Name()
        {
            //arrange
            MockData();
            var request = new AllPatientInClinicRequest
            {
                SortFilter = ViewAllPatientInClinicSortFilter.Patient
            };
            // act
            var result = await _handler.Handle(request, 1);
            // assert
            result.Count.ShouldBe(2);
            result[0].FullName.ShouldBe("Akash Adam");
            result[1].FullName.ShouldBe("Test1 Patient1");
        }
        [Fact]
        public async Task Handle_With_Search_Parameter_Should_Match_Data()
        {
            //arrange
            MockData();
            var request = new AllPatientInClinicRequest
            {
                SearchText = "Akash"
            };
            // act
            var result = await _handler.Handle(request, 1);
            // assert
            result.Count.ShouldBe(1);
            result[0].FullName.ShouldBe("Akash Adam");
        }


        private void MockData()
        {

            _patientRepositoryMock.GetAll().Returns(GetPatientAsQueryable().BuildMock());
            _organizationUnitRepositoryMock.GetAll().Returns(GetOrganizationUnitAsQueryable().BuildMock());
            _patientAppointmentRepositoryMock.GetAll().Returns(GetPatientAppointmentAsQueryable().BuildMock());
            _invoiceRepositoryMock.GetAll().Returns(GetInvoiceAsQueryable().BuildMock());
            _staffMemberRepositoryMock.GetAll().Returns(GetStaffMemberAsQueryable().BuildMock());
            _userRepositoryMock.GetAll().Returns(GetUserAsQueryable().BuildMock());
            _patientEncounterRepositoryMock.GetAll().Returns(GetPatientEncounterAsQueryable().BuildMock());
        }
        private IQueryable<PatientEncounter> GetPatientEncounterAsQueryable()
        {
            return new List<PatientEncounter>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    AppointmentId = 1,
                    
                },

            }.AsQueryable();
        }
        private IQueryable<User> GetUserAsQueryable()
        {
            return new List<User>()
            {
                new ()
                {
                    Id = 1,
                    UserName = "Test",
                    EmailAddress = "example@test",
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test",
                    Title = TitleType.Dr,
                    Gender = GenderType.Male,
                    Surname = "Test2",
                }
            }.AsQueryable();
        }
        private IQueryable<StaffMember> GetStaffMemberAsQueryable()
        {
            return new List<StaffMember>()
            {
                new()
                {
                    Id = 1,
                    UserId = 1,
                }
            }.AsQueryable();
        }
        private IQueryable<Invoice> GetInvoiceAsQueryable()
        {
            return new List<Invoice>()
            {
                new ()
                {
                    Id = 1,
                    PatientId = 1,
                    FacilityId = 1,
                    TenantId = 1,
                    InvoiceId = "0001",
                    InvoiceType = InvoiceType.InvoiceCreation,
                    PaymentStatus = PaymentStatus.Paid,
                },
                new ()
                {
                    Id = 2,
                    PatientId = 2,
                    FacilityId = 1,
                    TenantId = 1,
                    InvoiceId = "0002",
                    InvoiceType = InvoiceType.InvoiceCreation,
                    PaymentStatus = PaymentStatus.Unpaid,

                }
            }.AsQueryable();
        }
        private IQueryable<PatientAppointment> GetPatientAppointmentAsQueryable()
        {
            return new List<PatientAppointment>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    StartTime = DateTime.Now,
                    Type = AppointmentType.Walk_In,
                    Title = "test",
                    Status = AppointmentStatusType.Awaiting_Vitals,
                    TenantId = 1,
                    AttendingClinicId = 1,
                    AttendingPhysicianId = 1,
                    CreationTime = DateTime.Now,
                },
                new()
                {
                    Id = 2,
                    PatientId = 2,
                    StartTime = DateTime.Now,
                    Type = AppointmentType.Walk_In,
                    Title = "test 2",
                    Status = AppointmentStatusType.Seen_Doctor,
                    TenantId = 1,
                    AttendingClinicId = 1,
                    AttendingPhysicianId = 1,
                    CreationTime = DateTime.Now.AddMinutes(10)

                }
            }.AsQueryable();
        }
        private IQueryable<OrganizationUnitExtended> GetOrganizationUnitAsQueryable()
        {
            return new List<OrganizationUnitExtended>()
            {
                new ()
                {
                    Id = 1,
                    DisplayName = "Test1",
                    ShortName = "Test1",
                    IsActive = true,
                    FacilityId = 1,
                    Type = OrganizationUnitType.Clinic,
                    ParentId = 1,


                }
            }.AsQueryable();
        }
        private IQueryable<Patient> GetPatientAsQueryable()
        {
            return new List<Patient>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Test1",
                        LastName = "Patient1",
                        EmailAddress = "example@test.com",
                        PhoneNumber = "1234567890",
                        NoOfFemaleChildren = 2,
                        NoOfMaleChildren = 3,
                        Address = "Test Address",
                        GenderType = GenderType.Male,
                        MaritalStatus = MaritalStatus.Married

                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Akash",
                        LastName = "Adam",
                        EmailAddress = "example@test.com",
                        PhoneNumber = "1234567890",
                        NoOfFemaleChildren = 2,
                        NoOfMaleChildren = 3,
                        Address = "Test Address",
                        GenderType = GenderType.Male,
                        MaritalStatus = MaritalStatus.Married

                    }
                }
                .AsQueryable();
        }
    }
}
