using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Plateaumed.EHR.Test.Base.TestData;

namespace Plateaumed.EHR.Tests.Facilities
{
    public class RoomAppService_Tests : AppTestBase
    {
        private readonly IRoomAppService _roomAppService;

        public RoomAppService_Tests()
        {
            LoginAsDefaultTenantAdmin();
            _roomAppService = Resolve<IRoomAppService>();
        }

        [Fact]
        public async Task CreateOperatingRoom_GivenValidOperatingRoom_ShouldSave()
        {
            // Arrange
            int tenantId = 1;

            var facility = CreateFacility();
            var operatingRoom = new TestRoomsBuilder()
                .WithTenantId(tenantId)
                .WithFacilityId(facility.Id)
                .WithName("Operating Room 1")
                .IsActive(true)
                .WithRoomType(RoomType.OperatingRoom)
                .WithMinTimeInterval(30)
                .WithAvailabilities(new List<RoomAvailability>
                {
            new TestRoomAvailabilityBuilder()
                .WithDayOfWeek(DayOfWeek.Monday)
                .WithTime(TimeSpan.FromHours(8), TimeSpan.FromHours(12))
                .Build()
                })
                .Build();
            var createOperatingRoomDto = new CreateOrEditRoomsDto
            {
                Id = null,
                Name = operatingRoom.Name,
                FacilityId = operatingRoom.FacilityId,
                RoomAvailability = operatingRoom.Availabilities.Select(availability => new RoomAvailabilityDto
                {
                    DayOfWeek = availability.DayOfWeek,
                    StartTime = availability.StartTime,
                    EndTime = availability.EndTime
                }).ToList(),
                MinTimeInterval = operatingRoom.MinTimeInterval,
                Type = operatingRoom.Type
            };

            // Act
            await _roomAppService.CreateOrEdit(createOperatingRoomDto);

            // Assert
            var createdRoom = await UsingDbContextAsync(async context =>
            {
                return await context.Rooms.FirstOrDefaultAsync(r => r.Name == "Operating Room 1");
            });

            createdRoom.TenantId.ShouldBe(tenantId);
            createdRoom.Type.ShouldBe(RoomType.OperatingRoom);
        }
        
        [Fact]
        public async Task CreateConsultingRoom_GivenValidConsultingRoom_ShouldSave()
        {
            // Arrange
            int tenantId = 1;
            var facility = CreateFacility();
            var consultingRoom = new TestRoomsBuilder()
                .WithTenantId(tenantId)
                .WithFacilityId(facility.Id)
                .WithName("Consulting Room A")
                .IsActive(true)
                .WithRoomType(RoomType.ConsultingRoom)
                .Build();

            var createConsultingRoomDto = new CreateOrEditRoomsDto
            {
                Id = null,
                Name = consultingRoom.Name,
                FacilityId = consultingRoom.FacilityId,
                Type = consultingRoom.Type
            };

            // Act
            await _roomAppService.CreateOrEdit(createConsultingRoomDto);

            // Assert
            var createdRoom = await UsingDbContextAsync(async context =>
            {
                return await context.Rooms.FirstOrDefaultAsync(r => r.Name == "Consulting Room A");
            });

            createdRoom.TenantId.ShouldBe(tenantId);
            createdRoom.Type.ShouldBe(RoomType.ConsultingRoom);
        }

        private Facility CreateFacility()
        {
            return UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(DefaultTenantId).Save(context);
                return TestFacilityBuilder.Create(DefaultTenantId, group.Id).Save(context);
            });
        }
    }
}
