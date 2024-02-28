using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.EntityFrameworkCore;

namespace Plateaumed.EHR.Test.Base.TestData
{
    public class TestRoleBuilder
    {
        private readonly Role _role = new();

        private TestRoleBuilder(int tenantId)
        {
            _role.TenantId = tenantId;
        }

        public static TestRoleBuilder Create(int tenantId)
        {
            return new TestRoleBuilder(tenantId);
        }

        public TestRoleBuilder WithName(string name)
        {
            _role.Name = name;
            _role.DisplayName = name;
            return this;
        }

        public Role Build()
        {
            _role.SetNormalizedName();
            return _role;
        }

        public Role Save(EHRDbContext context)
        {
            var role = context.Roles.Add(Build()).Entity;
            context.SaveChanges();
            return role;
        }
    }
}