using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    public class WardBedAppService_Tests : AppTestBase
    {
        private readonly IWardBedsAppService _wardBedAppService;

        public WardBedAppService_Tests()
        {
            _wardBedAppService = Resolve<IWardBedsAppService>();
        }

        [Fact]
        public async Task  Test_Create_GivenValidWardBed_ShouldSave()
        {
            // Arrange
            var bedType = UsingDbContext(context =>
                TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").Save(context));
            var ward = UsingDbContext(context =>
           TestWardBuilder.Create(DefaultTenantId, facilityId: 1).WithName("WardName").Save(context));

            var request = new CreateOrEditWardBedDto
            {
                Count = 3,
                IsActive = true,
                BedTypeName = bedType.Name,
                BedTypeId = bedType.Id,
                WardId = ward.Id
            };

            // Act
            await _wardBedAppService.CreateOrEdit(request);

            // Assert
            var wardBeds = UsingDbContext(context =>
                context.WardBeds.Where(wb => wb.WardId == ward.Id && wb.BedTypeId == bedType.Id).ToList());
            wardBeds.Count.ShouldBe(3);
        }


        [Fact]
        public async Task Test_InsertAdditionalWardBeds_ShouldInsert()
        {
            // Arrange
            var bedType = UsingDbContext(context =>
                TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").Save(context));
            var ward = UsingDbContext(context =>
          TestWardBuilder.Create(DefaultTenantId, facilityId: 1).WithName("WardName").Save(context));

            var wardBed = UsingDbContext(context =>
               TestWardBedBuilder.Create(DefaultTenantId, bedType.Id, ward.Id).WithBedNumber("2").IsActive(true).Save(context));
            var request = new CreateOrEditWardBedDto
            {
                Count = 3,
                IsActive = true,
                BedTypeName = bedType.Name,
                BedTypeId = bedType.Id,
                WardId = ward.Id,
                Id = wardBed.Id
            };

            // Act
            await _wardBedAppService.CreateOrEdit(request);

            // Assert
            var insertedWardBeds = UsingDbContext(context =>
                context.WardBeds.Where(wb => wb.WardId == wardBed.WardId && wb.BedTypeId == bedType.Id).ToList());
            insertedWardBeds.Count.ShouldBe(request.Count);
        }

    }
}
