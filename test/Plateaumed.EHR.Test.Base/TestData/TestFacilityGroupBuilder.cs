using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestFacilityGroupBuilder
{
    private readonly int _tenantId;
    private FacilityGroup _facilityGroup;
    private string _facilityGroupName;
    private bool _isDeleted;

    private TestFacilityGroupBuilder(int tenantId)
    {
        _tenantId = tenantId;
        SetDefaults();
    }

    public static TestFacilityGroupBuilder Create(int tenantId)
    {
        return new TestFacilityGroupBuilder(tenantId);
    }

    public FacilityGroup Build()
    {
        _facilityGroup = new FacilityGroup
        {
            Name = _facilityGroupName,
            TenantId = _tenantId,
            IsDeleted = _isDeleted,
        };
        return _facilityGroup;
    }

    public FacilityGroup Save(EHRDbContext context)
    {
        var savedFacility = context.FacilityGroups.Add(Build()).Entity;
        context.SaveChanges();
        return savedFacility;
    }

    private void SetDefaults()
    {
        _facilityGroupName = "Test Facility Group";
        _isDeleted = false;
    }
}