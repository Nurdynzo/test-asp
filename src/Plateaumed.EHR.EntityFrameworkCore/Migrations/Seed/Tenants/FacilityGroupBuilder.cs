using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Migrations.Seed.Tenants;

public class FacilityGroupBuilder
{
    private readonly EHRDbContext _context;
    private readonly int _tenantId;

    public FacilityGroupBuilder(EHRDbContext context, int tenantId)
    {
        _context = context;
        _tenantId = tenantId;
    }

    public FacilityGroup Create()
    {
        FacilityGroup groupExists = _context.FacilityGroups.FirstOrDefault(x=>x.TenantId == _tenantId && x.Name == "Default");
        if (groupExists != null) return groupExists;
        var facilityGroup = new FacilityGroup()
        {
            Name = "Default",
            TenantId = _tenantId,
        };
        _context.FacilityGroups.Add(facilityGroup);
        _context.SaveChanges();
        return facilityGroup;
    }
}