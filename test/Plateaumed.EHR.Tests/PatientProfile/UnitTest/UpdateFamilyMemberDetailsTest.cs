using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PatientProfile;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using Shouldly;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateFamilyMemberDetailsTest
    {
        private readonly IRepository<PatientFamilyMembers, long> _familyMembersRepository
           = Substitute.For<IRepository<PatientFamilyMembers, long>>();
        //private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

        [Fact]
        public async Task UpdateRecreationalDrugCommandHandlerShould_UpdateSuccessfully()
        {
            //Arrange
            _familyMembersRepository.GetAll().Returns(GetPatientFamilyMembers().BuildMock());
            var edit = new UpdateFamilyMembersDto
            {
                Id = 1,
                Relationship = EHR.Patients.Relationship.Wife,
                IsAlive = true,
                AgeAtDeath = 0,
                CausesOfDeath = null,
                AgeAtDiagnosis = 0
            };
            //Act
            var handler = new UpdateFamilyMemberDetailsCommandHandler(_familyMembersRepository);
            await handler.Handle(edit);
            var result = _familyMembersRepository.GetAll().SingleOrDefault(x => x.Id == 1);
            //Assert
            result.Relationship.ShouldBe(EHR.Patients.Relationship.Wife);
        }

        private static IQueryable<PatientFamilyMembers> GetPatientFamilyMembers()
        {
            return new List<PatientFamilyMembers>()
            {
                new()
                {
                    Id = 1,
                    Relationship = EHR.Patients.Relationship.GirlFriend,
                    IsAlive = true,
                    AgeAtDeath = 0,
                    CausesOfDeath = null,
                    AgeAtDiagnosis = 0
                }
            }.AsQueryable();
        }
    }
}
