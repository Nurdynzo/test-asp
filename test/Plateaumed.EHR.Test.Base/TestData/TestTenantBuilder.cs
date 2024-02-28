using System.Collections.Generic;
using System.Linq;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.MultiTenancy;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestTenantBuilder
{
    private string _tenancyName;
    private string _name;
    private TenantType _tenantType;
    private readonly List<TenantOnboardingProgress> _onboardingProgress = new();
    private bool _isOnboarded;

    private TestTenantBuilder()
    {
        SetDefaults();
    }

    public static TestTenantBuilder Create()
    {
        return new TestTenantBuilder();
    }

    public TestTenantBuilder WithOnboardingStatus(params (TenantOnboardingStatus onboardingStatus, bool completed)[] status)
    {
        _onboardingProgress.AddRange(status.Select(s => new TenantOnboardingProgress
        {
            TenantOnboardingStatus = s.onboardingStatus, Completed = s.completed
        }));
        return this;
    }

    public TestTenantBuilder WithIsOnboarded(bool isOnboarded)
    {
        _isOnboarded = isOnboarded;
        return this;
    }

    public Tenant Build()
    {
        return new Tenant(_tenancyName, _name, _tenantType)
        {
            OnboardingProgress = _onboardingProgress,
            IsOnboarded = _isOnboarded
        };
    }

    private void SetDefaults()
    {
        _tenancyName = "Test Tenant";
        _name = "Test Tenant";
        _tenantType = TenantType.Business;
        _isOnboarded = false;
    }

    public Tenant Save(EHRDbContext context)
    {
        var tenant = context.Tenants.Add(Build()).Entity;
        context.SaveChanges();
        return tenant;
    }
}