using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using NSubstitute;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.PhysicalExaminations.Handlers;
using Plateaumed.EHR.VitalSigns;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PhysicalExaminations;

[Trait("Category", "Unit")]
public class CreatePatientPhysicalExaminationCommandHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();
    private readonly IUnitOfWorkManager _unitOfWork = Substitute.For<IUnitOfWorkManager>();
    
    [Fact]
    public async Task Handle_GivenCorrect_Request_TypeNotes()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientPhysicalExamination, long>>(); 
        
        var testCreateData = CreateTest_RequestData();
        testCreateData.PhysicalExaminationEntryType = "TypeNote";
        
        var handler = new CreatePatientPhysicalExaminationCommandHandler(repository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());
        
        // Act
        PatientPhysicalExamination patientPhysicalExamination = null;
        await repository.InsertAsync(Arg.Do<PatientPhysicalExamination>(j => patientPhysicalExamination = j));
        
        await handler.Handle(testCreateData);
        
        // Assert
        patientPhysicalExamination.PhysicalExaminationEntryType.ToString().ShouldBe(testCreateData.PhysicalExaminationEntryType);
        patientPhysicalExamination.PatientId.ShouldBe(testCreateData.PatientId);
        patientPhysicalExamination.PhysicalExaminationTypeId.ShouldBe(testCreateData.PhysicalExaminationTypeId);
        patientPhysicalExamination.OtherNote.ShouldBe(testCreateData.OtherNote);
        patientPhysicalExamination.TypeNotes.First().Type.ShouldBe(testCreateData.TypeNotes.First().Type);
        patientPhysicalExamination.TypeNotes.First().Note.ShouldBe(testCreateData.TypeNotes.First().Note);
    }
    
    [Fact]
    public async Task Handle_GivenCorrect_Request_Suggestion()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientPhysicalExamination, long>>(); 
        
        var testCreateData = CreateTest_RequestData();  
        testCreateData.PhysicalExaminationEntryType = "Suggestion";
        
        var handler = new CreatePatientPhysicalExaminationCommandHandler(repository, _unitOfWork, _objectMapper, Substitute.For<IEncounterManager>());
        
        // Act
        PatientPhysicalExamination patientPhysicalExamination = null;
        await repository.InsertAsync(Arg.Do<PatientPhysicalExamination>(j => patientPhysicalExamination = j));
        
        await handler.Handle(testCreateData);

        // Assert
        patientPhysicalExamination.PhysicalExaminationEntryType.ToString().ShouldBe(testCreateData.PhysicalExaminationEntryType);
        patientPhysicalExamination.PatientId.ShouldBe(testCreateData.PatientId);
        patientPhysicalExamination.PhysicalExaminationTypeId.ShouldBe(testCreateData.PhysicalExaminationTypeId);
        patientPhysicalExamination.OtherNote.ShouldBe(testCreateData.OtherNote);
        patientPhysicalExamination.TypeNotes.First().Type.ShouldBe(testCreateData.TypeNotes.First().Type);
        patientPhysicalExamination.TypeNotes.First().Note.ShouldBe(testCreateData.TypeNotes.First().Note);
    }

    [Fact]
    public async Task Handle_ShouldCheckEncounterExistsAndSave()
    {
        // Arrange
        var repository = Substitute.For<IRepository<PatientPhysicalExamination, long>>();

        var request = new CreatePatientPhysicalExaminationDto { EncounterId = 6,  PhysicalExaminationEntryType = "Suggestion"};

        var encounterManager = Substitute.For<IEncounterManager>();
        var handler = new CreatePatientPhysicalExaminationCommandHandler(repository, _unitOfWork, _objectMapper, encounterManager);

        // Act
        PatientPhysicalExamination patientPhysicalExamination = null;
        await repository.InsertAsync(Arg.Do<PatientPhysicalExamination>(j => patientPhysicalExamination = j));

        await handler.Handle(request);

        // Assert
        await encounterManager.Received(1).CheckEncounterExists(request.EncounterId);
        patientPhysicalExamination.EncounterId.ShouldBe(request.EncounterId);
    }

    private static CreatePatientPhysicalExaminationDto CreateTest_RequestData()
    {
        return new CreatePatientPhysicalExaminationDto
        {
            PatientId = 1,
            PhysicalExaminationTypeId = 1,
            OtherNote = "This is a test note",
            Suggestions = new List<PatientPhysicalExamSuggestionQuestionDto>()
            {
                new PatientPhysicalExamSuggestionQuestionDto()
                {
                    HeaderName = "Mouth",
                    PatientPhysicalExamSuggestionAnswers = new List<PatientPhysicalExamSuggestionAnswerDto>()
                    {
                        new PatientPhysicalExamSuggestionAnswerDto()
                        {
                            SnowmedId = "836471008",
                            Description = "Piercing in tongue",
                            IsAbsent = false
                        },
                        new PatientPhysicalExamSuggestionAnswerDto()
                        {
                            SnowmedId = "300249003",
                            Description = "Tongue normal",
                            IsAbsent = false
                        }
                    }
                }
            },
            TypeNotes = new List<PatientPhysicalExamTypeNoteRequestDto>()
            {
                new PatientPhysicalExamTypeNoteRequestDto()
                {
                    Type = "motorExam",
                    Note = "The big exams"
                },
                new PatientPhysicalExamTypeNoteRequestDto()
                {
                    Type = "memory",
                    Note = "Long memory consumption"
                },
                new PatientPhysicalExamTypeNoteRequestDto()
                {
                    Type = "speech",
                    Note = "Lovely speech and great voice"
                }
            }
        };
    }
}