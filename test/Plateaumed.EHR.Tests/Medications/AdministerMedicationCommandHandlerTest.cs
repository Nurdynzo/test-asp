using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Medication.Handlers;
using Shouldly;
using Xunit;
namespace Plateaumed.EHR.Tests.Medications
{
    [Trait("Category","Unit")]
    public class AdministerMedicationCommandHandlerTest
    {
        private readonly IAdministerMedicationCommandHandler _administerMedicationCommandHandlerMock;
        private readonly IEncounterManager _encounterManagerMock
            = Substitute.For<IEncounterManager>();
        private readonly IRepository<AllInputs.Medication,long> _medicationRepositoryMock
            = Substitute.For<IRepository<AllInputs.Medication, long>>();
        private readonly IRepository<MedicationAdministrationActivity, long> _medicationAdministrationActivityRepository
            = Substitute.For<IRepository<MedicationAdministrationActivity, long>>();
        private readonly IObjectMapper _objectMapperMock = AutoMapperTestHelpers.CreateRealObjectMapper();
        public AdministerMedicationCommandHandlerTest()
        {
            _administerMedicationCommandHandlerMock =
                new AdministerMedicationCommandHandler(
                    _medicationAdministrationActivityRepository,_encounterManagerMock,_objectMapperMock,_medicationRepositoryMock);
        }
        [Fact]
        public async void Handle_GivenValidRequest_ShouldCreateMedicationAdministrationActivity()
        {
            // Arrange
            var request = new MedicationAdministrationActivityRequest
            {
                MedicationId = 1,
                PatientEncounterId = 1
            };
            _medicationRepositoryMock.GetAsync(1).Returns(new AllInputs.Medication());
            _encounterManagerMock.CheckEncounterExists(1).Returns(Task.CompletedTask);
            // Act
            await _administerMedicationCommandHandlerMock.Handle(request);
            // Assert
            await _medicationAdministrationActivityRepository.Received(1).InsertAsync(Arg.Any<MedicationAdministrationActivity>());
        }
        [Fact]
        public async void Handle_GivenInvalidMedicationId_ShouldThrowException()
        {
            // Arrange
            var request = new MedicationAdministrationActivityRequest
            {
                MedicationId = 0,
                PatientEncounterId = 1
            };
            _medicationRepositoryMock.GetAsync(request.MedicationId).Returns((AllInputs.Medication)null);
            // Act and Assert
            var message = await Assert.ThrowsAsync<UserFriendlyException>(() => _administerMedicationCommandHandlerMock.Handle(request));
            message.Message.ShouldBe("Medication with id 0 doesn't exist.");

        }
        [Fact]
        public async void Handle_GivenInvalidEncounterId_ShouldThrowException()
        {
            // Arrange
            var request = new MedicationAdministrationActivityRequest
            {
                MedicationId = 1,
                PatientEncounterId = 0
            };
            _medicationRepositoryMock.GetAsync(request.MedicationId).Returns(new AllInputs.Medication());
            _encounterManagerMock.CheckEncounterExists(request.PatientEncounterId).Returns(Task.FromException(new UserFriendlyException("")));
            // Act and Assert
            await Assert.ThrowsAsync<UserFriendlyException>(() => _administerMedicationCommandHandlerMock.Handle(request));
        }

    }
}
