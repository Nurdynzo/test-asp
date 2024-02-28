using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Test.Base.TestData
{
    public class TestFacilityBuilder
    {
        private readonly int _tenantId;
        private readonly long _groupId;
        private string _facilityName;
        private int _typeId;
        private bool _active;
        private bool _isDeleted;
        private long _id;

        private TestFacilityBuilder(int tenantId, long groupId)
        {
            _tenantId = tenantId;
            _groupId = groupId;
            SetDefaults();
        }

        public static TestFacilityBuilder Create (int tenantId, long groupId)
        {
            return new TestFacilityBuilder(tenantId, groupId);
        }

        public TestFacilityBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public Facility Build()
        {
            return new Facility
            {
                Id = _id,
                Name = _facilityName,
                IsActive = _active,
                TenantId = _tenantId,
                IsDeleted = _isDeleted,
                TypeId = _typeId,
                GroupId = _groupId, 
            };
        }

        public Facility Save(EHRDbContext context)
        {
            var savedFacility = context.Facilities.Add(Build()).Entity;
            context.SaveChanges();
            return savedFacility;
        }

        private void SetDefaults()
        {
            _facilityName = "Test Facility";
            _typeId = 1;
            _active = true;
            _isDeleted = false;
        }
    }
}