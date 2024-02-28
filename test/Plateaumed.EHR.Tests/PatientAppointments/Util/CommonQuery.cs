using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.PatientAppointments.Query.BaseQueryHelper;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Tests.PatientAppointments.Util;

public static class CommonQuery
{
    public static AppointmentsBaseQuery GetAppointmentBaseQueryInstance()
    {
        var patientRepositoryMock = Substitute.For<IRepository<Patient, long>>();
        var patientAppointmentRepositoryMock = Substitute.For<IRepository<PatientAppointment, long>>();
        var patientCodeMappingRepositoryMock = Substitute.For<IRepository<PatientCodeMapping, long>>();
        var organizationUnitRepositoryMock = Substitute.For<IRepository<OrganizationUnitExtended, long>>();
        var staffMemberRepositoryMock = Substitute.For<IRepository<StaffMember, long>>();
        var patientReferralDocumentRepositoryMock = Substitute.For<IRepository<PatientReferralDocument, long>>();
        var userRepositoryMock = Substitute.For<IRepository<User, long>>();
        var patientScanDocumentRepositoryMock = Substitute.For<IRepository<PatientScanDocument, long>>();
        var rolesMock = Substitute.For<IRepository<Role>>();
        
        
        patientRepositoryMock.GetAll().Returns(GetPatientAsQueryable().BuildMock());
        patientAppointmentRepositoryMock.GetAll().Returns(GetPatientAppointmentAsQueryable().BuildMock());
        patientCodeMappingRepositoryMock.GetAll().Returns(GetPatientCodeMappingAsQueryable().BuildMock());
        organizationUnitRepositoryMock.GetAll().Returns(GetOrganizationUnitAsQueryable().BuildMock());
        staffMemberRepositoryMock.GetAll().Returns(GetStaffMemberAsQueryable().BuildMock());
        patientReferralDocumentRepositoryMock.GetAll().Returns(GetPatientReferralDocumentAsQueryable().BuildMock());
        userRepositoryMock.GetAll().Returns(GetUserAsQueryable().BuildMock());
        patientScanDocumentRepositoryMock.GetAll().Returns(GetPatientScanDocumentAsQueryable().BuildMock());
        rolesMock.GetAll().Returns(GetRoleAsQueryable().BuildMock());

        return new AppointmentsBaseQuery(patientAppointmentRepositoryMock, patientCodeMappingRepositoryMock,
            patientRepositoryMock, organizationUnitRepositoryMock, staffMemberRepositoryMock,
            patientReferralDocumentRepositoryMock, userRepositoryMock, patientScanDocumentRepositoryMock, rolesMock);
    }
    private static IQueryable<Role> GetRoleAsQueryable()
    {
        return new List<Role>()
        {
            new()
            {
                Id = 1,
                Name = "TestCode1",
                TenantId = 1,
                IsStatic = true,
                IsDefault = true,
                DisplayName = "TestCode1",
                IsDeleted = false

            }
        }.AsQueryable();
    }

    private static IQueryable<PatientScanDocument> GetPatientScanDocumentAsQueryable()
    {
        return new List<PatientScanDocument>
        {
            new()
            { 
                Id = 1, 
                TenantId   = 1,
                PatientCode = "TestCode1",
                IsApproved = true
             
            }
        }.AsQueryable();
    }

    private static  IQueryable<StaffMember> GetStaffMemberAsQueryable()
    {
        return new List<StaffMember>()
        {
            new()
            {
                Id = 1,
                StaffCode = "TestCode1",
                UserId = 1
                
            },
            new()
            {
                Id = 2,
                StaffCode = "TestCode2",
                UserId = 2
                
            },
        }.AsQueryable();
    }

    private static IQueryable<User> GetUserAsQueryable()
    {
        return new List<User>()
        {
            new()
            {
                Id = 1,
                UserName = "TestCode1",
                TenantId = 1,
                Name = "TestName1",
                Surname = "TestSurname1",
                MiddleName = "TestMiddleName1",
                Title = TitleType.Dr,
                Roles = new List<UserRole>
                {
                    new()
                    {
                        TenantId = 1,
                        UserId = 1,
                        RoleId = 1
                    }
                }
                
                
            },
            new()
            {
                Id = 2,
                UserName = "TestCode2",
                TenantId = 1,
                Name = "TestName2",
                Surname = "TestSurname2",
                MiddleName = "TestMiddleName2",
                Title = TitleType.Dr,
                
                
            },
        }.AsQueryable();
    }

    private static IQueryable<OrganizationUnitExtended> GetOrganizationUnitAsQueryable()
    {
        return new List<OrganizationUnitExtended>()
        {
            new()
            {
                TenantId = 1,
                Id = 1,
                Code = "TestCode1",
                DisplayName = "Clinic1",
                Type = OrganizationUnitType.Clinic,
                FacilityId = 1
            }
        }.AsQueryable();
    }

