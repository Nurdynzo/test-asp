using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientProfile.Integration;

[Trait("Category", "Integration")]
public class PatientProfileAppServiceTest : AppTestBase
{
    private  readonly IPatientProfileAppService _patientProfileAppService;
    private readonly IObjectMapper _objectMapper;

    public PatientProfileAppServiceTest()
    {
        _patientProfileAppService = Resolve<IPatientProfileAppService>();
        _objectMapper = Resolve<IObjectMapper>();
    }

    [MultiTenantFact]
    public async Task CreatePatientAllergy_With_Valid_Input_Should_Create_PatientAllergy()
    {
        //Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(ctx =>
        {
            var patient = GetPatient();
            ctx.Patients.Add(patient);
            ctx.SaveChanges();
            patientId = patient.Id;
        });

        var request = GetCreatePatientAllergyCommandRequest(patientId);
        //Act
        await _patientProfileAppService.CreatePatientAllergy(request);
        //Assert
        PatientAllergy patientAllergy = null;
        UsingDbContext(ctx =>
        {
            patientAllergy = ctx.PatientAllergies.FirstOrDefault(x=>x.PatientId == patientId);
        });
        patientAllergy.ShouldNotBeNull();
        patientAllergy.AllergyType.ShouldBe(request.AllergyType);
        patientAllergy.Reaction.ShouldBe(request.Reaction);
        patientAllergy.Notes.ShouldBe(request.Notes);
        patientAllergy.Severity.ShouldBe(request.Severity);
        patientAllergy.AllergySnomedId.ShouldBe(request.AllergySnomedId);
        patientAllergy.ReactionSnomedId.ShouldBe(request.ReactionSnomedId);

    }
    [MultiTenantFact]
    public async Task EditAllergy_With_Valid_Input_Should_Update_PatientAllergy()
    {
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(ctx =>
        {
            var patient = GetPatient();
            ctx.Patients.Add(patient);
            ctx.SaveChanges();
            patientId = patient.Id;
        });
        var request = GetCreatePatientAllergyCommandRequest(patientId);
        await _patientProfileAppService.CreatePatientAllergy(request);
        var createdItems = UsingDbContext(ctx => ctx.PatientAllergies.FirstOrDefault(x => x.PatientId == patientId));
        var editRequest = new EditPatientAllergyCommandRequest
        {
            Id = createdItems.Id,
            AllergyType = "AllergyType2",
            Reaction = "Reaction2",
            Notes = "Notes2",
            Severity = Severity.Severe,
            AllergySnomedId = 4345,
            ReactionSnomedId = 90973
        };
        //Act
        await _patientProfileAppService.EditPatientAllergy(editRequest);
        //Assert
        PatientAllergy patientAllergy = UsingDbContext(x=>x.PatientAllergies.FirstOrDefault(x=>x.Id == editRequest.Id));
        patientAllergy.ShouldNotBeNull();
        patientAllergy.Severity.ShouldBe(editRequest.Severity);
        patientAllergy.AllergyType.ShouldBe(editRequest.AllergyType);
        patientAllergy.Reaction.ShouldBe(editRequest.Reaction);
        patientAllergy.Notes.ShouldBe(editRequest.Notes);
    }

    [MultiTenantFact]
    public async Task Save_Menstruation_And_Frequency_Should_Save_Menstruation_And_Frequency()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = new SaveMenstruationAndFrequencyCommand
        {
            PatientId = patientId,
            Notes = "Notes",
            RequestedTest = "Test",
            AverageCycleLength = 1,
            AverageCycleLengthUnit = UnitOfTime.Month,
            AveragePeriodDuration = 1,
            IsPeriodPredictable = true,
            LastDayOfPeriod = DateTime.Today.AddDays(6)
        };
        // Act
       var result = await _patientProfileAppService.SaveMenstruationAndFrequency(request);
        
        // Assert
        result.Id.ShouldNotBeNull();
        result.Id.GetValueOrDefault().ShouldBeGreaterThan(0);
    }

    [MultiTenantFact]
    public async Task SaveMenstrualBloodFlow_Should_Save_Menstrual_Blood_Flow()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = new SaveMenstrualBloodFlowCommandRequest
        {
            PatientId = patientId,
            SanitaryPadUsedPerDay = 2,
            FlowType = MenstrualFlowType.Regular,
            IsPeriodHeavierThanUsual = true,
            IsFlowFloodThroughSanitaryTowel = true,
            IsHeavyPeriodImpactDayToDayLife = true,
            IsBloodClotLargerThanRegular = true
        };
        // Act
        var result = await _patientProfileAppService.SaveMenstrualBloodFlow(request);
        // Assert
        result.Id.ShouldNotBeNull();
        result.Id.GetValueOrDefault().ShouldBeGreaterThan(0);
    }

    [MultiTenantFact]
    public async Task SavePatientGenotypeAndBloodGroup_Should_Save_Patient_Genotype_And_BloodGroup()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = new UpdatePatientGenotypeAndBloodGroupCommandRequest
        {
            PatientId = patientId,
            GenotypeSource = BloodGroupAndGenotypeSource.ClinicalInvestigation,
            BloodGroupSource = BloodGroupAndGenotypeSource.SelfReport,
            BloodGroup = BloodGroup.A_Negative,
            BloodGenotype = BloodGenotype.AA
        };
        
        // Act
        await _patientProfileAppService.SavePatientGenotypeAndBloodGroup(request);
        
        // Assert
        Patient patient = null;
        UsingDbContext(ctx =>
        {
            patient = ctx.Patients.FirstOrDefault(p => p.Id == patientId);
        });
        patient.ShouldNotBeNull();
        patient.BloodGroup.ShouldBe(request.BloodGroup);
        patient.BloodGenotype.ShouldBe(request.BloodGenotype);
        patient.GenotypeSource.ShouldBe(request.GenotypeSource);
        patient.BloodGroupSource.ShouldBe(request.BloodGroupSource);

    }
    [MultiTenantFact]
    public async Task DeletePatientMenstrualFlow_Should_Delete_Patient_Menstrual_Flow()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = new SaveMenstrualBloodFlowCommandRequest
        {
            PatientId = patientId,
            SanitaryPadUsedPerDay = 2,
            FlowType = MenstrualFlowType.Regular,
            IsPeriodHeavierThanUsual = true,
            IsFlowFloodThroughSanitaryTowel = true,
            IsHeavyPeriodImpactDayToDayLife = true,
            IsBloodClotLargerThanRegular = true
        };
        var result = await _patientProfileAppService.SaveMenstrualBloodFlow(request);
        long flowId = result.Id.GetValueOrDefault();
        // Act
        await _patientProfileAppService.DeleteMenstrualFlow(flowId);

        // Assert
        Patient patient = UsingDbContext(ctx => ctx.Patients.FirstOrDefault(p => p.Id == patientId && !p.IsDeleted));
        patient.ShouldNotBeNull();
    }

    [MultiTenantFact]
    public async Task SavePatientPastMedicalHistory_Should_Save_Patient_Past_Medical_History()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = GetPatientPastMedicalConditionCommandRequest(patientId);
        // Act
        await _patientProfileAppService.SavePatientPastMedicalHistory(request);
        
        // Assert
        PatientPastMedicalCondition patient = null;
        UsingDbContext(ctx =>
        {
            patient = ctx.PatientPastMedicalConditions.Include(x=>x.Medications).FirstOrDefault(p => p.Id == patientId);
        });
        patient.ShouldNotBeNull();
        patient.PatientId.ShouldBe(patientId);
        patient.ChronicCondition.ShouldBe(request.ChronicCondition);
        patient.SnomedId.ShouldBe(request.SnomedId);
        patient.DiagnosisPeriod.ShouldBe(request.DiagnosisPeriod);
        patient.PeriodUnit.ShouldBe(request.PeriodUnit);
        patient.Control.ShouldBe(request.Control);
        patient.IsOnMedication.ShouldBe(request.IsOnMedication);
        patient.Notes.ShouldBe(request.Notes);
        patient.NumberOfPreviousInfarctions.ShouldBe(request.NumberOfPreviousInfarctions);
        patient.IsHistoryOfAngina.ShouldBe(request.IsHistoryOfAngina);
        patient.IsPreviousHistoryOfAngina.ShouldBe(request.IsPreviousHistoryOfAngina);
        patient.IsPreviousOfAngiogram.ShouldBe(request.IsPreviousOfAngiogram);
        patient.IsPreviousOfStenting.ShouldBe(request.IsPreviousOfStenting);
        patient.IsPreviousOfMultipleInfarction.ShouldBe(request.IsPreviousOfMultipleInfarction);
        patient.IsStillIll.ShouldBe(request.IsStillIll);
        patient.IsPrimaryTemplate.ShouldBe(request.IsPrimaryTemplate);
        patient.Medications.Count.ShouldBe(1);

    }
    [MultiTenantFact]
    public async Task DeletePatientChronicConditions_Should_Delete_Patient_Past_Medical_History()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = new PatientPastMedicalCondition
        {
            PatientId = patientId,
            Control = ConditionControl.WellControlled,
            Notes = "notes",
            ChronicCondition = "Hypertension",
            DiagnosisPeriod = 10,
            PeriodUnit = UnitOfTime.Month,
            NumberOfPreviousInfarctions = 10,
            IsHistoryOfAngina = true,
            IsPreviousHistoryOfAngina = true,
            IsPreviousOfAngiogram = true,
            IsPreviousOfStenting = true,
            IsPreviousOfMultipleInfarction = true,
            IsStillIll = true,
            IsPrimaryTemplate = true,
            Medications = new List<PatientPastMedicalConditionMedication>
            {
                new()
                {
                    MedicationType = "Metformin",
                    MedicationDose = "Metformin",
                    PrescriptionFrequency = 10,
                    FrequencyUnit = UnitOfTime.Month.ToString(),
                    IsCompliantWithMedication = true,
                    MedicationUsageFrequency = 10,
                    MedicationUsageFrequencyUnit = UnitOfTime.Month.ToString(),
                }
            }
        };
        UsingDbContext(ctx =>
        {
            ctx.PatientPastMedicalConditions.Add(request);
            ctx.SaveChanges();
        });
        // Act
        await _patientProfileAppService.DeletePatientChronicConditions(request.Id);
        
        // Assert
        PatientPastMedicalCondition pastMedicalCondition = null;
        UsingDbContext(ctx =>
        {
            pastMedicalCondition = ctx.PatientPastMedicalConditions.FirstOrDefault(p => p.Id == request.Id && !p.IsDeleted);
        });
        pastMedicalCondition.ShouldBeNull();
        
    }
    [MultiTenantFact]
    public async Task SavePatientFamilyHistory_Should_Save_Patient_Family_History()
    {
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = GetPatientFamilyHistoryCommandRequest(patientId);
        // Act
       var result = await _patientProfileAppService.SavePatientFamilyHistory(request);
       var savedFamilyHistory = UsingDbContext(x => x.PatientFamilyHistories
           .Include(m=>m.PatientFamilyMembers)
           .FirstOrDefault(p => p.Id == result.Id));

        // Assert
        savedFamilyHistory.ShouldNotBeNull();
        savedFamilyHistory.PatientId.ShouldBe(patientId);
        savedFamilyHistory.IsFamilyHistoryKnown.ShouldBe(request.IsFamilyHistoryKnown);
        savedFamilyHistory.TotalNumberOfSiblings.ShouldBe(request.TotalNumberOfSiblings);
        savedFamilyHistory.TotalNumberOfMaleSiblings.ShouldBe(request.TotalNumberOfMaleSiblings);
        savedFamilyHistory.TotalNumberOfFemaleSiblings.ShouldBe(request.TotalNumberOfFemaleSiblings);
        savedFamilyHistory.TotalNumberOfChildren.ShouldBe(request.TotalNumberOfChildren);
        savedFamilyHistory.TotalNumberOfMaleChildren.ShouldBe(request.TotalNumberOfMaleChildren);
        savedFamilyHistory.TotalNumberOfFemaleChildren.ShouldBe(request.TotalNumberOfFemaleChildren);
        savedFamilyHistory.PatientFamilyMembers.Count.ShouldBe(1);
    }

    [MultiTenantFact]
    public async Task SavePatientFamilyHistory_Should_Update_Patient_Family_History()
    {
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var request = GetPatientFamilyHistoryCommandRequest(patientId);
        var result = await _patientProfileAppService.SavePatientFamilyHistory(request);
        var savedFamilyHistory = UsingDbContext(x => x.PatientFamilyHistories
            .Include(m=>m.PatientFamilyMembers)
            .FirstOrDefault(p => p.Id == result.Id));
        var saveDto = MapToDto(savedFamilyHistory);
        saveDto.IsFamilyHistoryKnown = false;
        saveDto.TotalNumberOfSiblings = 2;
        saveDto.FamilyMembers.Add(new PatientFamilyMembersDto
        {
            Relationship = Relationship.Father,
            IsAlive = false,
            CausesOfDeath = "cause 2" ,
            AgeAtDeath = 10,
            AgeAtDiagnosis = 10,
            Id = 1
        });
        // Act
        await _patientProfileAppService.SavePatientFamilyHistory(saveDto);
        var updatedFamilyHistory = UsingDbContext(x => x.PatientFamilyHistories
            .Include(m=>m.PatientFamilyMembers)
            .FirstOrDefault(p => p.Id == result.Id));
        // Assert
        updatedFamilyHistory.ShouldNotBeNull();
        updatedFamilyHistory.PatientId.ShouldBe(patientId);
        updatedFamilyHistory.IsFamilyHistoryKnown.ShouldBe(saveDto.IsFamilyHistoryKnown);
        updatedFamilyHistory.TotalNumberOfSiblings.ShouldBe(saveDto.TotalNumberOfSiblings);
        updatedFamilyHistory.TotalNumberOfMaleSiblings.ShouldBe(saveDto.TotalNumberOfMaleSiblings);
        updatedFamilyHistory.TotalNumberOfFemaleSiblings.ShouldBe(saveDto.TotalNumberOfFemaleSiblings);
        updatedFamilyHistory.TotalNumberOfChildren.ShouldBe(saveDto.TotalNumberOfChildren);
        updatedFamilyHistory.TotalNumberOfMaleChildren.ShouldBe(saveDto.TotalNumberOfMaleChildren);
        updatedFamilyHistory.TotalNumberOfFemaleChildren.ShouldBe(saveDto.TotalNumberOfFemaleChildren);
        updatedFamilyHistory.PatientFamilyMembers.Count.ShouldBe(1);

    }
    [MultiTenantFact]
    public async Task SavePatientTravelHistory_Should_Save_Patient_Travel_History()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
        });
        var histories = new List<CreatePatientTravelHistoryCommand>() 
        {
             new()
             {
                PatientId = patientId,
                CountryId = 1,
                City = "Test City",
                Date = DateTime.Now,
                Duration = 1
             }
        };

       
        
        // Act
        await _patientProfileAppService.SavePatientTravelHistory(histories);
        
        // Assert
        PatientTravelHistory patientTravelHistory = null;
        UsingDbContext(ctx =>
        {
            patientTravelHistory = ctx.PatientTravelHistories.FirstOrDefault(p => p.PatientId == patientId);
        });
        patientTravelHistory.ShouldNotBeNull();
        patientTravelHistory.CountryId.ShouldBe(histories[0].CountryId);
        patientTravelHistory.City.ShouldBe(histories[0].City);
        patientTravelHistory.Date.ShouldBe(histories[0].Date);
        patientTravelHistory.Duration.ShouldBe(histories[0].Duration);
        
    }

    [MultiTenantFact]
    public async Task DeletePatientMajorInjury_Should_Delete_Patient_Major_Injury()
    {
        // Arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
            var patientInjury = GetPatientMajorInjury(patientId, tenantId);
            context.PatientMajorInjuries.Add(patientInjury);
            context.SaveChanges();
        });
        // Act
        await _patientProfileAppService.DeletePatientMajorInjury(patientId);
        
        // Assert
        var patientInjury = await _patientProfileAppService.GetPatientMajorInjury(patientId);
        patientInjury.Count.ShouldBe(0);
    }

    private static PatientMajorInjury GetPatientMajorInjury(long patientId, int tenantId)
    {
        var patientInjury = new PatientMajorInjury
        {
            PatientId = patientId,
            Notes = "test",
            Id = 1,
            Diagnosis = "test",
            TenantId = tenantId,
            IsOngoing = true,
            IsComplicationPresent = true,
            PeriodOfInjury = 2,
            PeriodOfInjuryUnit = UnitOfTime.Month,
        };
        return patientInjury;
    }

    private CreatePatientAllergyCommandRequest GetCreatePatientAllergyCommandRequest(long patientId)
    {
        return new CreatePatientAllergyCommandRequest
        {
            PatientId = patientId,
            AllergyType = "AllergyType",
            Reaction = "Reaction",
            Notes = "Notes",
            Severity = Severity.Mild,
            AllergySnomedId = 1234,
            ReactionSnomedId = 4567
        };
    }
    private  Patient GetPatient()
    {
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Joe",
            PhoneNumber = "08060421709",
            EmailAddress = "test@example.com",
            DateOfBirth = DateTime.Now,
            GenderType = GenderType.Male,
            Title = TitleType.Mr
        };
        return patient;
    }
    private static PatientPastMedicalConditionCommandRequest GetPatientPastMedicalConditionCommandRequest(long patientId)
    {

        var request = new PatientPastMedicalConditionCommandRequest
        {
            PatientId = patientId,
            Control = ConditionControl.WellControlled,
            Notes = "notes",
            ChronicCondition = "Hypertension",
            DiagnosisPeriod = 10,
            PeriodUnit = UnitOfTime.Month,
            NumberOfPreviousInfarctions = 10,
            IsHistoryOfAngina = true,
            IsPreviousHistoryOfAngina = true,
            IsPreviousOfAngiogram = true,
            IsPreviousOfStenting = true,
            IsPreviousOfMultipleInfarction = true,
            IsStillIll = true,
            IsPrimaryTemplate = true,
            Medications = new List<PatientPastMedicalConditionMedicationRequest>
            {
                new()
                {
                    MedicationType = "Metformin",
                    MedicationDose = "Metformin",
                    PrescriptionFrequency = 10,
                    FrequencyUnit = UnitOfTime.Month.ToString(),
                    IsCompliantWithMedication = true,
                    MedicationUsageFrequency = 10,
                    MedicationUsageFrequencyUnit = UnitOfTime.Month.ToString()
                }
            }
        };
        return request;
    }
    private static PatientFamilyHistoryDto GetPatientFamilyHistoryCommandRequest(long patientId)
    {
        return new PatientFamilyHistoryDto
        {
            PatientId = patientId,
            IsFamilyHistoryKnown = true,
            TotalNumberOfSiblings = 1,
            TotalNumberOfMaleSiblings = 1,
            TotalNumberOfFemaleSiblings = 1,
            TotalNumberOfChildren = 1,
            TotalNumberOfMaleChildren = 1,
            TotalNumberOfFemaleChildren = 1,
            FamilyMembers = new List<PatientFamilyMembersDto>()
            {
                new()
                {
                    Relationship = Relationship.Aunt,
                    IsAlive = true,
                    AgeAtDeath = 1,
                    CausesOfDeath = "Cause of death",
                    AgeAtDiagnosis = 1,
                    Id = 1,
                }
            }
        };
    }
    private  PatientFamilyHistoryDto MapToDto(PatientFamilyHistory patientFamilyHistory)
    {
        var patientFamilyHistoryDto = _objectMapper.Map<PatientFamilyHistoryDto>(patientFamilyHistory);
        patientFamilyHistoryDto.FamilyMembers = _objectMapper.Map<List<PatientFamilyMembersDto>>(patientFamilyHistory.PatientFamilyMembers);
        return patientFamilyHistoryDto;
    }

}
