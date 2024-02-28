using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Migrations.Seed.Tenants;

public class FacilityBuilder
{
    private readonly EHRDbContext _context;
    private readonly int _tenantId;

    public FacilityBuilder(EHRDbContext context, int tenantId)
    {
        _context = context;
        _tenantId = tenantId;
    }

    public Facility Create(long facilityGroupId, long facilityTypeId)
    {
        var existingFacility = _context.Facilities.IgnoreQueryFilters()
            .FirstOrDefault(x => x.TenantId == _tenantId && x.GroupId == facilityGroupId  && x.Name == "Default");
        if (existingFacility != null) return existingFacility;
        var facility = new Facility
        {
            TenantId = _tenantId,
            GroupId = facilityGroupId,
            Name = "Default",
            TypeId = facilityTypeId,
            
            PatientCodeTemplate = new PatientCodeTemplate
            {
                Length = 10,
                Prefix = "P",
                Suffix = "A",
            }

        };

        _context.Facilities.Add(facility);
        _context.SaveChanges();
        return facility;
    }   
}