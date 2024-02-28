using System.Threading.Tasks;
using Abp.Domain.Repositories;
using NSubstitute;
using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Admissions.Handlers;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;
using Xunit;

namespace Plateaumed.EHR.Tests.Admissions;

[Trait("Category", "Unit")]
public class TransferPatientCommandHandlerTests
{
    [Fact]
    public async Task Handle_GivenValidRequest_ShouldTransferPatient()
    {
        // Arrange
        var request = new TransferPatientRequest
        {
            EncounterId = 2,
            PatientId = 2,
            WardId = 7,
            WardBedId = 5,
            Status = Misc.PatientStabilityStatus.CriticallyIll
        };
        
        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = TransferHandler(encounterManager);
        
        // Act
        await handler.Handle(request);
        
        // Assert
        await encounterManager.Received().RequestTransferPatient(request.EncounterId, request.WardId, request.WardBedId, Misc.PatientStabilityStatus.CriticallyIll);
    }
    
    private static TransferPatientCommandHandler TransferHandler(IEncounterManager encounterManager = null)
    { 
        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        patientRepository.GetAsync(Arg.Any<long>()).Returns(new Patient()); 
            
        var wardRepository = Substitute.For<IRepository<Ward, long>>();
        wardRepository.GetAsync(Arg.Any<long>()).Returns(new Ward());
        var wardBedRepository = Substitute.For<IRepository<WardBed, long>>();
        wardBedRepository.GetAsync(Arg.Any<long>()).Returns(new WardBed());

        return new TransferPatientCommandHandler(patientRepository,
            wardRepository, wardBedRepository, encounterManager);
    }
}