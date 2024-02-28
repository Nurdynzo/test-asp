using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Bogus;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.PhysicalExaminations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PhysicalExaminations;

public class GetPhysicalExaminationListQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task Handle_GivenHeader_ShouldReturnExams()
    {
        // Arrange
        var request = new GetPhysicalExaminationListRequest { Header = "Ears" };

        var data = GetData();
        var repository = Substitute.For<IRepository<PhysicalExamination, long>>();
        repository.GetAll().Returns(data);

        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        patientRepository.GetAsync(request.PatientId).Returns(Task.FromResult(new Patient()));

        var handler = new GetPhysicalExaminationListQueryHandler(repository, patientRepository, _objectMapper);
        // Act
        var response = await handler.Handle(request);
        // Assert
        response.Count.ShouldBe(2);
        response[0].Id.ShouldBe(data.ToList()[3].Id);
        response[1].Id.ShouldBe(data.ToList()[4].Id);
    }

    [Fact]
    public async Task Handle_GivenGenderRequirement_ShouldFilterExams()
    {
        // Arrange
        var request = new GetPhysicalExaminationListRequest { Header = "Head" };

        var data = GetData();
        var examRepository = Substitute.For<IRepository<PhysicalExamination, long>>();
        examRepository.GetAll().Returns(data);
        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        patientRepository.GetAsync(request.PatientId).Returns(Task.FromResult(new Patient { GenderType = GenderType.Female }));

        var handler = new GetPhysicalExaminationListQueryHandler(examRepository, patientRepository, _objectMapper);
        // Act
        var response = await handler.Handle(request);
        // Assert
        response.Count.ShouldBe(2);
        response[0].Id.ShouldBe(data.ToList()[0].Id);
        response[1].Id.ShouldBe(data.ToList()[2].Id);
    }

    [Theory]
    [InlineData(false, false, "", false)]
    [InlineData(true, false, "", true)]
    [InlineData(false, true, "", true)]
    [InlineData(false, false, "abc", true)]
    public async Task Handle_GivenQualifiersPlaneAndSite_ShouldSetHasQualifiers(bool plane, bool site, string qualifiers, bool hasQualifiers)
    {
        // Arrange
        var request = new GetPhysicalExaminationListRequest { Header = "Ears" };

        var exam = CreatePhysicalExamination("General", "Ears");
        exam.Plane = plane;
        exam.Site = site;
        exam.Qualifiers = qualifiers;

        var repository = Substitute.For<IRepository<PhysicalExamination, long>>();
        repository.GetAll().Returns(new List<PhysicalExamination>{exam}.AsQueryable().BuildMock());

        var patientRepository = Substitute.For<IRepository<Patient, long>>();
        patientRepository.GetAsync(request.PatientId).Returns(Task.FromResult(new Patient()));

        var handler = new GetPhysicalExaminationListQueryHandler(repository, patientRepository, _objectMapper);
        // Act
        var response = await handler.Handle(request);
        // Assert
        response.Count.ShouldBe(1);
        response[0].HasQualifiers.ShouldBe(hasQualifiers);
    }

    private IQueryable<PhysicalExamination> GetData()
    {
        return new List<PhysicalExamination>()
        {
            CreatePhysicalExamination("General", "Head", GenderType.Female),
            CreatePhysicalExamination("General", "Head", GenderType.Male),
            CreatePhysicalExamination("General", "Head"),
            CreatePhysicalExamination("General", "Ears"),
            CreatePhysicalExamination("General", "Ears"),
            CreatePhysicalExamination("Cardiology", "Pulse"),
            CreatePhysicalExamination("Cardiology", "Pulse")
        }.AsQueryable().BuildMock();
    }

    private PhysicalExamination CreatePhysicalExamination(string type, string headers, GenderType? gender = null)
    {
        var faker = new Faker<PhysicalExamination>();
        faker.RuleFor(x => x.Type, f => type);
        faker.RuleFor(x => x.Header, f => headers);
        faker.RuleFor(x => x.Id, f => f.Random.Long());
        faker.RuleFor(x => x.PresentTerms, f => f.Lorem.Word());
        faker.RuleFor(x => x.AbsentTerms, f => f.Lorem.Word());
        faker.RuleFor(x => x.SnomedId, x => x.UniqueIndex.ToString());
        faker.RuleFor(x => x.AbsentTermsSnomedId, x => x.UniqueIndex.ToString());
        faker.RuleFor(x => x.Gender, x => gender);
        return faker.Generate();
    }
}