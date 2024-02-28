using Bogus;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestWardBuilder
{
    private Ward _ward;

    private TestWardBuilder(int tenantId, long facilityId)
    {
        SetDefaults();
        _ward.TenantId = tenantId;
        _ward.FacilityId = facilityId;
    }
    
    public static TestWardBuilder Create(int tenantId, long facilityId)
    {
        return new TestWardBuilder(tenantId, facilityId);
    }

    public TestWardBuilder WithName(string name)
    {
        _ward.Name = name;
        return this;
    }
    
    public TestWardBuilder WithBed(BedType bedType, int count)
    {
        _ward.WardBeds.Add(new WardBed
        {
            BedType = bedType,
            BedNumber = $"{bedType.Name} {count}"
        });
        return this;
    }
    
    public Ward Build()
    {
        return _ward;
    }

    public Ward Save(EHRDbContext context)
    {
        var ward = context.Wards.Add(Build()).Entity;
        context.SaveChanges();
        return ward;
    }

    private void SetDefaults()
    {
        var faker = new Faker<Ward>();
        faker.RuleFor(w => w.Name, f => f.Random.String());
        faker.RuleFor(w => w.Description, f => f.Random.String());
        _ward = faker.Generate();
    }
}