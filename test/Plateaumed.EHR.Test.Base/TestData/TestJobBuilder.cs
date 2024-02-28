using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestJobBuilder
{
    private readonly Job _job = new ();
    private readonly int _tenantId;

    private TestJobBuilder(int tenantId)
    {
        _tenantId = tenantId;
        _job.TenantId = tenantId;
    }

    public static TestJobBuilder Create(int tenantId)
    {
        return new TestJobBuilder(tenantId);
    }

    public TestJobBuilder WithId(long id)
    {
        _job.Id = id;
        return this;
    }
    
    public TestJobBuilder AsPrimary()  {
        _job.IsPrimary = true;
        return this;
    }

    public TestJobBuilder WithJobLevel(JobLevel jobLevel)
    {
        _job.JobLevel = jobLevel;
        _job.JobLevelId = jobLevel.Id;
        return this;
    }

    public TestJobBuilder WithStaffMember(StaffMember staffMember)
    {
        _job.StaffMember = staffMember;
        return this;
    }

    public TestJobBuilder WithJobRole(Role role)
    {
        _job.JobRole = role;
        return this;
    }

    public TestJobBuilder WithTeamRole(Role role)
    {
        _job.TeamRole = role;
        return this;
    }

    public TestJobBuilder WithWard(Ward ward)
    {
        _job.WardsJobs.Add(new WardJob
        {
            TenantId = _tenantId,
            Ward = ward,
            Job = _job
        });
        return this;
    }

    public TestJobBuilder WithUnit(OrganizationUnitExtended unit)
    {
        _job.Unit = unit;
        return this;
    }

    public TestJobBuilder WithDepartment(OrganizationUnitExtended department)
    {
        _job.Department = department;
        return this;
    }

    public TestJobBuilder WithServiceCentre(ServiceCentreType serviceCentreType)
    {
        _job.JobServiceCentres.Add(new JobServiceCentre{Job = _job, ServiceCentre = serviceCentreType});
        return this;
    }

    public Job Build()
    {
        return _job;
    }

    public Job Save(EHRDbContext context)
    {
        var job = context.Jobs.Add(Build()).Entity;
        context.SaveChanges();
        return job;
    }
}