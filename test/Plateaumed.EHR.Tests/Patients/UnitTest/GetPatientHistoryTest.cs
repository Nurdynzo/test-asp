using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Query;
using Shouldly;
using Xunit;


namespace Plateaumed.EHR.Tests.Patients.UnitTest;

[Trait("Category","Unit")]
public class GetPatientHistoryTest
{
    private readonly IRepository<Patient,long> _patientRepository = Substitute.For<IRepository<Patient, long>>();
    private readonly IRepository<Country, int> _countryRepository = Substitute.For<IRepository<Country, int>>();
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository = Substitute.For<IRepository<PatientCodeMapping, long>>();

    [Fact]
    public async Task GetPatientDetailedHistoryQueryHandler_Handler_Returns_PatientDetailsQueryResponse()
    {
        //arrange
        _patientRepository.GetAll().Returns(GetPatientAsQueryable().BuildMock());
        _countryRepository.GetAll().Returns(GetCountries().BuildMock());
        _patientCodeMappingRepository.GetAll().Returns(GetPatientCodeMappingAsQueryable().BuildMock());
         var handler = new GetPatientDetailedHistoryQueryHandler(_patientRepository, _countryRepository, _patientCodeMappingRepository);
        
         // act
         var result = await handler.Handle(1, 1);
       
         // assert
         result.ShouldNotBeNull();
         result.Address.ShouldBe("Test Address");
         result.Nationality.ShouldBe("Nigeria");
         result.FullName.ShouldBe("Test1 Patient1");
         result.EmailAddress.ShouldBe("example@test.com");
         result.MaritalStatus.ShouldBe(MaritalStatus.Married);
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
                NoOfFemaleChildren = 2,
                NoOfMaleChildren = 3,
                Address = "Test Address",
                GenderType = GenderType.Male,
                DateOfBirth = DateTime.Today.AddYears(-40),
                MaritalStatus = MaritalStatus.Married,
                NoOfFemaleSiblings = 3,
                NumberOfSiblings = 4,
                CountryId = 1,
                
                
                
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
           
        }.AsQueryable();
    }

    public IQueryable<Country> GetCountries()
    {
        return new List<Country>()
        {
            new("Nigeria", "Nigeria", "NG", "234", "Naira", "NGN", "NGN")
            {
                Id = 1
            },
        }.AsQueryable();
    }
}