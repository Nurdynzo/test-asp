using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Bogus;
using MockQueryable.NSubstitute;
using NSubstitute;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Handlers;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.PhysicalExaminations;

public class GetPhysicalExaminationQueryHandlerTests
{
    private readonly IObjectMapper _objectMapper = AutoMapperTestHelpers.CreateRealObjectMapper();

    [Fact]
    public async Task Handle_GivenType_ShouldFetchQualifiers()
    {
        // Arrange
        const long examId = 69;

        var physicalExamination = CreatePhysicalExamination("Scar");

        var examRepository = Substitute.For<IRepository<PhysicalExamination, long>>();
        examRepository.GetAsync(examId).Returns(Task.FromResult(physicalExamination));

        var qualifierRepository = Substitute.For<IRepository<ExaminationQualifier, long>>();
        qualifierRepository.GetAll().Returns(GetQualifiers());

        var handler = new GetPhysicalExaminationQueryHandler(examRepository, qualifierRepository, _objectMapper);
        // Act
        var response = await handler.Handle(examId);
        // Assert
        response.Qualifiers.Count.ShouldBe(3);
    }

    private IQueryable<ExaminationQualifier> GetQualifiers()
    {
        return new List<ExaminationQualifier>
        {
            CreateExaminationQualifier("Scar"),
            CreateExaminationQualifier("Scar"),
            CreateExaminationQualifier("Bleeding"),
            CreateExaminationQualifier("Scar"),
            CreateExaminationQualifier("Bleeding"),
            CreateExaminationQualifier("Cyanosis"),
        }.AsQueryable().BuildMock();
    }

    private ExaminationQualifier CreateExaminationQualifier(string subQualifier)
    {
        var faker = new Faker<ExaminationQualifier>();
        faker.RuleFor(x => x.Id, f => f.Random.Long());
        faker.RuleFor(x => x.SubQualifier, f => subQualifier);
        faker.RuleFor(x => x.SnomedId, x => x.UniqueIndex.ToString());
        faker.RuleFor(x => x.SubDivision, x => x.Lorem.Word());
        faker.RuleFor(x => x.Qualifier, x => x.Lorem.Word());

        return faker.Generate();
    }

    private PhysicalExamination CreatePhysicalExamination(string qualifier)
    {
        var faker = new Faker<PhysicalExamination>();
        faker.RuleFor(x => x.Qualifiers, f => qualifier);
        faker.RuleFor(x => x.Id, f => f.Random.Long());
        faker.RuleFor(x => x.Type, f => f.Lorem.Word());
        faker.RuleFor(x => x.Header, f => f.Lorem.Word());
        faker.RuleFor(x => x.PresentTerms, f => f.Lorem.Word());
        faker.RuleFor(x => x.AbsentTerms, f => f.Lorem.Word());
        faker.RuleFor(x => x.SnomedId, x => x.UniqueIndex.ToString());
        faker.RuleFor(x => x.AbsentTermsSnomedId, x => x.UniqueIndex.ToString());
        return faker.Generate();
    }
}