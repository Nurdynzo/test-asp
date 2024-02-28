using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Insurance;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using System;
using System.Linq;
using Abp.Organizations;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(Invoice),
            typeof(InvoiceItem),
            typeof(Country),
            typeof(PatientOccupation),
            typeof(OccupationCategory),
            typeof(StaffMember),
            typeof(OrganizationUnit),
            typeof(Role),
            typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(Invoice),
            typeof(InvoiceItem),
            typeof(PatientAppointment),
            typeof(PatientReferralDocument),
            typeof(PatientInsurer),
            typeof(PatientRelation),
            typeof(Patient),
            typeof(Country),
            typeof(PatientOccupation),
            typeof(OccupationCategory),
            typeof(OrganizationUnitTime),
            typeof(FacilityDocument),
            typeof(FacilityInsurer),
            typeof(InsuranceProvider),
            typeof(FacilityStaff),
            typeof(StaffMember),
            typeof(TenantDocument),
            typeof(WardBed),
            typeof(Ward),
            typeof(BedType),
            typeof(Facility),
            typeof(FacilityGroup),
            typeof(FacilityType),
            typeof(StaffCodeTemplate),
            typeof(PatientCodeTemplate),
            typeof(JobLevel),
            typeof(JobTitle),
            typeof(OrganizationUnit),
            typeof(Role)
        };

        public static readonly Type[] TrackedTypes = HostSideTrackedTypes
            .Concat(TenantSideTrackedTypes)
            .GroupBy(type => type.FullName)
            .Select(types => types.First())
            .ToArray();
    }
}