    private static IQueryable<PatientCodeMapping> GetPatientCodeMappingAsQueryable()
    {
        return new List<PatientCodeMapping>()
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                PatientCode = "TestCode1",
                FacilityId = 1
            },
            new()
            {
                Id = 2,
                PatientId = 2,
                PatientCode = "TestCode2",
                FacilityId = 1
            },
            new()
            {
                Id = 3,
                PatientId = 3,
                PatientCode = "TestCode3",
                FacilityId = 1
            }
        }.AsQueryable();
    }

    private static IQueryable<PatientAppointment> GetPatientAppointmentAsQueryable()
    {
        return new List<PatientAppointment>
        {
            new()
            {
                TenantId = 1,
                Id = 1,
                PatientId = 1,
                Duration = 10,
                StartTime = DateTime.Now.Date.AddMinutes(12),
                Status = AppointmentStatusType.Not_Arrived,
                Type = AppointmentType.Walk_In,
                AttendingClinicId = 1,
                AttendingPhysicianId = 1,
                ReferringClinicId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes",
                Title = "Test Title",
                IsRepeat = true,
                RepeatType = AppointmentRepeatType.Daily,
                
            },
            new()
            {
                TenantId = 1,
                Id = 3,
                PatientId = 3,
                Duration = 10,
                StartTime = DateTime.Today.Date.AddDays(3),
                Status = AppointmentStatusType.Not_Arrived,
                Type = AppointmentType.Follow_Up,
                AttendingClinicId = 1,
                ReferringClinicId = 1,
                AttendingPhysicianId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes2",
                Title = "Test Title2",
                IsRepeat = false,

            },
            new()
            {
                TenantId = 1,
                Id = 2,
                PatientId = 2,
                Duration = 10,
                StartTime = DateTime.Now.Date.AddMinutes(10),
                Status = AppointmentStatusType.Not_Arrived,
                Type = AppointmentType.Walk_In,
                AttendingClinicId = 1,
                AttendingPhysicianId = 1,
                ReferringClinicId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes",
                Title = "Test Title",
                IsRepeat = true,
                RepeatType = AppointmentRepeatType.Daily,
                
            },
            new()
            {
                TenantId = 1,
                Id = 4,
                PatientId = 1,
                Duration = 10,
                StartTime = DateTime.Today.Date.AddDays(10),
                Status = AppointmentStatusType.Not_Arrived,
                Type = AppointmentType.Walk_In,
                AttendingClinicId = 1,
                AttendingPhysicianId = 2,
                ReferringClinicId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes 4",
                Title = "Test Title 4",
                IsRepeat = true,
                RepeatType = AppointmentRepeatType.Daily,
                
            },
            new()
            {
                TenantId = 1,
                Id = 5,
                PatientId = 1,
                Duration = 10,
                StartTime = DateTime.Today.Date.AddDays(3),
                Status = AppointmentStatusType.Not_Arrived,
                Type = AppointmentType.Walk_In,
                AttendingClinicId = 1,
                AttendingPhysicianId = 2,
                ReferringClinicId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes 5",
                Title = "Test Title 5",
                IsRepeat = true,
                RepeatType = AppointmentRepeatType.Daily,
                
            },
            
            new()
            {
                TenantId = 1,
                Id = 6,
                PatientId = 1,
                Duration = 10,
                StartTime = DateTime.Today.Date.AddDays(-2),
                Status = AppointmentStatusType.Not_Arrived,
                Type = AppointmentType.Walk_In,
                AttendingClinicId = 1,
                AttendingPhysicianId = 2,
                ReferringClinicId = 1,
                PatientReferralDocumentId = 1,
                Notes = "Test Notes 5",
                Title = "Test Title 5",
                IsRepeat = true,
                RepeatType = AppointmentRepeatType.Daily,
                
            },
        }.AsQueryable();
    }

    private static IQueryable<Patient> GetPatientAsQueryable()
    {
        return new List<Patient>
        {
            new()
            {
                FirstName = "Test1",
                LastName = "Patient1",
                Id = 1,
                EmailAddress = "example@test.com",
                PhoneNumber = "1234567890",
                
                
            }, new()
            {
                FirstName = "Test2",
                LastName = "Patient2",
                Id = 2,
                EmailAddress = "test@test.com",
                PhoneNumber = "1234567890",
                
            },
            new()
            {
                FirstName = "Test3",
                LastName = "Patient3",
                Id = 3,
                EmailAddress = "test2@me.com",
                PhoneNumber = "1234567890",
            }
        }.AsQueryable();
    }

    private static IQueryable<PatientReferralDocument> GetPatientReferralDocumentAsQueryable()
    {
        return new List<PatientReferralDocument>()
        {
            new()
            {
                Id = 1,
                PatientId = 1,
                ReferralDocument = Guid.NewGuid(),
                DiagnosisSummary = "Test Diagnosis Summary",
                ReferringHealthCareProvider = "Test Referring Health Care Provider",
            },
            new()
            {
                Id = 2,
                PatientId = 2,
                ReferralDocument = Guid.NewGuid(),
                DiagnosisSummary = "Test Diagnosis Summary",
                ReferringHealthCareProvider = "Test Referring Health Care Provider",
            },
        }.AsQueryable();
    }
}
