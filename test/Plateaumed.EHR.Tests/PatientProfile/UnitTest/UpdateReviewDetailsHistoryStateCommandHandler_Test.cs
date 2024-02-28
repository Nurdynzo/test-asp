using Abp.Domain.Repositories;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Command;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Vaccines;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.UnitTest
{
    [Trait("Category", "Unit")]
    public class UpdateReviewDetailsHistoryStateCommandHandler_Test
    {
        private readonly IRepository<ReviewDetailsHistoryState, long> _reviewDetailsHistoryStateRepository
            = Substitute.For<IRepository<ReviewDetailsHistoryState, long>>();


        [Fact]
        public async Task UpdateReviewDetailsHistoryStatesShould_UpdateSuccessfully()
        {
            //Arrange
            _reviewDetailsHistoryStateRepository.GetAll().Returns(GetReviewDetailsHistoryStates().BuildMock());
            var edit = new ReviewDetailsHistoryStateDto
            {
                PatientId = 1,
                Id = 1,
                PatientDoesNotTakeAlcohol = true,
                PatientDoesNotSmoke = true,
                NoFamilyHistory = true,
                NoPhysicalExerciseHistory = true,
                NoBloodTransfusionHistory = true,
                NoChronicIllness = true,
                NoMajorInjuries = true,
                NoTravelHistory = true,
                NoSurgicalHistory = true,
                NoVaccinationHistory = false,
                NoUseOfContraceptives = true,
                NoGynaecologicalIllness = true,
                NoGynaecologicalSurgery = true,
                NoHistoryOfCervicalScreening = true,
                NeverBeenPregnant = true,
                NoDeliveryDetails = true,
                NoUseOfRecreationalDrugs = true,
                NotCurrentlyOnMedication = true,
                NoAllergies = true,
                NoImplant = true,
            };
            //Act
            var handler = new UpdateReviewDetailsHistoryStateCommandHandler(_reviewDetailsHistoryStateRepository);
            await handler.Handle(edit, "Test Editor");
            var result = _reviewDetailsHistoryStateRepository.GetAll().SingleOrDefault(x => x.Id == edit.Id);
            //Assert
            result.NoVaccinationHistory.ShouldBe(false);
            result.LastEditorName.ShouldBe("Test Editor");
        }


        private static IQueryable<ReviewDetailsHistoryState> GetReviewDetailsHistoryStates()
        {
            return new List<ReviewDetailsHistoryState>()
            {
                new()
                {
                    Id = 1,
                    PatientId = 1,
                    PatientDoesNotTakeAlcohol = true,
                    PatientDoesNotSmoke = true,
                    NoFamilyHistory = true,
                    NoPhysicalExerciseHistory = true,
                    NoBloodTransfusionHistory = true,
                    NoChronicIllness = true,
                    NoMajorInjuries = true,
                    NoTravelHistory = true,
                    NoSurgicalHistory = true,
                    NoVaccinationHistory = true,
                    NoUseOfContraceptives = true,
                    NoGynaecologicalIllness = true,
                    NoGynaecologicalSurgery = true,
                    NoHistoryOfCervicalScreening = true,
                    NeverBeenPregnant = true,
                    NoDeliveryDetails = true,
                    NoUseOfRecreationalDrugs = true,
                    NotCurrentlyOnMedication = true,
                    NoAllergies = true,
                    NoImplant = true,
        }
            }.AsQueryable();
        }
    }
}
