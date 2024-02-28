using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Shouldly;
using System;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using Abp.UI;
using Plateaumed.EHR.Patients.Dtos;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Test.Base.TestData;
using Xunit;

namespace Plateaumed.EHR.Tests.Patients;

public class PatientServiceTest : AppTestBase
{
    private readonly IPatientsAppService _patientsAppService;

    public PatientServiceTest()
    {
        _patientsAppService = Resolve<IPatientsAppService>();
    }

    [MultiTenantFact]
    public async Task Valid_Input_Should_Return_Success_Patient_Record()
    {
        //Arrange

        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto();

        //Act
        var createdPatient = await _patientsAppService.CreateOrEdit(patient);

        //Assert
        createdPatient.ShouldNotBeNull();

        createdPatient.Id.GetValueOrDefault().ShouldBeGreaterThan(0);
        createdPatient.FirstName.ShouldBe("John");
        createdPatient.LastName.ShouldBe("Joe");
        createdPatient.EmailAddress.ShouldBe("test@example.com");
        createdPatient.PhoneNumber.ShouldBe("1234567890");
        createdPatient.GenderType.ShouldBe(GenderType.Female);
        createdPatient.DateOfBirth.ShouldBeLessThanOrEqualTo(DateTime.Now);
    }
    
    [MultiTenantFact]
    public async Task Invalid_Patient_Code_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(patientCode: null);

