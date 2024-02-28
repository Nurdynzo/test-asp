using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Abstraction;
using Plateaumed.EHR.VitalSigns.Dto;
using Plateaumed.EHR.VitalSigns.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.VitalSigns;

[Trait("Category", "Unit")]
public class VitalSignValidatorTests
{
    
    [Fact]
    public async Task Handle_GivenCorrectVitalReading_ShouldNotThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act  
        var exception = await Record.ExceptionAsync(() => handler.ValidateRequest(32, vitalSign, null));
        
        // Assert
        Assert.Null(exception);
    }
    
    [Fact]
    public async Task Handle_GivenInvalidVitalReading_ShouldThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.ValidateRequest(0, vitalSign, null));
            
        // Assert
        exception.Message.ShouldBe("Vital reading cannot be less than or equal zero."); 
    }
    
    [Fact]
    public async Task Handle_GivenMaxVitalReadingLength_ShouldThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.ValidateRequest(3982, vitalSign, null));
            
        // Assert
        exception.Message.ShouldBe($"The vital reading for {vitalSign.Sign} is greater than the max length of {vitalSign.MaxLength.ToString()}."); 
    }
    
    [Fact]
    public async Task Handle_GivenMaxVitalReadingDecimalLength_ShouldThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.ValidateRequest(398.234, vitalSign, null));
            
        // Assert
        exception.Message.ShouldBe($"The value after the decimal point for {vitalSign.Sign} is greater than the max length of {vitalSign.DecimalPlaces.ToString()}."); 
    }
    
    [Fact]
    public async Task Handle_GivenWrongMesurementRangeId_ShouldThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        measurementRangeRepository.GetAsync(1).Returns(GetMesurementRanges().FirstOrDefault());
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.ValidateRequest(322, vitalSign, 3));
        
        // Assert
        exception.Message.ShouldBe("Measurement Range not found"); 
    }
    
    [Fact]
    public async Task Handle_GivenMaxVitalReadingLength_MesurementRange_ShouldThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        measurementRangeRepository.GetAsync(1).Returns(GetMesurementRanges().FirstOrDefault());
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.ValidateRequest(3982, vitalSign, 1));
            
        // Assert
        exception.Message.ShouldBe($"The vital reading for {vitalSign.Sign} is greater than the max length of {vitalSign.MaxLength.ToString()}."); 
    }
    
    [Fact]
    public async Task Handle_GivenMaxVitalReadingDecimalLength_MesurementRange_ShouldThrowException()
    { 
        var vitalSignValidationManager = Substitute.For<IVitalSignValidator>(); 
        var measurementRangeRepository = Substitute.For<IRepository<MeasurementRange, long>>();
        measurementRangeRepository.GetAsync(1).Returns(GetMesurementRanges().FirstOrDefault());
        
        var handler = new VitalSignValidator(measurementRangeRepository);

        var vitalSign = GetVitalSigns().FirstOrDefault();
        // Act 
        var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.ValidateRequest(398.234, vitalSign, 1));
            
        // Assert
        exception.Message.ShouldBe($"The value after the decimal point for {vitalSign.Sign} is greater than the max length of {vitalSign.DecimalPlaces.ToString()}."); 
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


    private static IQueryable<MeasurementRange> GetMesurementRanges()
    {
        return new List<MeasurementRange>
        {
            new()
            {
                Id = 1,
                Lower = 60,
                Upper = 90,
                Unit = "⁰C",
                DecimalPlaces = 1,
                MaxLength = 3
            },
            new()
            {
                Id = 2,
                Lower = 97.0M,
                Upper = 120,
                Unit = "⁰F",
                DecimalPlaces = 1,
                MaxLength = 4
            }
        }.AsQueryable().BuildMock(); 
    }
}