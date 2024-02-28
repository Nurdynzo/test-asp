using Bogus;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestJobTitleBuilder
{
    private JobTitle _jobTitle;

    private TestJobTitleBuilder(int tenantId)
    {
        SetDefaults();
        _jobTitle.TenantId = tenantId;
    }

    public static TestJobTitleBuilder Create(int tenantId)
    {
        return new TestJobTitleBuilder(tenantId);
    }

    public TestJobTitleBuilder WithId(long id)
    {
        _jobTitle.Id = id;
        return this;
    }

    public TestJobTitleBuilder WithNames(string name, string shortName)
    {
        _jobTitle.Name = name;
        _jobTitle.ShortName = shortName;
        return this;
    }

    public TestJobTitleBuilder WithFacilityId(long facilityId)
    {
        _jobTitle.FacilityId = facilityId;
        return this;
    }

    public JobTitle Build()
    {
        return _jobTitle;
    }

    public JobTitle Save(EHRDbContext context)
    {
        var jobTitle = context.JobTitles.Add(Build()).Entity;
        context.SaveChanges();
        return jobTitle;
    }

    private void SetDefaults()
    {
        var faker = new Faker<JobTitle>();
        faker.RuleFor(x => x.Name, f => f.Name.JobTitle());
        faker.RuleFor(x => x.ShortName, f => f.Name.JobTitle());
        _jobTitle = faker.Generate();
    }
}