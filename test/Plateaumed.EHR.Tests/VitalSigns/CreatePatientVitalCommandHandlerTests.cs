using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Abstraction;
using Plateaumed.EHR.VitalSigns.Dto;
using Plateaumed.EHR.VitalSigns.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns
{
    [Trait("Category", "Unit")]
    public class CreatePatientVitalCommandHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenCorrectRequest_ShouldCreatePatientVital()
        {
            // Arrange
            var request = RequestData(); 
            
            var repository = Substitute.For<IRepository<PatientVital, long>>();
            var encounterManager = Substitute.For<IEncounterManager>();
            var updateAppointmentStatus = Substitute.For<IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler>();
            
            var repositoryVitalSign = Substitute.For<IRepository<VitalSign, long>>();
            repositoryVitalSign.GetAsync(1).Returns(GetVitalSigns().FirstOrDefault());
            
            var vitalSignValidationManager = Substitute.For<IVitalSignValidator>();

            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(1).Returns(new Patient { Id = 1});

            var handler = new CreatePatientVitalCommandHandler(repository, Substitute.For<IUnitOfWorkManager>(),
                _objectMapper, patientRepository, encounterManager, repositoryVitalSign, vitalSignValidationManager, updateAppointmentStatus);
            // Act
            List<PatientVital> patientVitals = new();
            await repository.InsertAsync(Arg.Do<PatientVital>(patientVitals.Add));
            await handler.Handle(request);
            // Assert
            await encounterManager.Received().CheckEncounterExists(request.EncounterId);
            patientVitals[0].EncounterId.ShouldBe(request.EncounterId);
            patientVitals[0].PatientId.ShouldBe(request.PatientId); 
        }
        
        [Fact]
        public async Task Handle_GivenInvalidVital_ShouldThrowException()
        {
            // Arrange
            var request = RequestData();
            request.PatientVitals.Add(new CreatePatientVitalDto()
            {
                VitalSignId = 4,
                MeasurementSiteId = 1,
                MeasurementRangeId = 1,
                VitalReading = 0,
                Position = "RIGHT"
            });
            
            var repository = Substitute.For<IRepository<PatientVital, long>>();
            var encounterManager = Substitute.For<IEncounterManager>();
            var updateAppointmentStatus = Substitute.For<IUpdateAppointmentStatusFromAwaitingVitalsCommandHandler>();

            var repositoryVitalSign = Substitute.For<IRepository<VitalSign, long>>();
            repositoryVitalSign.GetAsync(1).Returns(GetVitalSigns().FirstOrDefault());
            
            var vitalSignValidationManager = Substitute.For<IVitalSignValidator>();

            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(1).Returns(new Patient { Id = 1});

            var handler = new CreatePatientVitalCommandHandler(repository, Substitute.For<IUnitOfWorkManager>(),
                _objectMapper, patientRepository, encounterManager, repositoryVitalSign, vitalSignValidationManager, updateAppointmentStatus);
            // Act 
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            
            // Assert
            exception.Message.ShouldBe("Vital not found"); 
        }

        private CreateMultiplePatientVitalDto RequestData()
        {
            return new CreateMultiplePatientVitalDto
            {
                PatientId = 1,
                EncounterId = 1,
                PatientVitals = new List<CreatePatientVitalDto>
                {
                    new()
                    {
                        VitalSignId = 1,
                        MeasurementSiteId = 1,
                        MeasurementRangeId = 1,
                        VitalReading = 1,
                        Position = "LEFT"
                    }
                }
            };
        }
        
        private static IQueryable<VitalSign> GetVitalSigns()
        {
            return new List<VitalSign>
            {
                new()
                {
                    Id = 1,
                    Sign = "Blood Pressure",
                    LeftRight = true,
                    DecimalPlaces = 2,
                    MaxLength = 3,
                    Ranges = new List<MeasurementRange>
                    {
                        new()
                        {
                            Id = 1,
                            Lower = 60,
                            Upper = 90,
                            Unit = "mmHg"
                        },
                        new()
                        {
                            Id = 2,
                            Lower = 90,
                            Upper = 120,
                            Unit = "mmHg"
                        }
                    },
                    Sites = new List<MeasurementSite>
                    {
                        new()
                        {
                            Id = 1,
                            Site = "Left Arm",
                            Default = true
                        },
                        new()
                        {
                            Id = 2,
                            Site = "Right Arm",
                            Default = false
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Sign = "Height",
                    LeftRight = false,
                    DecimalPlaces = 0,
                    MaxLength = 3,
                    Ranges = new List<MeasurementRange>
                    {
                        new()
                        {
                            Id = 3,
                            Lower = 60,
                            Upper = 100,
                            Unit = "cm"
                        }
                    },
                    Sites = new List<MeasurementSite>
                    {
                        new()
                        {
                            Id = 3,
                            Site = "Standing",
                            Default = true
                        },
                        new()
                        {
                            Id = 4,
                            Site = "Lying",
                            Default = false
                        }
                    }
                },
            }.AsQueryable().BuildMock(); 
        }
        
    }
}
