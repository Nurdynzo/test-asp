using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PatientProfile.Query;
using Plateaumed.EHR.Patients;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class GetPatientBloodGroupAndGenotypeRequestHandlerTest
    {
        private readonly IRepository<Patient, long> _patientRepository
          = Substitute.For<IRepository<Patient, long>>();


        [Fact]
        public async Task GetPatientBloodGroupShouldReturnCorrectType()
        {
            //Arrange
            _patientRepository.GetAll().Returns(GetPatients().BuildMock());
            //Act
            var handler = new GetPatientBloodGroupAndGenotypeRequestHandler(_patientRepository);
            var result = await handler.Handle(1);
            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<GetPatientBloodGroupAndGenotypeResponseDto>();
        }

        private static IQueryable<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new()
                {
                    Id = 1,
                    UuId = Guid.NewGuid(),
                    GenderType = EHR.Authorization.Users.GenderType.Male,
                    FirstName = "Test",
                    LastName = "test",
                    PhoneNumber = "48938673",
                    DateOfBirth = DateTime.Now.AddYears(10),
                    BloodGroup = BloodGroup.AB_Positive,
                    BloodGenotype = BloodGenotype.AA,
                }
            }.AsQueryable();
        }
    }
}
