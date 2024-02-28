using Plateaumed.EHR.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Test.Base.TestData
{
    

    public class TestRoomsBuilder
    {
        private int? _tenantId;
        private string _name;
        private long _facilityId;
        private bool _isActive;
        private int? _minTimeInterval;
        private RoomType _type;
        private List<RoomAvailability> _availabilities;

        public TestRoomsBuilder WithTenantId(int? tenantId)
        {
            _tenantId = tenantId;
            return this;
        }

        public TestRoomsBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TestRoomsBuilder WithFacilityId(long facilityId)
        {
            _facilityId = facilityId;
            return this;
        }

        public TestRoomsBuilder IsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }

        public TestRoomsBuilder WithMinTimeInterval(int? minTimeInterval)
        {
            _minTimeInterval = minTimeInterval;
            return this;
        }

        public TestRoomsBuilder WithRoomType(RoomType type)
        {
            _type = type;
            return this;
        }

        public TestRoomsBuilder WithAvailabilities(List<RoomAvailability> availabilities)
        {
            _availabilities = availabilities;
            return this;
        }

        public Rooms Build()
        {
            return new Rooms
            {
                TenantId = _tenantId,
                Name = _name,
                FacilityId = _facilityId,
                IsActive = _isActive,
                MinTimeInterval = _minTimeInterval,
                Type = _type,
                Availabilities = _availabilities
            };
        }
    }

}
