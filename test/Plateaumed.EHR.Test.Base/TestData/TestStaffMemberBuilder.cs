using System.Linq;
using Bogus;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestStaffMemberBuilder
{
    private StaffMember _staffMember;

    private TestStaffMemberBuilder()
    {
        SetDefaults();
    }

    public static TestStaffMemberBuilder Create()
    {
        return new TestStaffMemberBuilder();
    }

    public TestStaffMemberBuilder WithUser(User user)
    {
        _staffMember.UserFk = user;
        return this;
    }

    public TestStaffMemberBuilder WithFacility(Facility facility, bool isDefault = false)
    {
        _staffMember.AssignedFacilities.Add(new FacilityStaff
        {
            FacilityFk = facility,
            FacilityId = facility.Id,
            StaffMemberFk = _staffMember,
            IsDefault = isDefault
        });

        return this;
    }

    public TestStaffMemberBuilder WithJob(params Job[] jobs)
    {
        jobs.ToList().ForEach(_staffMember.Jobs.Add);
        return this;
    }

    public TestStaffMemberBuilder WithAdminRole(Role role)
    {
        _staffMember.AdminRole = role;
        return this;
    }

    public StaffMember Build()
    {
        return _staffMember;
    }

    public StaffMember Save(EHRDbContext context)
    {
        var entity = context.StaffMembers.Add(Build()).Entity;
        context.SaveChanges();
        return entity;
    }

    private void SetDefaults()
    {
        var faker = new Faker<StaffMember>();
        faker.RuleFor(x => x.StaffCode, f => f.Random.AlphaNumeric(10));
        faker.RuleFor(x => x.ContractStartDate, f => f.Date.Past());
        faker.RuleFor(x => x.ContractEndDate, f => f.Date.Future());
        _staffMember = faker.Generate();
    }
}