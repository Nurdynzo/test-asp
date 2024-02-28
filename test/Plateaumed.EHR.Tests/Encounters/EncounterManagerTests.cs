using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Encounters;

[Trait("Category", "Unit")]
public class EncounterManagerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task AdmitPatient_GivenAdmissionId_ShouldCreateEncounterWithAdmission()
    {
        // Arrange
        var request = new CreateAdmissionEncounterRequest
        {
            AdmissionId = 1,
            UnitId = 12,
            AttendingPhysicianId = 34,
            PatientId = 56,
            WardId = 78,
            WardBedId = 90,
            FacilityId = 23,
            ServiceCentre = ServiceCentreType.InPatient
        };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        PatientEncounter encounter = null;
        await repository.InsertAsync(Arg.Do<PatientEncounter>(x => encounter = x));

        var manager = CreateManager(repository);
        // Act
        await manager.AdmitPatient(request);
        // Assert
        encounter.AdmissionId.ShouldBe(request.AdmissionId);
        encounter.UnitId.ShouldBe(request.UnitId);
        encounter.PatientId.ShouldBe(request.PatientId);
        encounter.WardId.ShouldBe(request.WardId);
        encounter.WardBedId.ShouldBe(request.WardBedId);
        encounter.ServiceCentre.ShouldBe(ServiceCentreType.InPatient);
    }

    [Fact]
    public async Task CheckEncounterExists_GivenEncounterId_ShouldCheckRepository()
    {
        // Arrange
        const long encounterId = 1;

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();

        var manager = CreateManager(repository);
        // Act
        await manager.CheckEncounterExists(encounterId);
        // Assert
        await repository.Received().GetAsync(encounterId);
    }

    [Fact]
    public async Task RequestTransferPatient_ShouldSetEncounterToTransferOutPending()
    {
        // Arrange
        const long encounterId = 1;
        const long wardId = 2;
        const long wardBedId = 3;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            WardId = 4,
            WardBedId = 5
        };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        var manager = CreateManager(repository);
        // Act
        await manager.RequestTransferPatient(encounterId, wardId, wardBedId, PatientStabilityStatus.Stable);
        // Assert
        encounter.Status.ShouldBe(EncounterStatusType.TransferOutPending);
        await repository.Received().UpdateAsync(encounter);
    }

    [Fact]
    public async Task RequestTransferPatient_ShouldCreateANewEncounterWithTransferInPending()
    {
        // Arrange
        const long encounterId = 1;
        const long wardId = 2;
        const long wardBedId = 3;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            WardId = 4,
            WardBedId = 5,
            PatientId = 6,
            Status = EncounterStatusType.InProgress,
            ServiceCentre = ServiceCentreType.InPatient,
            UnitId = 7,
            FacilityId = 8,
            AdmissionId = 9
        };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        PatientEncounter newEncounter = null;
        await repository.InsertAsync(Arg.Do<PatientEncounter>(x => newEncounter = x));

        var manager = CreateManager(repository);
        // Act
        await manager.RequestTransferPatient(encounterId, wardId, wardBedId, PatientStabilityStatus.CriticallyIll);
        // Assert
        newEncounter.PatientId.ShouldBe(encounter.PatientId);
        newEncounter.Status.ShouldBe(EncounterStatusType.TransferInPending);
        newEncounter.ServiceCentre.ShouldBe(encounter.ServiceCentre);
        newEncounter.UnitId.ShouldBe(encounter.UnitId);
        newEncounter.FacilityId.ShouldBe(encounter.FacilityId);
        newEncounter.AdmissionId.ShouldBe(encounter.AdmissionId);
        newEncounter.WardId.ShouldBe(wardId);
        newEncounter.WardBedId.ShouldBe(wardBedId);
    }

    [Fact]
    public async Task CompleteTransferPatient_GivenTransferOutPending_ShouldSetEncounterToTransferred()
    {
        // Arrange
        const long encounterId = 1;
        const long patientId = 5;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            Status = EncounterStatusType.TransferOutPending,
            PatientId = patientId
        };

        var nextEncounter = new PatientEncounter
            { PatientId = patientId, Status = EncounterStatusType.TransferInPending };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        repository.GetAll().Returns(new[] { nextEncounter }.AsQueryable());

        var manager = CreateManager(repository);
        // Act
        await manager.CompleteTransferPatient(encounterId);
        // Assert
        encounter.Status.ShouldBe(EncounterStatusType.Transferred);
        await repository.Received().UpdateAsync(encounter);
    }

    [Fact]
    public async Task CompleteTransferPatient_GivenTransferInPending_ShouldSetEncounterToInProgress()
    {
        // Arrange
        const long encounterId = 1;
        const long patientId = 5;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            Status = EncounterStatusType.TransferOutPending,
            PatientId = patientId
        };

        var nextEncounter = new PatientEncounter
            { PatientId = patientId, Status = EncounterStatusType.TransferInPending };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        repository.GetAll().Returns(new[] { nextEncounter }.AsQueryable());

        var manager = CreateManager(repository);
        // Act
        await manager.CompleteTransferPatient(encounterId);
        // Assert
        nextEncounter.Status.ShouldBe(EncounterStatusType.InProgress);
        await repository.Received().UpdateAsync(nextEncounter);
    }

    [Theory]
    [InlineData(ServiceCentreType.InPatient, EncounterStatusType.DischargePending)]
    [InlineData(ServiceCentreType.OutPatient, EncounterStatusType.Discharged)]
    public async Task RequestDischargePatient_GivenServiceCentre_ShouldSetEncounterStatus(ServiceCentreType serviceCentre, EncounterStatusType encounterStatus)
    {
        // Arrange
        const long encounterId = 1;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            Status = EncounterStatusType.InProgress,
            ServiceCentre = serviceCentre
        };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        var manager = CreateManager(repository);
        // Act
        await manager.RequestDischargePatient(encounterId);
        // Assert
        encounter.Status.ShouldBe(encounterStatus);
        await repository.Received().UpdateAsync(encounter);
    }

    [Fact]
    public async Task CompleteDischargePatient_ShouldSetEncounterToInDischarged()
    {
        // Arrange
        const long encounterId = 1;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            Status = EncounterStatusType.DischargePending,
        };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        var manager = CreateManager(repository);
        // Act
        await manager.CompleteDischargePatient(encounterId);
        // Assert
        encounter.Status.ShouldBe(EncounterStatusType.Discharged);
        await repository.Received().UpdateAsync(encounter);
    }

    [Fact]
    public async Task MarkAsDeceased_ShouldSetEncounterToInDeceased()
    {
        // Arrange
        const long encounterId = 1;

        var encounter = new PatientEncounter
        {
            Id = encounterId,
            Status = EncounterStatusType.InProgress,
        };

        var repository = Substitute.For<IRepository<PatientEncounter, long>>();
        repository.GetAsync(encounterId).Returns(encounter);

        var manager = CreateManager(repository);
        // Act
        await manager.MarkAsDeceased(encounterId);
        // Assert
        encounter.Status.ShouldBe(EncounterStatusType.Deceased);
        await repository.Received().UpdateAsync(encounter);
    }

    private EncounterManager CreateManager(IRepository<PatientEncounter, long> repository)
    {
        var orgUnitRepository = Substitute.For<IRepository<OrganizationUnitExtended, long>>();
        orgUnitRepository.GetAsync(Arg.Any<long>()).Returns(new OrganizationUnitExtended());
        var facilityRepository = Substitute.For<IRepository<Facility, long>>();
        facilityRepository.GetAsync(Arg.Any<long>()).Returns(new Facility());
        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        patientRepository.GetAsync(Arg.Any<long>()).Returns(new Patient());
        var staffRepository = Substitute.For<IRepository<StaffMember, long>>();
        staffRepository.GetAsync(Arg.Any<long>()).Returns(new StaffMember());
        var wardRepository = Substitute.For<IRepository<Ward, long>>();
        wardRepository.GetAsync(Arg.Any<long>()).Returns(new Ward());
        var wardBedRepository = Substitute.For<IRepository<WardBed, long>>();
        wardBedRepository.GetAsync(Arg.Any<long>()).Returns(new WardBed());
        var stabilityStatus = Substitute.For<IRepository<PatientStability, long>>();
        stabilityStatus.GetAsync(Arg.Any<long>()).Returns(new PatientStability());
        return new EncounterManager(repository, orgUnitRepository, facilityRepository, patientRepository,
            wardRepository, wardBedRepository, stabilityStatus, Substitute.For<IUnitOfWorkManager>(), _objectMapper, Substitute.For<IWardBedsManager>());
    }
}