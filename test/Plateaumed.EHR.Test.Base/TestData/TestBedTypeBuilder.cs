using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestBedTypeBuilder
{
    private readonly int _tenantId;
    private BedType _bedType;
    private string _name;
    private long? _facilityId;

    private TestBedTypeBuilder (int tenantId)
    {
        _tenantId = tenantId;
    }

    public static TestBedTypeBuilder Create(int tenantId)
    {
        return new TestBedTypeBuilder(tenantId);
    }

    public TestBedTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TestBedTypeBuilder WithFacility(long facilityId)
    {
        _facilityId = facilityId;
        return this;
    }

    public BedType Build()
    {
        _bedType = new BedType
        {
            Name = _name,
            TenantId = _tenantId,
            FacilityId = _facilityId
        };
        return _bedType;
    }

    public BedType Save(EHRDbContext context)
    {
        var bedType = context.BedTypes.Add(Build()).Entity;
        context.SaveChanges();
        return bedType;
    }
}