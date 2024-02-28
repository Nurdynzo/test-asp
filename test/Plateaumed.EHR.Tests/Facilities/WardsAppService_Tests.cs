using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.Test.Base.TestData;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    public class WardsAppService_Tests : AppTestBase
    {
        private readonly IWardsAppService _wardAppService;

        public WardsAppService_Tests()
        {
            _wardAppService = Resolve<IWardsAppService>();
        }

        [Fact]
        public async Task Test_Create_GivenFacilityIdDoesNotExist_ShouldThrow()
        {
            // Arrange
            const int facilityId = 999;

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _wardAppService.CreateOrEdit(new CreateOrEditWardDto
                {
                    Name = "Test Ward",
                    Description = "Test Ward Description",
                    FacilityId = facilityId
                });
            });

            // Assert
            exception.Message.ShouldBe("Facility does not exist");
        }
        
        [Fact]
        public async Task Test_Create_GivenFacilityDoesNotBelongToTenant_ShouldThrow()
        {
            // Arrange
            Tenant tenant = null;
            User user = null;
            var facility = CreateFacility();

            var input = new CreateOrEditWardDto
            {
                Name = "Test Ward",
                Description = "Test Ward Description",
                FacilityId = facility.Id
            };

            UsingDbContext(context =>
            {
                tenant = TestTenantBuilder.Create().Save(context);
                user = TestUserBuilder.Create(tenant.Id).Save(context);
            });

            LoginAsTenant(tenant.TenancyName, user.UserName);
            await SetUserPermissions(user, AppPermissions.Pages_Wards_Create, AppPermissions.Pages_Wards);

            // Act
            var exception =
                await Assert.ThrowsAsync<UserFriendlyException>(async () => await _wardAppService.CreateOrEdit(input));

            // Assert
            exception.Message.ShouldBe("Facility does not exist");
        }

        [Fact]
        public async Task Test_Create_GivenWardBedsWithNewBedTypes_ShouldSave()
        {
            // Arrange
            var facility = CreateFacility();

            var input = new CreateOrEditWardDto
            {
                Name = "Ward1",
                Description = "Ward1 Description",
                FacilityId = facility.Id,
                WardBeds = new List<CreateOrEditWardBedDto>
                {
                    new()
                    {
                        Count = 3,
                        IsActive = true,
                        BedTypeName = "new bed",
                    },
                    new()
                    {
                        Count = 2,
                        IsActive = true,
                        BedTypeName = "another new bed",
                    }
                }
            };

            // Act

            await _wardAppService.CreateOrEdit(input);

            // Assert
            var ward = UsingDbContext(context =>
                context.Wards.Include(w => w.WardBeds).First(ward => ward.Name == input.Name));
            ward.WardBeds.Count.ShouldBe(5);
        }

        [Fact]
        public async Task Test_Create_GivenWardBedsWithExistingBedTypes_ShouldSave()
        {
            // Arrange
            var facility = CreateFacility();
            var bedType = UsingDbContext(context =>
                TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").Save(context));

            var input = new CreateOrEditWardDto
            {
                Name = "Ward2",
                Description = "Ward2 Description",
                FacilityId = facility.Id,
                WardBeds = new List<CreateOrEditWardBedDto>
                {
                    new()
                    {
                        Count = 3,
                        IsActive = true,
                        BedTypeName = "new bed",
                        BedTypeId = bedType.Id
                    }
                }
            };

            // Act

            await _wardAppService.CreateOrEdit(input);

            // Assert
            var ward = UsingDbContext(context =>
                context.Wards.Include(w => w.WardBeds)
                    .ThenInclude(w => w.BedType)
                    .First(ward => ward.Name == input.Name));
            ward.WardBeds.Count.ShouldBe(3);
            ward.WardBeds.First().BedType.Name.ShouldBe("new bed");
        }

        [Fact]
        public async Task Test_Create_GivenWardBedsWithExistingBedTypeName_ShouldThrow()
        {
            // Arrange
            var facility = CreateFacility();
            UsingDbContext(context =>
                TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").WithFacility(facility.Id).Save(context));

            var input = new CreateOrEditWardDto
            {
                Name = "Ward2",
                Description = "Ward2 Description",
                FacilityId = facility.Id,
                WardBeds = new List<CreateOrEditWardBedDto>
                {
                    new()
                    {
                        Count = 3,
                        IsActive = true,
                        BedTypeName = "Cot",
                        BedTypeId = null
                    }
                }
            };

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async ()=> await _wardAppService.CreateOrEdit(input));

            // Assert
            exception.Message.ShouldBe("Bed type name already exists");
        }

        [Fact]
        public async Task Test_Create_GivenWardBedsWithNonExistentBedTypeId_ShouldThrow()
        {
            // Arrange
            var facility = CreateFacility();

            var input = new CreateOrEditWardDto
            {
                Name = "Ward2",
                Description = "Ward2 Description",
                FacilityId = facility.Id,
                WardBeds = new List<CreateOrEditWardBedDto>
                {
                    new()
                    {
                        Count = 3,
                        IsActive = true,
                        BedTypeId = 999
                    }
                }
            };

            // Act
            var exception =
                await Assert.ThrowsAsync<UserFriendlyException>(async () => await _wardAppService.CreateOrEdit(input));

            // Assert
            exception.Message.ShouldBe("Bed type does not exist");
        }

        [Fact]
        public async Task Test_Update_GivenFacilityIdDoesNotExist_ShouldThrow()
        {
            // Arrange
            const int facilityId = 999;

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _wardAppService.CreateOrEdit(new CreateOrEditWardDto
                {
                    Id = 1,
                    Name = "Test Ward",
                    Description = "Test Ward Description",
                    FacilityId = facilityId
                });
            });

            // Assert
            exception.Message.ShouldBe("Facility does not exist");
        }

        [Fact]
        public async Task Test_Update_GivenWardIdDoesNotExist_ShouldThrow()
        {
            // Arrange
            var facility = CreateFacility();

            // Act
            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _wardAppService.CreateOrEdit(new CreateOrEditWardDto
                {
                    Id = 99,
                    Name = "Test Ward",
                    Description = "Test Ward Description",
                    FacilityId = facility.Id
                });
            });

            // Assert
            exception.Message.ShouldBe("Ward does not exist");
        }

        [Fact]
        public async Task Test_Update_GivenWardDetails_ShouldSave()
        {
            // Arrange
            var facility = CreateFacility();
            var (bedType, ward) = UsingDbContext(context =>
            {
                var bt = TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").Save(context);
                var w = TestWardBuilder.Create(DefaultTenantId, facility.Id).WithBed(bt, 2).Save(context);
                return (bt, w);
            });

            var input = new CreateOrEditWardDto
            {
                Id = ward.Id,
                Name = "Ward2",
                Description = "Ward2 Description",
                FacilityId = facility.Id,
                WardBeds = new List<CreateOrEditWardBedDto>
                {
                    new()
                    {
                        Count = 3,
                        IsActive = true,
                        BedTypeName = "Cot",
                        BedTypeId = bedType.Id
                    },
                    new()
                    {
                        Count = 2,
                        IsActive = true,
                        BedTypeName = "new bed",
                        WardId = ward.Id
                    }
                }
            };

            // Act

            await _wardAppService.CreateOrEdit(input);

            // Assert
            var saved = UsingDbContext(context => context.Wards.Include(w => w.WardBeds)
                .ThenInclude(w => w.BedType)
                .First(w => w.Name == input.Name));
            saved.Name.ShouldBe(input.Name);
            saved.Description.ShouldBe(input.Description);
            saved.WardBeds.Count.ShouldBe(2);
            saved.WardBeds.First().BedType.Name.ShouldBe("Cot");
            saved.WardBeds.First().BedNumber.ShouldBe("3");
            saved.WardBeds.Last().BedType.Name.ShouldBe("new bed");
        }

        [Fact]
        public async Task Test_GetAll_GivenFacility_ShouldReturnWardWithBeds()
        {
            // Arrange
            var facility = CreateFacility();
            var ward = UsingDbContext(context =>
            {
                var bedType = TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").Save(context);
                return TestWardBuilder.Create(DefaultTenantId, facility.Id).WithName("Ward1")
                    .WithBed(bedType, 4).Save(context);
            });

            // Act
            var pagedResultDto = await _wardAppService.GetAll(new GetAllWardsInput
                { FacilityIds = new List<long> { facility.Id } });
            var savedWard = pagedResultDto.Items[0].Ward;

            // Assert
            pagedResultDto.Items.Count.ShouldBe(1);
            savedWard.WardBeds.Count.ShouldBe(1);
            savedWard.WardBeds.First().BedTypeName.ShouldBe(ward.WardBeds.First().BedType.Name);
        }


        [Fact]
        public async Task Test_GetAll_GivenFacilityWitNoWards_ShouldReturnEmpty()
        {
            // Arrange
            const long dummyFacilityId = 69;
            var facility = CreateFacility();
            var ward = UsingDbContext(context =>
            {
                var bedType = TestBedTypeBuilder.Create(DefaultTenantId).WithName("Cot").Save(context);
                return TestWardBuilder.Create(DefaultTenantId, facility.Id).WithName("Ward1")
                    .WithBed(bedType, 4).Save(context);
            });

            // Act
            var pagedResultDto = await _wardAppService.GetAll(new GetAllWardsInput
                { FacilityIds = new List<long> { dummyFacilityId } });

            // Assert
            pagedResultDto.Items.Count.ShouldBe(0);
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