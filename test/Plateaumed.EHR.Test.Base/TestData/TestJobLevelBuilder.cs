using Bogus;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestJobLevelBuilder
{
    private JobLevel _jobLevel;

    private TestJobLevelBuilder(int tenantId)
    {
        SetDefaults();
        _jobLevel.TenantId = tenantId;
    }

    public static TestJobLevelBuilder Create(int tenantId)
    {
        return new TestJobLevelBuilder(tenantId);
    }

    public TestJobLevelBuilder WithId(long id)
    {
        _jobLevel.Id = id;
        return this;
    }

    public TestJobLevelBuilder WithNames(string name, string shortName)
    {
        _jobLevel.Name = name;
        _jobLevel.ShortName = shortName;
        return this;
    }

    public TestJobLevelBuilder WithJobTitle(JobTitle jobTitle)
    {
        _jobLevel.JobTitleFk = jobTitle;
        _jobLevel.JobTitleId = jobTitle.Id;
        return this;
    }

    public JobLevel Build()
    {
        return _jobLevel;
    }

    public JobLevel Save(EHRDbContext context)
    {
        var jobLevel = context.JobLevels.Add(Build()).Entity;
        context.SaveChanges();
        return jobLevel;
    }

    private void SetDefaults()
    {
        var faker = new Faker<JobLevel>();
        faker.RuleFor(x => x.Name, f => f.Name.JobTitle());
        faker.RuleFor(x => x.ShortName, f => f.Name.JobTitle());
        _jobLevel = faker.Generate();
    }
}