        //Act && Assert
        await Should.ThrowAsync<AbpValidationException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }
    
    [MultiTenantFact]
    public async Task Invalid_PhoneNumber_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(phoneNumber: null);

        //Act && Assert
        await Should.ThrowAsync<AbpValidationException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }

    [MultiTenantFact]
    public async Task Invalid_FirstName_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(firstName: null);

        //Act && Assert
        await Should.ThrowAsync<AbpValidationException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }

    [MultiTenantFact]
    public async Task Invalid_LastName_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(lastName: null);

        //Act && Assert
        await Should.ThrowAsync<AbpValidationException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }

    [MultiTenantFact]
    public async Task Wrong_PhoneNumber_Format_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(phoneNumber: "twat0012test");

        //Act && Assert
        await Should.ThrowAsync<AbpValidationException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }

    [MultiTenantFact]
    public async Task Wrong_Email_Format_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(email: "email.com");

        //Act && Assert
        await Should.ThrowAsync<AbpValidationException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }

    [MultiTenantFact]
    public async Task Wrong_DateOfBirth_Should_Throw_Exception()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        var patient = GetCreateOrEditPatientDto(dateOfBirth: DateTime.Now.AddYears(1));

        //Act && Assert
        await Should.ThrowAsync<UserFriendlyException>(async () => await _patientsAppService.CreateOrEdit(patient));
    }

    [MultiTenantFact]
    public async Task CheckPatientExist_Should_Return_Patient_When_Patient_Exist()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();

        var input = new CheckPatientExistInput
        {
            GenderType = GenderType.Male,
            PhoneNumber = "08060421709"
        };

        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(firstName: "Tobi", phoneNumber: "08060421709",
            genderType: GenderType.Male));

        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(firstName: "Tolu",
            patientCode: "AAA-MMM-1234YUY", phoneNumber: "08060421701", genderType: GenderType.Female));

        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(firstName: "Tunde",
            patientCode: "BBB-YYY-2389823", phoneNumber: "08060421709", genderType: GenderType.Male));


        // Act
        var patients = await _patientsAppService.CheckPatientExist(input);


        // Assert
        patients.Count.ShouldBe(2);
    }


    [MultiTenantFact]
    public async Task CheckPatientExist_Should_Return_Correct_Patient_Data_When_Patient_Exist()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();

        var input = new CheckPatientExistInput
        {
            GenderType = GenderType.Male,
            PhoneNumber = "08060421709"
        };

        var patient1 = await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(firstName: "Tobi",
            phoneNumber: "08060421709", genderType: GenderType.Male));

        var patient2 = await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(firstName: "Tolu",
            patientCode: "AAA-MMM-1234YUY", phoneNumber: "08060421701", genderType: GenderType.Female));

        // Act
        var patients = await _patientsAppService.CheckPatientExist(input);

        var patient = patients.FirstOrDefault();


        // Assert
        patient.FirstName.ShouldBe(patient1.FirstName);
        patient.LastName.ShouldBe(patient1.LastName);
        patient.PatientCode.ShouldBe(patient1.PatientCode);
        patient.PhoneNumber.ShouldBe(patient1.PhoneNumber);
        patient.EmailAddress.ShouldBe(patient1.EmailAddress);
        patient.DateOfBirth.ShouldBe(patient1.DateOfBirth);
    }
   

    [MultiTenantFact]
    public async Task CheckPatientExist_Should_Return_Empty_List_When_Patient_Does_Not_Exist()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();

        var input = new CheckPatientExistInput
        {
            GenderType = GenderType.Male,
            PhoneNumber = "08060421709"
        };


        // Act
        var patients = await _patientsAppService.CheckPatientExist(input);


        // Assert
        patients.Count.ShouldBe(0);
    }

    private static CreateOrEditPatientDto GetCreateOrEditPatientDto(
        string firstName = "John",
        string lastName = "Joe",
        string email = "test@example.com",
        string phoneNumber = "1234567890",
        GenderType genderType = GenderType.Female,
        DateTime dateOfBirth = default(DateTime),
        string patientCode = "ZZZ-1091012"
    )
    {
        var patient = new CreateOrEditPatientDto
        {
            FirstName = firstName,
            LastName = lastName,
            EmailAddress = email,
            PhoneNumber = phoneNumber,
            GenderType = genderType,
            DateOfBirth = dateOfBirth == default ? DateTime.Now.AddYears(-40) : dateOfBirth,
            PatientCode = patientCode
        };
        return patient;
    }

    [MultiTenantFact]
    public async Task Generate_Patient_Code_Should_Return_Valid_Patient_Code()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();
        UsingDbContext(context =>
        {
            var patientCodeTemplate = new PatientCodeTemplate
            {
                Prefix = "PREFIX",
                Suffix = "SUFFIX",
                Length = 10
            };
            var facility = SetDefaultFacilityPatientCode(context, patientCodeTemplate);
            AssignDefaultStaffMemberToFacility(context, facility.Id, true);
        });

        //act
        var patientCode = await _patientsAppService.GetNewPatientCode();

        //assert
        patientCode.ShouldBe("PREFIX-0000000001-SUFFIX");
    }

    [MultiTenantFact]
    public async Task Generate_Patient_Code_Should_Increase_Serial_Code_For_Each()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();

        UsingDbContext(context =>
        {
            var patientCodeTemplate = new PatientCodeTemplate
            {
                Prefix = "PREFIX",
                Length = 10
            };
            var facility = SetDefaultFacilityPatientCode(context, patientCodeTemplate);
            AssignDefaultStaffMemberToFacility(context, facility.Id, true);
        });

        //act
        var patientCode = await _patientsAppService.GetNewPatientCode();
        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(patientCode: patientCode));
        var patientCode2 = await _patientsAppService.GetNewPatientCode();
        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(patientCode: patientCode2));
        var patientCode3 = await _patientsAppService.GetNewPatientCode();

        //assert
        patientCode.ShouldBe("PREFIX-0000000001");
        patientCode2.ShouldBe("PREFIX-0000000002");
        patientCode3.ShouldBe("PREFIX-0000000003");
    }

    [MultiTenantFact]
    public async Task Should_Generate_Code_If_Prefix_and_Suffix_Is_Missing()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();
        UsingDbContext(context =>
        {
            var patientCodeTemplate = new PatientCodeTemplate()
            {
                Length = 10
            };
            var facility = SetDefaultFacilityPatientCode(context, patientCodeTemplate);
            AssignDefaultStaffMemberToFacility(context, facility.Id, true);
        });

        //act
        var patientCode = await _patientsAppService.GetNewPatientCode();
        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(patientCode: patientCode));
        var patientCode2 = await _patientsAppService.GetNewPatientCode();
        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(patientCode: patientCode2));
        var patientCode3 = await _patientsAppService.GetNewPatientCode();

        //assert
        patientCode.ShouldBe("0000000001");
        patientCode2.ShouldBe("0000000002");
        patientCode3.ShouldBe("0000000003");
    }
    
    [MultiTenantFact]
    public async Task Should_Re_Use_Generated_Code_If_Not_Used_In_The_Facility()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();
        UsingDbContext(context =>
        {
            var patientCodeTemplate = new PatientCodeTemplate
            {
                Length = 10
            };
            var facility = SetDefaultFacilityPatientCode(context, patientCodeTemplate);
            AssignDefaultStaffMemberToFacility(context, facility.Id, true);
        });

        //act
        var patientCode = await _patientsAppService.GetNewPatientCode();
        
        var patientCode2 = await _patientsAppService.GetNewPatientCode();
        
        var patientCode3 = await _patientsAppService.GetNewPatientCode();

        //assert
        patientCode.ShouldBe("0000000001");
        patientCode2.ShouldBe("0000000001");
        patientCode3.ShouldBe("0000000001");
        
    }
    
    [MultiTenantFact]
    public async Task Should_Generated_New_Code_If_PatientCode_Is_In_Use_In_The_Facility()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();
        UsingDbContext(context =>
        {
            var patientCodeTemplate = new PatientCodeTemplate
            {
                Length = 10,
                Prefix = "PREFIX"
            };
            var facility = SetDefaultFacilityPatientCode(context, patientCodeTemplate);
            AssignDefaultStaffMemberToFacility(context, facility.Id, true);
        });

        //act
        var patientCode = await _patientsAppService.GetNewPatientCode();
        var patient = GetCreateOrEditPatientDto(patientCode: patientCode);
        await _patientsAppService.CreateOrEdit(patient);
        
        var patientCode2 = await _patientsAppService.GetNewPatientCode();
        
        
        

        //assert
        patientCode.ShouldBe("PREFIX-0000000001");
        patientCode2.ShouldBe("PREFIX-0000000002");
        
        
    }

    [MultiTenantFact]
    public async Task Should_Ignore_Zero_Padding_If_Code_Is_Up_ToLength()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();
        UsingDbContext(context =>
        {
            var patientCodeTemplate = new PatientCodeTemplate()
            {
                Length = 1
            };
            var facility = SetDefaultFacilityPatientCode(context, patientCodeTemplate);
            AssignDefaultStaffMemberToFacility(context, facility.Id, true);
        });

        //act
        var patientCode = await _patientsAppService.GetNewPatientCode();
        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(patientCode: patientCode));
        var patientCode2 = await _patientsAppService.GetNewPatientCode();
        await _patientsAppService.CreateOrEdit(GetCreateOrEditPatientDto(patientCode: patientCode2));
        var patientCode3 = await _patientsAppService.GetNewPatientCode();

        //assert
        patientCode.ShouldBe("1");
        patientCode2.ShouldBe("2");
        patientCode3.ShouldBe("3");
    }

    [MultiTenantFact]
    public async Task SearchPatient_Should_Return_Patient_When_Patient_Exist()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "Jeremy", phoneNumber: "08060421709", genderType: GenderType.Male,
                patientCode: "AAA-000-0000"));
        await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "San", phoneNumber: "08060421709", genderType: GenderType.Male,
                patientCode: "BBB-000-0000"));
        await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "Tobi", phoneNumber: "08060421709", genderType: GenderType.Male,
                patientCode: "CCC-000-0000"));
        var searchText = "08060421709";


        // Act
        var results = await _patientsAppService.SearchPatient(searchText);

        // Assert
        results.Count.ShouldBe(3);
    }

    [MultiTenantFact]
    public async Task SearchPatient_Should_Match_Patient_When_Patient_Exist()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "Johnson", phoneNumber: "08060421709", genderType: GenderType.Male,
                patientCode: "AAA-000-0000"));
        await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "John", phoneNumber: "08060421709", genderType: GenderType.Male,
                patientCode: "BBB-000-0000"));
        await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "Tobi", phoneNumber: "08060421709", genderType: GenderType.Male,
                patientCode: "CCC-000-0000"));
        var searchText = "John";


        // Act
        var results = await _patientsAppService.SearchPatient(searchText);

        // Assert
        results.Count.ShouldBe(2);
    }

    [Fact]
    public async Task GetPatientDetails_GivenValidInput_ShouldReturnLastSeenBy()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        var tenantId = AbpSession.TenantId.Value;
        var (patient, encounter, doctorUser, nurseUser) = UsingDbContext(context =>
        {
            var patient = context.Patients.Add(GetPatient()).Entity;
            var encounter1 = new PatientEncounter{Patient = patient};
            var encounter2= new PatientEncounter{Patient = patient};
            context.PatientEncounters.Add(encounter1);
            context.PatientEncounters.Add(encounter2);
            
            var doctorRole = TestRoleBuilder.Create(tenantId).WithName("Doctor").Save(context);
            var nurseRole = TestRoleBuilder.Create(tenantId).WithName("Nurse").Save(context);
            
            var doctorUser = TestUserBuilder.Create(tenantId).Save(context);
            var nurseUser = TestUserBuilder.Create(tenantId).Save(context);

            var doctor = TestStaffMemberBuilder.Create().WithUser(doctorUser).Save(context);
            var nurse = TestStaffMemberBuilder.Create().WithUser(nurseUser).Save(context);

            TestJobBuilder.Create(tenantId).WithStaffMember(doctor).AsPrimary().WithJobRole(doctorRole).Save(context);
            TestJobBuilder.Create(tenantId).WithStaffMember(nurse).AsPrimary().WithJobRole(nurseRole).Save(context);

            context.StaffEncounters.Add(new StaffEncounter
                { Encounter = encounter1, Staff = doctor, CreationTime = new DateTime(2020, 1, 1) });
            context.StaffEncounters.Add(new StaffEncounter
                { Encounter = encounter1, Staff = nurse, CreationTime = new DateTime(2021, 1, 1) });

            context.StaffEncounters.Add(new StaffEncounter
                { Encounter = encounter2, Staff = doctor, CreationTime = new DateTime(2024, 1, 1) });
            context.StaffEncounters.Add(new StaffEncounter
                { Encounter = encounter2, Staff = nurse, CreationTime = new DateTime(2023, 1, 1) });

            context.SaveChanges();
            return (patient, encounter2, doctorUser, nurseUser);
        });

        var input = new GetPatientDetailsInput
        {
            PatientId = patient.Id,
            EncounterId = encounter.Id
        };

        // Act
        var result = await _patientsAppService.GetPatientDetails(input);

        // Assert
        result.LastSeenByDoctorName.ShouldBe(doctorUser.FullName);
        result.LastSeenByNurseName.ShouldBe(nurseUser.FullName);
        result.LastSeenByDoctor.ShouldBe(new DateTime(2024, 1, 1));
        result.LastSeenByNurse.ShouldBe(new DateTime(2023, 1, 1));
    }

    [Fact]
    public async Task GetPatientDetails_GivenValidInput_ShouldReturnDaysPostOp()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        var tenantId = AbpSession.TenantId.Value;
        var (patient, encounter) = UsingDbContext(context =>
        {
            var patient = context.Patients.Add(GetPatient()).Entity;
            var encounter = new PatientEncounter { Patient = patient };
            context.PatientEncounters.Add(encounter);

            var specializedProcedure = new SpecializedProcedure
            {
                TenantId = tenantId,
                Procedure = new Procedure
                {
                    Patient = patient,
                    ProcedureType = ProcedureType.RecordProcedure,
                    ProcedureStatus = ProcedureStatus.Done,
                    CreationTime = new DateTime(2020, 1, 1),
                    Encounter = encounter
                },
                ProposedDate = DateTime.Now.AddDays(-10),
            };

            context.SpecializedProcedures.Add(specializedProcedure);
            context.SaveChanges();
            return (patient, encounter);
        });

        var input = new GetPatientDetailsInput
        {
            PatientId = patient.Id,
            EncounterId = encounter.Id
        };

        // Act
        var result = await _patientsAppService.GetPatientDetails(input);

        // Assert
        result.DaysPostOp.ShouldBe(10);
    }

    private static Patient GetPatient()
    {
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Joe",
            PhoneNumber = "08060421709",
            EmailAddress = "test@example.com",
            DateOfBirth = DateTime.Now,
            GenderType = GenderType.Male,
            Title = TitleType.Mr
        };
        return patient;
    }
    private Facility SetDefaultFacilityPatientCode(EHRDbContext context, PatientCodeTemplate patientCodeTemplate)
    {
        var facility = context.Facilities.Include(x => x.PatientCodeTemplate)
            .FirstOrDefault(x => x.Name == "Default");
        if (facility != null)
        {
            facility.PatientCodeTemplate = patientCodeTemplate;
        }

        context.SaveChanges();
        return facility;
    }

    private void AssignDefaultStaffMemberToFacility(EHRDbContext context, long facilityId, bool isDefault = false)
    {
        var defaultStaffMember = context.StaffMembers.FirstOrDefault();
        if (!context.FacilityStaff.Any(x => x.FacilityId == facilityId && x.StaffMemberId == defaultStaffMember.Id))
        {
            var staffFacility = new FacilityStaff
            {
                FacilityId = facilityId,
                IsDefault = true,
                StaffMemberId = defaultStaffMember.Id,
            };
            defaultStaffMember.AssignedFacilities.Add(staffFacility);
            context.SaveChanges();
            return;
        }
        var staffs = context.FacilityStaff
            .Where(x => x.StaffMemberId == defaultStaffMember.Id).ToList();
        foreach (var staff in staffs)
        {
            staff.IsDefault = false;
        }

        var updated = staffs.FirstOrDefault(x => x.FacilityId == facilityId && x.StaffMemberId == defaultStaffMember.Id);
        if (updated != null)
            updated.IsDefault = isDefault;
        context.SaveChanges();
    }
}