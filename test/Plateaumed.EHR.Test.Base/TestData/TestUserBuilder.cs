using System.Collections.Generic;
using System.Linq;
using Abp.Authorization.Users;
using Bogus;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestUserBuilder
{
    private readonly List<int> _roleIds = new();
    private readonly User _user;
    private readonly List<long> _organizationUnits = new();
    private readonly int _tenantId;

    private TestUserBuilder(int tenantId)
    {
        _tenantId = tenantId;
        _user = CreateRandomUser();
        _user.TenantId = tenantId;
    }
    
    public static TestUserBuilder Create(int tenantId)
    {
        return new TestUserBuilder(tenantId);
    }

    public TestUserBuilder WithId(long userId)
    {
        _user.Id = userId;
        return this;
    }

    public TestUserBuilder WithNames(string name, string surname)
    {
        _user.Name = name;
        _user.Surname = surname;
        return this;
    }

    public TestUserBuilder WithOrgUnitId(params long[] unitId)
    {
        _organizationUnits.AddRange(unitId);
        return this;
    }

    public TestUserBuilder WithRoleId(int roleId)
    {
        _roleIds.Add(roleId);
        return this;
    }

    public TestUserBuilder WithStaffMember(StaffMember staffMember)
    {
        _user.StaffMemberFk = staffMember;
        return this;
    }

    public User Build()
    {
        _user.SetNormalizedNames();
        _user.OrganizationUnits = _organizationUnits.Select(unitId => new UserOrganizationUnit(_tenantId, _user.Id, unitId)).ToList();
        _user.Roles = _roleIds.Select(roleId => new UserRole(_tenantId, _user.Id, roleId)).ToList();
        return _user;
    }

    public User Save(EHRDbContext context)
    {
        var savedUser = context.Users.Add(Build()).Entity;
        context.SaveChanges();
        
        return savedUser;
    }

    private static User CreateRandomUser()
    {
        var faker = new Faker<User>();
        faker.RuleFor(u => u.Name, f => f.Person.FirstName);
        faker.RuleFor(u => u.Surname, f => f.Person.LastName);
        faker.RuleFor(u => u.MiddleName, f => f.Person.FirstName);
        faker.RuleFor(u => u.UserName, f => f.Person.UserName);
        faker.RuleFor(u => u.EmailAddress, f => f.Person.Email);
        faker.RuleFor(u => u.Password, f => f.Internet.Password());
        faker.RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());
        faker.RuleFor(u => u.Title, f => f.PickRandom<TitleType>());
        faker.RuleFor(u => u.IsActive, f => f.Random.Bool());
        return faker.Generate();
    }
}