using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Investigations.Handlers;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Investigations
{
    [Trait("Category", "Unit")]
    public class GetInvestigationQueryHandlerTests
    {
        private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

        [Fact]
        public async Task Handle_GivenInvestigationId_ShouldReturnInvestigation()
        {
            // Arrange
            var request = new GetInvestigationRequest { Id = 1, PatientId = 1};

            var handler = CreateHandler();
            // Act
            var result = await handler.Handle(request);
            // Assert
            result.ShouldNotBeNull();
            result.Components.ShouldNotBeNull();
            result.Components.Count.ShouldBe(1);
            result.Components[0].Ranges.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_GivenMicrobiologyInvestigation_ShouldMapStaticInvestigations()
        {
            //Arrange
            var request = new GetInvestigationRequest { Id = 4, PatientId = 1 };

            var handler = CreateHandler();
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.ShouldNotBeNull();
            result.Suggestions.Count(x => x.Category == SuggestionCategories.GramStain).ShouldBe(1);
            result.Suggestions.Count(x => x.Category == SuggestionCategories.BlueStain).ShouldBe(1);
            result.Suggestions.Count(x => x.Category == SuggestionCategories.Culture).ShouldBe(1);
            result.Suggestions.Count(x => x.Category == SuggestionCategories.AntibioticSensitivity).ShouldBe(2);
            result.Suggestions.Count(x => x.Category == SuggestionCategories.CommonMicrobiology).ShouldBe(1);
            result.NugentScore.ShouldBe(true);
        }

        [Fact]
        public async Task Handle_GivenMicrobiologyInvestigationWithMicroscopy_ShouldUnionCustomAndStaticSuggestions()
        {
            //Arrange
            var request = new GetInvestigationRequest { Id = 4, PatientId = 1 };

            var handler = CreateHandler();
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.ShouldNotBeNull();
            result.Suggestions.Count(x => x.Category == SuggestionCategories.Microscopy).ShouldBe(2);
        }

        [Fact]
        public async Task Handle_GivenMicrobiologyInvestigationWithMacroscopy_ShouldMap()
        {
            //Arrange
            var request = new GetInvestigationRequest { Id = 4, PatientId = 1 };

            var handler = CreateHandler();
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.ShouldNotBeNull();
            result.Suggestions.Count(x => x.Category == SuggestionCategories.Macroscopy).ShouldBe(1);
        }

        [Fact]
        public async Task Handle_GivenMicrobiologyInvestigationWithDipstick_ShouldMapDipstick()
        {
            //Arrange
            var request = new GetInvestigationRequest { Id = 4, PatientId = 1 };

            var handler = CreateHandler();
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.ShouldNotBeNull();
            result.Dipstick.Count.ShouldBe(1);
            result.Dipstick[0].Parameter.ShouldBe("Param A");
            result.Dipstick[0].Ranges.Count.ShouldBe(2);
            result.Dipstick[0].Ranges[0].Unit.ShouldBe("+");
            result.Dipstick[0].Ranges[0].Results.Count.ShouldBe(2);
            result.Dipstick[0].Ranges[0].Results[0].Result.ShouldBe("Neg");
            result.Dipstick[0].Ranges[0].Results[0].Order.ShouldBe(1);
            result.Dipstick[0].Ranges[0].Results[1].Result.ShouldBe("Trace");
            result.Dipstick[0].Ranges[0].Results[1].Order.ShouldBe(2);
            result.Dipstick[0].Ranges[1].Unit.ShouldBe("mg/dL");
            result.Dipstick[0].Ranges[1].Results.Count.ShouldBe(2);
            result.Dipstick[0].Ranges[1].Results[0].Result.ShouldBe("0");
            result.Dipstick[0].Ranges[1].Results[0].Order.ShouldBe(1);
            result.Dipstick[0].Ranges[1].Results[1].Result.ShouldBe("10");
            result.Dipstick[0].Ranges[1].Results[1].Order.ShouldBe(2);
        }

        [Theory]
        [InlineData(0, GenderType.Male, "0d - 1w male")]
        [InlineData(6, GenderType.Female, "0d - 1w female")]
        [InlineData(14, GenderType.Male, "2w - 2m male")]
        [InlineData(60, GenderType.Female, "2w - 2m female")]
        [InlineData(184, GenderType.Male, "6m - 1y male")]
        [InlineData(364, GenderType.Female, "6m - 1y female")]
        [InlineData(1830, GenderType.Male, "5y - 18y male")]
        [InlineData(6570, GenderType.Female, "5y - 18y female")]
        public async Task Handle_GivenPatientAgeAndGender_ShouldFilterRanges(int daysOld, GenderType gender, string unit)
        {
            //Arrange
            const long patientId = 55;
            var request = new GetInvestigationRequest { Id = 67, PatientId = patientId };

            var patientRepository = CreatePatientRepository(daysOld, gender, patientId);

            var handler = CreateHandler(patientRepository);
            //Act
            var result = await handler.Handle(request);
            //Assert
            result.ShouldNotBeNull();
            result.Ranges.Count.ShouldBe(1);
            result.Ranges[0].Unit.ShouldBe(unit);
        }

        [Fact]
        public async Task Handle_GivenPatientDoesNotExist_ShouldThrow()
        {
            //Arrange
            const long patientId = 55;
            var request = new GetInvestigationRequest { Id = 67, PatientId = patientId };

            var patientRepository = CreatePatientRepository(0, GenderType.Other, 1);

            var handler = CreateHandler(patientRepository);
            //Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(() => handler.Handle(request));
            //Assert
            exception.Message.ShouldBe("Patient not found");
        }

        private static IRepository<Patient, long> CreatePatientRepository(int daysOld, GenderType gender, long patientId)
        {
            var patient = new Patient
            {
                Id = patientId,
                GenderType = gender,
                DateOfBirth = DateTime.Now.AddDays(-daysOld),
            };
            var patientRepository = Substitute.For<IRepository<Patient, long>>();
            patientRepository.GetAsync(patientId).Returns(patient);
            return patientRepository;
        }

        private GetInvestigationQueryHandler CreateHandler(IRepository<Patient, long> patientRepository = null)
        {
            var repository = Substitute.For<IRepository<Investigation, long>>();
            repository.GetAll().Returns(GetData().BuildMock());

            var suggestionRepository = CreateSuggestionRepository();
            patientRepository ??= CreatePatientRepository(2000, GenderType.Male, 1);

            return new GetInvestigationQueryHandler(repository, _objectMapper, suggestionRepository, patientRepository);
        }

        private static IRepository<InvestigationSuggestion, long> CreateSuggestionRepository()
        {
            var staticSuggestions = StaticSuggestions();

            var repository = Substitute.For<IRepository<InvestigationSuggestion, long>>();
            repository.GetAll().Returns(staticSuggestions.BuildMock());

            return repository;
        }

        private static IQueryable<InvestigationSuggestion> StaticSuggestions()
        {
            return new List<InvestigationSuggestion>
            {
                new()
                {
                    Category = SuggestionCategories.Microscopy,
                    Result = "Tumors",
                    SnomedId = "128462008",
                },
                new()
                {
                    Category = SuggestionCategories.BlueStain,
                    Result = "Yeast",
                    SnomedId = "57894566",
                },
                new()
                {
                    Category = SuggestionCategories.GramStain,
                    Result = "Moderate",
                    SnomedId = "128499999",
                },
                new()
                {
                    Category = SuggestionCategories.Culture,
                    Result = "Heavy",
                    SnomedId = "54694566",
                },
                new()
                {
                    Category = SuggestionCategories.CommonMicrobiology,
                    Result = "Few",
                },
                new()
                {
                    Category = SuggestionCategories.AntibioticSensitivity,
                    Result = "Ampicillin",
                    SnomedId = "387170002",
                },
                new()
                {
                    Category = SuggestionCategories.AntibioticSensitivity,
                    Result = "Cefixime",
                    SnomedId = "387536009",
                },
            }.AsQueryable();
        }

        private IQueryable<Investigation> GetData()
        {
            return new List<Investigation>
            {
                new()
                {
                    Id = 1,
                    Type = InvestigationTypes.Chemistry,
                    Name = "Electrolytes, Urea & Creatinine",
                    ShortName = "E/U/Cr",
                    SnomedId = "444164000",
                    Synonyms = "Measurement of urea, sodium, potassium, chloride, bicarbonate and creatinine",
                    Specimen = "Serum",
                    Components = new List<Investigation>
                    {
                        new()
                        {
                            Id = 2,
                            Name = "Sodium",
                            SnomedId = "39972003",
                            Synonyms = null,
                            Ranges = new List<InvestigationRange>
                            {
                                new()
                                {
                                    Unit = "mmol/L",
                                    MinRange = 130,
                                    MaxRange = 146,
                                },
                                new()
                                {
                                    Unit = "mEq/L",
                                    MinRange = 130,
                                    MaxRange = 146,
                                },
                            },
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Name = "Sodium",
                    SnomedId = "39972003",
                    Synonyms = null,
                    Ranges = new List<InvestigationRange>
                    {
                        new()
                        {
                            Unit = "mmol/L",
                            MinRange = 130,
                            MaxRange = 146,
                        },
                        new()
                        {
                            Unit = "mEq/L",
                            MinRange = 130,
                            MaxRange = 146,
                        },
                    },
                },
                new()
                {
                    Id = 3,
                    Ranges = new List<InvestigationRange>
                    {
                        new()
                        {
                            Unit = "mmol/L",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 18,
                            AgeMaxUnit = UnitOfTime.Year,
                            AgeMin = 5,
                            AgeMinUnit = UnitOfTime.Year,
                        },
                        new()
                        {
                            Unit = "mmol/L",
                            MinRange = 3,
                            MaxRange = 4,
                            AgeMax = 18,
                            AgeMaxUnit = UnitOfTime.Year,
                            AgeMin = 5,
                            AgeMinUnit = UnitOfTime.Year,
                        },
                    }
                },
                new()
                {
                    Id = 4,
                    Type = InvestigationTypes.Microbiology,
                    Name = "Blood culture",
                    Specimen = "Serum",
                    SpecificOrganism = "Lactobacillus",
                    Suggestions = new List<InvestigationSuggestion>
                    {
                        new()
                        {
                            Result = "Bacteria",
                            SnomedId = "128462008",
                            Category = SuggestionCategories.Microscopy,
                        },
                        new()
                        {
                            Result = "Cell culture",
                            SnomedId = "745123556",
                            Category = SuggestionCategories.Macroscopy,
                        }
                    },
                    Dipstick = new List<DipstickInvestigation>
                    {
                        new()
                        {
                            Parameter = "Param A",
                            Ranges = new List<DipstickRange>
                            {
                                new()
                                {
                                    Unit = "+",
                                    Results = new List<DipstickResult>
                                    {
                                        new()
                                        {
                                            Order = 1,
                                            Result = "Neg",
                                        },
                                        new()
                                        {
                                            Order = 2,
                                            Result = "Trace",
                                        },
                                    }
                                },
                                new()
                                {
                                    Unit = "mg/dL",
                                    Results = new List<DipstickResult>
                                    {
                                        new()
                                        {
                                            Order = 1,
                                            Result = "0",
                                        },
                                        new()
                                        {
                                            Order = 2,
                                            Result = "10",
                                        },
                                    }
                                },
                            }
                        }
                    },
                    Microbiology = new MicrobiologyInvestigation
                    {
                        AntibioticSensitivityTest = true,
                        Culture = true,
                        GramStain = true,
                        Microscopy = true,
                        MethyleneBlueStain = true,
                        CommonResults = true,
                        NugentScore = true,
                    },
                    Components = new List<Investigation>
                    {
                        new()
                        {
                            Id = 2,
                            Name = "Sodium",
                            SnomedId = "39972003",
                            Synonyms = null,
                            Ranges = new List<InvestigationRange>
                            {
                                new()
                                {
                                    Unit = "mmol/L",
                                    MinRange = 130,
                                    MaxRange = 146,
                                },
                                new()
                                {
                                    Unit = "mEq/L",
                                    MinRange = 130,
                                    MaxRange = 146,
                                },
                            },
                        }
                    }
                },
                new()
                {
                    Id = 67,
                    Type = InvestigationTypes.Microbiology,
                    Name = "Range Check",
                    Ranges = new List<InvestigationRange>
                    {
                        new()
                        {
                            Unit = "5y - 18y male",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 18,
                            AgeMaxUnit = UnitOfTime.Year,
                            AgeMin = 5,
                            AgeMinUnit = UnitOfTime.Year,
                            Gender = GenderType.Male
                        },
                        new()
                        {
                            Unit = "6m - 1y male",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 1,
                            AgeMaxUnit = UnitOfTime.Year,
                            AgeMin = 6,
                            AgeMinUnit = UnitOfTime.Month,
                            Gender = GenderType.Male
                        },
                        new()
                        {
                            Unit = "2w - 2m male",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 2,
                            AgeMaxUnit = UnitOfTime.Month,
                            AgeMin = 2,
                            AgeMinUnit = UnitOfTime.Week,
                            Gender = GenderType.Male
                        },
                        new()
                        {
                            Unit = "0d - 1w male",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 1,
                            AgeMaxUnit = UnitOfTime.Week,
                            AgeMin = 0,
                            AgeMinUnit = UnitOfTime.Day,
                            Gender = GenderType.Male
                        },new()
                        {
                            Unit = "5y - 18y female",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 18,
                            AgeMaxUnit = UnitOfTime.Year,
                            AgeMin = 5,
                            AgeMinUnit = UnitOfTime.Year,
                            Gender = GenderType.Female
                        },
                        new()
                        {
                            Unit = "6m - 1y female",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 1,
                            AgeMaxUnit = UnitOfTime.Year,
                            AgeMin = 6,
                            AgeMinUnit = UnitOfTime.Month,
                            Gender = GenderType.Female
                        },
                        new()
                        {
                            Unit = "2w - 2m female",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 2,
                            AgeMaxUnit = UnitOfTime.Month,
                            AgeMin = 2,
                            AgeMinUnit = UnitOfTime.Week,
                            Gender = GenderType.Female
                        },
                        new()
                        {
                            Unit = "0d - 1w female",
                            MinRange = 1,
                            MaxRange = 2,
                            AgeMax = 1,
                            AgeMaxUnit = UnitOfTime.Week,
                            AgeMin = 0,
                            AgeMinUnit = UnitOfTime.Day,
                            Gender = GenderType.Female
                        },
                    }
                }
            }.AsQueryable();
        }
    }
}