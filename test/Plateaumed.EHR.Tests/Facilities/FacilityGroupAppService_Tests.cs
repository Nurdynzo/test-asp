using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Test.Base.TestData;
using Xunit;

namespace Plateaumed.EHR.Tests.Facilities
{
    public class FacilityGroupAppService_Tests : AppTestBase
    {
        private readonly IFacilityGroupsAppService _facilityGroupsAppService;

        public FacilityGroupAppService_Tests()
        {
            _facilityGroupsAppService = Resolve<IFacilityGroupsAppService>();
        }

        [Fact]
        public async Task CreateOrEditPatientCodeTemplateDetails_GivenValidInput_ShouldSavePatientCodeTemplate()
        {
            // Arrange
            var facility = CreateFacility();

            // Act
            await _facilityGroupsAppService.CreateOrEditPatientCodeTemplateDetails(
                new CreateOrEditFacilityGroupPatientCodeTemplateDto
                {
                    Id = facility.GroupId,
                    Name = "Pew",
                    ChildFacilities = new List<CreateOrEditFacilityPatientCodeTemplateDto>
                    {
                        new()
                        {
                            Id = facility.Id,
                            Name = "Fac1",
                            GroupId = facility.GroupId,
                            PatientCodeTemplate = new CreateOrEditPatientCodeTemplateDto
                            {
                                Prefix = "Pat",
                                Length = 8,
                                Suffix = "ient",
                                StartingIndex = 1
                            },
                            
                        }
                    }
                });
            // Assert
            UsingDbContext(context =>
            {
                var savedFacility = context.Facilities.Include(f => f.PatientCodeTemplate)
                    .First(x => x.Id == facility.Id);
                Assert.Equal("Pat", savedFacility.PatientCodeTemplate.Prefix);
                Assert.Equal(8, savedFacility.PatientCodeTemplate.Length);
                Assert.Equal("ient", savedFacility.PatientCodeTemplate.Suffix);
                Assert.Equal(1, savedFacility.PatientCodeTemplate.StartingIndex);
            });
        }
        
        private Facility CreateFacility()
        {
            return UsingDbContext(context =>
            {
                var group = TestFacilityGroupBuilder.Create(DefaultTenantId).Save(context);
                var facility = TestFacilityBuilder.Create(DefaultTenantId, group.Id).Save(context);
                
                context.SaveChanges();
                return facility;
            });
        }
    }
}