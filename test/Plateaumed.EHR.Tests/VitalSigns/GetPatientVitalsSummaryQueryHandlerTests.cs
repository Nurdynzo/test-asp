using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns;

[Trait("Category", "Unit")]
public class GetPatientVitalsSummaryQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IAbpSession _abpSession = Substitute.For<IAbpSession>();
    private readonly IRepository<User,long> _userRepository = Substitute.For<IRepository<User, long>>();

    [Fact]
    public async Task Handle_ShouldReturn_PatientVitalsSummary()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientVital, long>>();
        var patientVitals = GetPatientVitalSigns();
        repository.GetAll().Returns(patientVitals);
        _abpSession.TenantId.Returns(1);
        
        var handler = new GetPatientVitalsSummaryQueryHandler(repository, _objectMapper,_abpSession, _userRepository);
        // Act
        var result = await handler.Handle(1);

        // Assert 
        result.First().Date.ShouldBe(patientVitals.First().CreationTime);
        result.First().Time.ShouldBe(patientVitals.First().CreationTime);
        result.First().PatientVitals.Count.ShouldBe(4);
        result.First().PatientVitals.First().PatientId.ShouldBe(patientVitals.First().PatientId); 
        result.First().PatientVitals.First().VitalSignId.ShouldBe(patientVitals.First().VitalSignId);
        result.First().PatientVitals.First().MeasurementSiteId.ShouldBe(patientVitals.First().MeasurementSiteId);
        result.First().PatientVitals.First().MeasurementRangeId.ShouldBe(patientVitals.First().MeasurementRangeId);
        
        result.First().PatientVitals.First().VitalReading.ShouldBe(180.1);
        result.First().PatientVitals.First().PatientVitalType.ShouldBe(PatientVitalType.NewRecheckedVital.ToString());
    }
    
    [Fact]
    public async Task Handle_PatientVitalsSummary_OverThreshold_IsTrue()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientVital, long>>();
        var patientVitals = GetPatientVitalSigns();
        
        patientVitals.First().VitalSign.Sign = "Temperature";
        patientVitals.First().VitalReading = 70;
        
        repository.GetAll().Returns(patientVitals);
        _abpSession.TenantId.Returns(1);
        
        var handler = new GetPatientVitalsSummaryQueryHandler(repository, _objectMapper,_abpSession,_userRepository);
        
        // Act
        var result = await handler.Handle(1);

        // Assert 
        result.First().PatientVitals.Count.ShouldBe(4);
        result.First().PatientVitals.Last().OverThreshold.ShouldBe(true);
    }
    
    [Fact]
    public async Task Handle_PatientVitalsSummary_OverThreshold_IsFalse()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientVital, long>>();
        var patientVitals = GetPatientVitalSigns();
        
        patientVitals.First().VitalSign.Sign = "Blood Pressure";
        patientVitals.First().VitalReading = 70;
        
        repository.GetAll().Returns(patientVitals);
        _abpSession.TenantId.Returns(1);
        
        var handler = new GetPatientVitalsSummaryQueryHandler(repository, _objectMapper,_abpSession,_userRepository);
        
        // Act
        var result = await handler.Handle(1);

        // Assert 
        result.First().PatientVitals.Count.ShouldBe(4);
        result.First().PatientVitals.Last().OverThreshold.ShouldBe(false);
    }

    private static IQueryable<PatientVital> GetPatientVitalSigns()
    {
        return new List<PatientVital>
        {
            new()
            {
                TenantId = 1, 
                PatientId = 1,
                VitalSignId = 1,
                VitalSign = new VitalSign()
                {
                    Id = 1,
                    Sign = "Blood Pressure",
                    LeftRight = true,
                    DecimalPlaces = 0
                },
                MeasurementSiteId = 1,
                MeasurementSite = new MeasurementSite()
                {
                    Id = 1,
                    Site = "Left Arm",
                    Default = true
                },
                MeasurementRangeId = 1,
                MeasurementRange = new MeasurementRange()
                {
                    Id = 1,
                    Lower = 60,
                    Upper = 90,
                    Unit = "mmHg"
                },
                VitalReading = 70,
                PatientVitalType = PatientVitalType.NormalVital,
                IsDeleted = false,
                CreationTime = DateTime.UtcNow,
                CreatorUserId = 1,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Surtest"
                }
            },
            new()
            {
                TenantId = 1, 
                PatientId = 1,
                VitalSignId = 1,
                VitalSign = new VitalSign()
                {
                    Id = 2,
                    Sign = "Height",
                    LeftRight = false,
                    DecimalPlaces = 1
                },
                MeasurementSiteId = 1,
                MeasurementSite = new MeasurementSite()
                {
                    Id = 3,
                    Site = "Standing",
                    Default = true
                },
                MeasurementRangeId = 1,
                MeasurementRange = new MeasurementRange()
                {
                    Id = 3,
                    Lower = 60,
                    Upper = 100,
                    Unit = "cm"
                },
                VitalReading = 180.1,
                PatientVitalType = PatientVitalType.NormalVital,
                IsDeleted = false,
                CreationTime = DateTime.UtcNow,
                CreatorUserId = 1,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Surtest"
                }
            },
            new()
            {
                TenantId = 1, 
                PatientId = 1,
                VitalSignId = 1,
                VitalSign = new VitalSign()
                {
                    Id = 2,
                    Sign = "Height",
                    LeftRight = false,
                    DecimalPlaces = 1
                },
                MeasurementSiteId = 1,
                MeasurementSite = new MeasurementSite()
                {
                    Id = 3,
                    Site = "Standing",
                    Default = true
                },
                MeasurementRangeId = 1,
                MeasurementRange = new MeasurementRange()
                {
                    Id = 3,
                    Lower = 60,
                    Upper = 100,
                    Unit = "cm"
                },
                VitalReading = 180.1,
                PatientVitalType = PatientVitalType.OldVital,
                IsDeleted = false,
                CreationTime = DateTime.UtcNow,
                CreatorUserId = 1,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Surtest"
                },
                LastModificationTime = DateTime.UtcNow,
                LastModifierUserId = 1,
                LastModifierUser = new User()
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Surtest"
                }
            },
            new()
            {
                TenantId = 1, 
                PatientId = 1,
                VitalSignId = 1,
                VitalSign = new VitalSign()
                {
                    Id = 2,
                    Sign = "Height",
                    LeftRight = false,
                    DecimalPlaces = 1
                },
                MeasurementSiteId = 1,
                MeasurementSite = new MeasurementSite()
                {
                    Id = 3,
                    Site = "Standing",
                    Default = true
                },
                MeasurementRangeId = 1,
                MeasurementRange = new MeasurementRange()
                {
                    Id = 3,
                    Lower = 60,
                    Upper = 100,
                    Unit = "cm"
                },
                VitalReading = 180.1,
                PatientVitalType = PatientVitalType.NewRecheckedVital,
                IsDeleted = false,
                CreationTime = DateTime.UtcNow,
                CreatorUserId = 1,
                CreatorUser = new User()
                {
                    Id = 1,
                    Name = "Test",
                    Surname = "Surtest"
                }
            }
        }.AsQueryable().BuildMock();
    }
    
}
