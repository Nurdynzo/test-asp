using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Shouldly;
using System;
using System.Threading.Tasks;
using Plateaumed.EHR.Patients.Dtos;
using System.Linq;
using System.Collections.Generic;

namespace Plateaumed.EHR.Tests.Patients;

public class PatientRelationsServiceTest : AppTestBase
{
    private readonly IPatientsAppService _patientsAppService;
    private readonly IPatientRelationsAppService _patientRelationsAppService;

    public PatientRelationsServiceTest()
    {
        _patientsAppService = Resolve<IPatientsAppService>();
        _patientRelationsAppService = Resolve<IPatientRelationsAppService>();
    }

    [MultiTenantFact]
    public async Task GetAllNextOfKin_Should_Return_Data_When_Next_Of_Kin_Exist()  
    {
        // Arrange
        LoginAsDefaultTenantAdmin();

        var wife = GetCreateOrEditPatientRelationDto(isGuardian: false, relationship: Relationship.Wife);

        var father = GetCreateOrEditPatientRelationDto(relationship: Relationship.Father, firstName: "Benjamin", lastName: "Joe", phoneNumber: "08060989278", email: "ben@example.com");

        var relations = new List<CreateOrEditPatientRelationDto>() { wife, father};

        var patient = await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "Tobi", phoneNumber: "08060421709", genderType: GenderType.Male, relations: relations));

        // Act
        var patientNextOfKins = await _patientRelationsAppService.GetAllNextOfKin(patient.Id ?? 0);

        var relation = patientNextOfKins.FirstOrDefault();


        // Assert
        patientNextOfKins.Count.ShouldBe(1);
        relation.PhoneNumber.ShouldBe(wife.PhoneNumber);
        relation.Relationship.ShouldBe(wife.Relationship);
        relation.FullName.ShouldContain(wife.FirstName);
        relation.FullName.ShouldContain(wife.LastName);
    }
    
    
    [MultiTenantFact]
    public async Task GetAllNextOfKin_Should_Return_Empty_List_When_Next_Of_Kin_Does_Not_Exist() 
    {
        // Arrange
        LoginAsDefaultTenantAdmin();

        var father = GetCreateOrEditPatientRelationDto(relationship: Relationship.Father, firstName: "Benjamin", lastName: "Joe", phoneNumber: "08060989278", email: "ben@example.com");

        var relations = new List<CreateOrEditPatientRelationDto>() { father};

        var patient = await _patientsAppService.CreateOrEdit(
            GetCreateOrEditPatientDto(firstName: "Tobi", phoneNumber: "08060421709", genderType: GenderType.Male, relations: relations));

        // Act
        var patientNextOfKins = await _patientRelationsAppService.GetAllNextOfKin(patient.Id ?? 0);


        // Assert
        patientNextOfKins.ShouldBeEmpty();
    }


    private static CreateOrEditPatientDto GetCreateOrEditPatientDto(
        string firstName = "John",
        string lastName = "Joe",
        string email = "test@example.com",
        string phoneNumber = "1234567890",
        GenderType genderType = GenderType.Female,
        DateTime dateOfBirth = default(DateTime),
        string patientCode = "ZZZ-1091012",
        List<CreateOrEditPatientRelationDto> relations = null
    )
    {

        var patient = new CreateOrEditPatientDto
        {
            FirstName = firstName,
            LastName = lastName,
            EmailAddress = email,
            PhoneNumber = phoneNumber,
            GenderType = genderType,
            DateOfBirth = dateOfBirth == default(DateTime) ? DateTime.Now.AddYears(-40) : dateOfBirth,
            PatientCode = patientCode,
            Relations = relations
        };
        return patient;
    }
    
    
    private static CreateOrEditPatientRelationDto GetCreateOrEditPatientRelationDto(
        string firstName = "Soe",
        string lastName = "Joe",
        string email = "soejoe@example.com",
        string phoneNumber = "1234567890",
        Relationship relationship = Relationship.Friend,
        bool isGuardian = true
    )
    {

        var relation = new CreateOrEditPatientRelationDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Email = email,
            Relationship = relationship,
            IsGuardian = isGuardian
        };
        return relation;
    }

}

