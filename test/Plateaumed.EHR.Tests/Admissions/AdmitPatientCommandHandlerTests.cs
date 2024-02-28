using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using NSubstitute;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Admissions.Handlers;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Admissions
{
    [Trait("Category", "Unit")]
    public class AdmitPatientCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateAdmission()
        {
            // Arrange
            var request = new AdmitPatientRequest
            {
                UnitId = 12,
                AttendingPhysicianId = 34,
                PatientId = 56,
                WardId = 78,
                WardBedId = 90,
                FacilityId = 23,
                ServiceCentre = ServiceCentreType.InPatient
            };

            var repository = Substitute.For<IRepository<Admission, long>>();
            Admission admission = null;
            await repository.InsertAsync(Arg.Do<Admission>(x => admission = x));

            var handler = CreateHandler(repository, Substitute.For<IEncounterManager>());
            // Act
            await handler.Handle(request);
            // Assert
            admission.PatientId.ShouldBe(request.PatientId);
        }
        
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateEncounter()
        {
            // Arrange
            var request = new AdmitPatientRequest
            {
                UnitId = 12,
                AttendingPhysicianId = 34,
                PatientId = 56,
                WardId = 78,
                WardBedId = 90,
                FacilityId = 23,
                ServiceCentre = ServiceCentreType.InPatient
            };

            var repository = Substitute.For<IRepository<Admission, long>>();

            var encounterManager = Substitute.For<IEncounterManager>();
            
            var handler = CreateHandler(repository, encounterManager);
            // Act
            await handler.Handle(request);
            // Assert
            await encounterManager.Received().AdmitPatient(Arg.Any<CreateAdmissionEncounterRequest>());
        }

        [Fact]
        public async Task Handle_GivenValidRequestContainsEncounterId_ShouldValidateEncounter()
        {
            // Arrange
            var request = new AdmitPatientRequest
            {
                EncounterId = 13,
                ServiceCentre = ServiceCentreType.InPatient
            };

            var repository = Substitute.For<IRepository<Admission, long>>();

            var encounterManager = Substitute.For<IEncounterManager>();

            var handler = CreateHandler(repository, encounterManager);
            // Act
            await handler.Handle(request);
            // Assert
            await encounterManager.Received().CheckEncounterExists(request.EncounterId.Value);
        }

        [Fact]
        public async Task Handle_GivenAnExistingAdmissionForEncounterId_ShouldThrow()
        {
            // Arrange
            var request = new AdmitPatientRequest
            {
                EncounterId = 13,
                ServiceCentre = ServiceCentreType.InPatient
            };

            var repository = Substitute.For<IRepository<Admission, long>>();
            repository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Admission, bool>>>()).Returns(Task.FromResult(new Admission()));

            var encounterManager = Substitute.For<IEncounterManager>();

            var handler = CreateHandler(repository, encounterManager);
            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            // Assert
            exception.Message.ShouldBe("Patient has already been admitted");
        }

        private static AdmitPatientCommandHandler CreateHandler(IRepository<Admission, long> repository, IEncounterManager encounterManager = null)
        {
            var facilityRepository = Substitute.For<IRepository<Facility, long>>();
            facilityRepository.GetAsync(Arg.Any<long>()).Returns(new Facility());
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(Arg.Any<long>()).Returns(new Patient());
            var staffRepository = Substitute.For<IRepository<StaffMember, long>>();
            staffRepository.GetAsync(Arg.Any<long>()).Returns(new StaffMember());
            encounterManager ??= Substitute.For<IEncounterManager>();
            
            var wardRepository = Substitute.For<IRepository<Ward, long>>();
            wardRepository.GetAsync(Arg.Any<long>()).Returns(new Ward());
            var wardBedRepository = Substitute.For<IRepository<WardBed, long>>();
            wardBedRepository.GetAsync(Arg.Any<long>()).Returns(new WardBed());

            return new AdmitPatientCommandHandler(repository, facilityRepository, patientRepository,
                staffRepository, encounterManager, Substitute.For<IUnitOfWorkManager>(), wardRepository, wardBedRepository);
        }
    }
}
