using Plateaumed.EHR.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Test.Base.TestData
{

    public class TestRoomAvailabilityBuilder
    {
        private Rooms _room;
        private DayOfWeek? _dayOfWeek;
        private TimeSpan? _startTime;
        private TimeSpan? _endTime;

        public TestRoomAvailabilityBuilder ForRoom(Rooms room)
        {
            _room = room;
            return this;
        }

        public TestRoomAvailabilityBuilder WithDayOfWeek(DayOfWeek? dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
            return this;
        }

        public TestRoomAvailabilityBuilder WithTime(TimeSpan? startTime, TimeSpan? endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
            return this;
        }

        public RoomAvailability Build()
        {
            return new RoomAvailability
            {
                Rooms = _room,
                DayOfWeek = _dayOfWeek,
                StartTime = _startTime,
                EndTime = _endTime
            };
        }
    }

}
