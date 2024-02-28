using System;
using System.Collections.Generic;
using Abp.Organizations;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestOrganizationUnitExtendedBuilder
{
    private readonly int _tenantId;
    private readonly long _facilityId;
    private readonly string _displayName;
    private OrganizationUnitExtended _organizationUnit;
    private string _shortName;
    private bool _active;
    private bool _isDeleted;
    private string _code;
    private ICollection<OrganizationUnit> _children;
    private OrganizationUnitType _type;
    
    private TestOrganizationUnitExtendedBuilder(int tenantId, long facilityId, string displayName)
    {
        _tenantId = tenantId;
        _facilityId = facilityId;
        _displayName = displayName;
        SetDefaults();
    }
    
    public static TestOrganizationUnitExtendedBuilder Create(int tenantId, long facilityId, string displayName)
    {
        return new TestOrganizationUnitExtendedBuilder(tenantId, facilityId, displayName);
    }

    public TestOrganizationUnitExtendedBuilder WithCode(params int[] code)
    {
        _code = OrganizationUnit.CreateCode(code);
        return this;
    }

    public TestOrganizationUnitExtendedBuilder WithShortName(string shortName)
    {
        _shortName = shortName;
        return this;
    }

    public TestOrganizationUnitExtendedBuilder WithActive(bool active)
    {
        _active = active;
        return this;
    }

    public TestOrganizationUnitExtendedBuilder WithFacilityType()
    {
        _type = OrganizationUnitType.Facility;
        return this;
    }
    
    public TestOrganizationUnitExtendedBuilder WithDepartmentType()
    {
        _type = OrganizationUnitType.Department;
        return this;
    }
    
    public TestOrganizationUnitExtendedBuilder WithUnitType()
    {
        _type = OrganizationUnitType.Unit;
        return this;
    }

    public TestOrganizationUnitExtendedBuilder WithClinicType()
    {
        _type = OrganizationUnitType.Clinic;
        return this;
    }

    public TestOrganizationUnitExtendedBuilder WithChildren(params OrganizationUnitExtended[] children)
    {
        Array.ForEach(children, c => _children.Add(c));
        return this;
    }

    public OrganizationUnitExtended Build()
    {
        _organizationUnit = new OrganizationUnitExtended
        {
            DisplayName = _displayName,
            ShortName = _shortName,
            IsActive = _active,
            Type = _type,
            TenantId = _tenantId,
            CreationTime = DateTime.Now,
            IsDeleted = _isDeleted,
            FacilityId = _facilityId,
            Code = _code,
            Children = _children,
        };

        return _organizationUnit;
    }

    public OrganizationUnitExtended Save(EHRDbContext context)
    {
        var savedOrg = context.OrganizationUnitExtended.Add(Build()).Entity;
        context.SaveChanges();
        return savedOrg;
    }

    private void SetDefaults()
    {
        _shortName = "Test OU";
        _active = true;
        _isDeleted = false;
        _children = new List<OrganizationUnit>();
        _type = OrganizationUnitType.Unit;
        _code = OrganizationUnit.CreateCode(1,2,3);
    }
}