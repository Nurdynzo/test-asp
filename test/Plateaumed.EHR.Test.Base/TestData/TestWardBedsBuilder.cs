using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Test.Base.TestData
{
    public class TestWardBedBuilder
    {
        private WardBed _wardBed;

        private TestWardBedBuilder(int tenantId, long bedTypeId, long wardId)
        {
            SetDefaults();
            _wardBed.TenantId = tenantId;
            _wardBed.BedTypeId = bedTypeId;
            _wardBed.WardId = wardId;
        }

        public static TestWardBedBuilder Create(int tenantId, long bedTypeId, long wardId)
        {
            return new TestWardBedBuilder(tenantId, bedTypeId, wardId);
        }

        public TestWardBedBuilder WithBedNumber(string bedNumber)
        {
            _wardBed.BedNumber = bedNumber;
            return this;
        }

        public TestWardBedBuilder IsActive(bool isActive)
        {
            _wardBed.IsActive = isActive;
            return this;
        }

        public WardBed Build()
        {
            return _wardBed;
        }

        public WardBed Save(EHRDbContext context)
        {
            var wardBed = context.WardBeds.Add(Build()).Entity;
            context.SaveChanges();
            return wardBed;
        }

        private void SetDefaults()
        {
            _wardBed = new WardBed
            {
                IsActive = true
            };
        }
    }

}
