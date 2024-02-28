using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Staff;

namespace Plateaumed.EHR.Migrations.Seed.Tenants;

public class StaffMemberBuilder
{
    private readonly EHRDbContext _context;
    private readonly int _tenantId;

    public StaffMemberBuilder(EHRDbContext context, int tenantId)
    {
        _context = context;
        _tenantId = tenantId;
    }

    public void Create(long facilityId)
    {
        CreateFacilityAndAssignUser(facilityId);
        CreateFacilityAndAssignUser(facilityId,2);
    }

    private void CreateFacilityAndAssignUser(long facilityId, long userId = 1)
    {
        var exists = _context.StaffMembers.IgnoreQueryFilters().FirstOrDefault(x => x.UserId == userId);
        var jobLevel = _context.JobLevels.IgnoreQueryFilters().First();

        var staffMember = new StaffMember
        {
            UserId = userId,
        };
        if (exists == null)
        {
          
            _context.StaffMembers.Add(staffMember);
            _context.SaveChanges();
            exists = staffMember;
        }
        var facilityExists = _context.FacilityStaff.IgnoreQueryFilters().FirstOrDefault(x => x.FacilityId == facilityId && x.StaffMemberId == exists.Id);
        if (facilityExists == null)
        {
            var staffFacility = new FacilityStaff
            {
                FacilityId = facilityId,
                IsDefault = true,
                StaffMemberId = exists.Id
            };
            _context.FacilityStaff.Add(staffFacility);
            _context.SaveChanges();
        }
        
        
    }
}