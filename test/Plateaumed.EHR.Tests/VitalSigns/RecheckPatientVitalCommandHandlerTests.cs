using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Abstraction;
using Plateaumed.EHR.VitalSigns.Dto;
using Plateaumed.EHR.VitalSigns.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns;

[Trait("Category", "Unit")]
public class RecheckPatientVitalCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_NoVitalReading_ShouldThrow()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientVital, long>>();
        var repositoryVitalSign = Substitute.For<IRepository<VitalSign, long>>(); 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>();
        var testCreateData = CreateTestData();
        testCreateData.VitalReading = 0;
        
        var handler = new RecheckPatientVitalCommandHandler(repository, _unitOfWork, _objectMapper, repositoryVitalSign, vitalSignValidationManager);
        
        // Act
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(testCreateData));
        
        // Assert
        exception.Message.ShouldBe("Vital reading is required."); 
    }

    [Fact]
    public async Task Handle_GivenCorrect_Request_From_VitalSign()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientVital, long>>(); 
        var repositoryVitalSign = Substitute.For<IRepository<VitalSign, long>>(); 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>();
        
        var testCreateData = CreateTestData();
        testCreateData.Id = 1;

        var existingPatientVital = GetPatientVitalSigns();
        repository.GetAll().Returns(existingPatientVital);

        repositoryVitalSign.GetAsync(1).Returns(GetVitalSigns().FirstOrDefault());
        
        var handler = new RecheckPatientVitalCommandHandler(repository, _unitOfWork, _objectMapper, repositoryVitalSign, vitalSignValidationManager);
        
        // Act
        PatientVital patientVital = null;
        await repository.InsertAsync(Arg.Do<PatientVital>(j => patientVital = j));
        
        await handler.Handle(testCreateData);
        
        // Assert
        patientVital.PatientId.ShouldBe(testCreateData.PatientId);
        patientVital.VitalReading.ShouldBe(testCreateData.VitalReading);
    }
    
    private static RecheckPatientVitalDto CreateTestData()
    {
        return new RecheckPatientVitalDto
        {
            DeleteMostRecentRecord = false,
            PatientId = 1, 
            VitalReading = 70,
            VitalSignId = 1
        };
    }
    
    private static IQueryable<PatientVital> GetPatientVitalSigns()
    {
        return new List<PatientVital>
        {
            new()
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
        }.AsQueryable().BuildMock();
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