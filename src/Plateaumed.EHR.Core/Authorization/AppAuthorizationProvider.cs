using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Plateaumed.EHR.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages =
                context.GetPermissionOrNull(AppPermissions.Pages)
                ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var invoices = pages.CreateChildPermission(AppPermissions.Pages_Invoices, L("Invoices"));
            invoices.CreateChildPermission(AppPermissions.Pages_Invoices_Create, L("CreateNewInvoice"));
            invoices.CreateChildPermission(AppPermissions.Pages_Invoices_Edit, L("EditInvoice"));
            invoices.CreateChildPermission(AppPermissions.Pages_Invoices_Delete, L("DeleteInvoice"));

            var invoiceItems = pages.CreateChildPermission(AppPermissions.Pages_InvoiceItems, L("InvoiceItems"));
            invoiceItems.CreateChildPermission(AppPermissions.Pages_InvoiceItems_Create, L("CreateNewInvoiceItem"));
            invoiceItems.CreateChildPermission(AppPermissions.Pages_InvoiceItems_Edit, L("EditInvoiceItem"));
            invoiceItems.CreateChildPermission(AppPermissions.Pages_InvoiceItems_Delete, L("DeleteInvoiceItem"));

            var patientProfile = pages.CreateChildPermission(AppPermissions.Pages_Patient_Profiles, L("PatientProfile"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Create, L("CreateNewPatientProfile"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Edit, L("EditPatientProfile"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Delete, L("DeletePatientProfile"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_View, L("ViewPatientProfile"));

            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_FamilyHistory_View, L("FamilyHistory_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_FamilyHistory_Create, L("FamilyHistory_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_FamilyHistory_Delete, L("FamilyHistory_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_OccupationHistory_View, L("OccupationHistory_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_OccupationHistory_Create, L("OccupationHistory_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_OccupationHistory_Delete, L("OccupationHistory_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_OccupationHistory_Edit, L("OccupationHistory_Edit"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_BloodGroup_Create, L("BloodGroup_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_BloodGroup_View, L("BloodGroup_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Physical_Exercise_View, L("Physical_Exercise_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Physical_Exercise_Create, L("Physical_Exercise_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Chronic_Condition_View, L("Chronic_Condition_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Create, L("Chronic_Condition_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Delete, L("Chronic_Condition_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Travel_History_View, L("Travel_History_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Travel_History_Create, L("Travel_History_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Travel_History_Delete, L("Travel_History_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Surgical_History_View, L("Surgical_History_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Surgical_History_Create, L("Surgical_History_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Surgical_History_Delete, L("Surgical_History_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Surgical_History_Edit, L("Surgical_History_Edit"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Vaccination_History_View, L("Vaccination_History_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Vaccination_History_Create, L("Vaccination_History_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Vaccination_History_Delete, L("Vaccination_History_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Drug_History_View, L("Drug_History_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Drug_History_Create, L("Drug_History_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Drug_History_Delete, L("Drug_History_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Drug_History_Edit, L("Drug_History_Edit"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Allergy_History_Edit, L("Allergy_History_Edit"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Allergy_History_Create, L("Allergy_History_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Allergy_History_View, L("Allergy_History_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Allergy_History_Delete, L("Allergy_History_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Implant_View, L("Implant_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Implant_Create, L("Implant_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Implant_Delete, L("Implant_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Implant_Edit, L("Implant_Edit"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Review_of_System_Delete, L("Review_of_System_Delete"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Review_of_System_Edit, L("Review_of_System_Edit"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Review_of_System_Create, L("Review_of_System_Create"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Review_of_System_View, L("Review_of_System_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Treatement_Plan_View, L("Treatement_Plan_View"));
            patientProfile.CreateChildPermission(AppPermissions.Pages_Patient_Profiles_Clinical_Investigation_View, L("Clinical_Investigation_View"));


            var patientAppointments = pages.CreateChildPermission(AppPermissions.Pages_PatientAppointments, L("PatientAppointments"), multiTenancySides: MultiTenancySides.Tenant);
            patientAppointments.CreateChildPermission(AppPermissions.Pages_PatientAppointments_Create, L("CreateNewPatientAppointment"), multiTenancySides: MultiTenancySides.Tenant);
            patientAppointments.CreateChildPermission(AppPermissions.Pages_PatientAppointments_Edit, L("EditPatientAppointment"), multiTenancySides: MultiTenancySides.Tenant);
            patientAppointments.CreateChildPermission(AppPermissions.Pages_PatientAppointments_Delete, L("DeletePatientAppointment"), multiTenancySides: MultiTenancySides.Tenant);

            var doctorDischarge = pages.CreateChildPermission(AppPermissions.Pages_Discharge, L("Discharge"), multiTenancySides: MultiTenancySides.Tenant);
            doctorDischarge.CreateChildPermission(AppPermissions.Pages_Discharge_Create, L("CreateNewDischarge"), multiTenancySides: MultiTenancySides.Tenant);
            doctorDischarge.CreateChildPermission(AppPermissions.Pages_Discharge_Edit, L("EditDischarge"), multiTenancySides: MultiTenancySides.Tenant);
            doctorDischarge.CreateChildPermission(AppPermissions.Pages_Discharge_Finalize, L("FinalizeDischarge"), multiTenancySides: MultiTenancySides.Tenant);

            var patientReferralDocuments = pages.CreateChildPermission(
                AppPermissions.Pages_PatientReferralDocuments,
                L("PatientReferralDocuments"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientReferralDocuments.CreateChildPermission(
                AppPermissions.Pages_PatientReferralDocuments_Create,
                L("CreateNewPatientReferralDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientReferralDocuments.CreateChildPermission(
                AppPermissions.Pages_PatientReferralDocuments_Edit,
                L("EditPatientReferralDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientReferralDocuments.CreateChildPermission(
                AppPermissions.Pages_PatientReferralDocuments_Delete,
                L("DeletePatientReferralDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            var patientDocuments = pages.CreateChildPermission(
                AppPermissions.Pages_ScanDocument,
                L("ScanDocuments"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientDocuments.CreateChildPermission(
                AppPermissions.Pages_ScanDocument_Upload,
                displayName: L("UploadScanDocument"), 
                multiTenancySides: MultiTenancySides.Tenant);
            patientDocuments.CreateChildPermission(AppPermissions.Pages_ScanDocument_Review, 
                displayName: L("ReviewScanDocument"),
                multiTenancySides: MultiTenancySides.Tenant);

            var patientInsurers = pages.CreateChildPermission(
                AppPermissions.Pages_PatientInsurers,
                L("PatientInsurers"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientInsurers.CreateChildPermission(
                AppPermissions.Pages_PatientInsurers_Create,
                L("CreateNewPatientInsurer"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientInsurers.CreateChildPermission(
                AppPermissions.Pages_PatientInsurers_Edit,
                L("EditPatientInsurer"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientInsurers.CreateChildPermission(
                AppPermissions.Pages_PatientInsurers_Delete,
                L("DeletePatientInsurer"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var patientRelations = pages.CreateChildPermission(
                AppPermissions.Pages_PatientRelations,
                L("PatientRelations"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientRelations.CreateChildPermission(
                AppPermissions.Pages_PatientRelations_Create,
                L("CreateNewPatientRelation"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientRelations.CreateChildPermission(
                AppPermissions.Pages_PatientRelations_Edit,
                L("EditPatientRelation"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientRelations.CreateChildPermission(
                AppPermissions.Pages_PatientRelations_Delete,
                L("DeletePatientRelation"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var patients = pages.CreateChildPermission(
                AppPermissions.Pages_Patients,
                L("Patients"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patients.CreateChildPermission(
                AppPermissions.Pages_Patients_Create,
                L("CreateNewPatient"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patients.CreateChildPermission(
                AppPermissions.Pages_Patients_Edit,
                L("EditPatient"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patients.CreateChildPermission(
                AppPermissions.Pages_Patients_Delete,
                L("DeletePatient"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var countries = pages.CreateChildPermission(
                AppPermissions.Pages_Countries,
                L("Countries")
            );
            countries.CreateChildPermission(
                AppPermissions.Pages_Countries_Create,
                L("CreateNewCountry")
            );
            countries.CreateChildPermission(AppPermissions.Pages_Countries_Edit, L("EditCountry"));

            countries.CreateChildPermission(
                AppPermissions.Pages_Countries_Delete,
                L("DeleteCountry")
            );

            var regions = pages.CreateChildPermission(
                AppPermissions.Pages_Regions,
                L("Regions")
            );
            regions.CreateChildPermission(
                AppPermissions.Pages_Regions_Create,
                L("CreateNewRegion")
            );
            regions.CreateChildPermission(AppPermissions.Pages_Regions_Edit, L("EditRegion"));

            regions.CreateChildPermission(
                AppPermissions.Pages_Regions_Delete,
                L("DeleteRegion")
            );

            var district = pages.CreateChildPermission(
                AppPermissions.Pages_Districts,
                L("Districts")
            );
            district.CreateChildPermission(
                AppPermissions.Pages_Districts_Create,
                L("CreateNewDistrict")
            );
            district.CreateChildPermission(AppPermissions.Pages_Districts_Edit, L("EditDistrict"));

            district.CreateChildPermission(
                AppPermissions.Pages_Districts_Delete,
                L("DeleteDistrict")
            );

            var patientOccupations = pages.CreateChildPermission(
                AppPermissions.Pages_PatientOccupations,
                L("PatientOccupations")
            );
            patientOccupations.CreateChildPermission(
                AppPermissions.Pages_PatientOccupations_Create,
                L("CreateNewPatientOccupation")
            );
            patientOccupations.CreateChildPermission(
                AppPermissions.Pages_PatientOccupations_Edit,
                L("EditPatientOccupation")
            );
            patientOccupations.CreateChildPermission(
                AppPermissions.Pages_PatientOccupations_Delete,
                L("DeletePatientOccupation")
            );

            var patientOccupationCategories = pages.CreateChildPermission(
                AppPermissions.Pages_PatientOccupationCategories,
                L("PatientOccupationCategories")
            );
            patientOccupationCategories.CreateChildPermission(
                AppPermissions.Pages_PatientOccupationCategories_Create,
                L("CreateNewPatientOccupationCategory")
            );
            patientOccupationCategories.CreateChildPermission(
                AppPermissions.Pages_PatientOccupationCategories_Edit,
                L("EditPatientOccupationCategory")
            );
            patientOccupationCategories.CreateChildPermission(
                AppPermissions.Pages_PatientOccupationCategories_Delete,
                L("DeletePatientOccupationCategory")
            );

            var organizationUnitTimes = pages.CreateChildPermission(
                AppPermissions.Pages_OrganizationUnitTimes,
                L("OrganizationUnitTimes"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            organizationUnitTimes.CreateChildPermission(
                AppPermissions.Pages_OrganizationUnitTimes_Create,
                L("CreateNewOrganizationUnitTime"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            organizationUnitTimes.CreateChildPermission(
                AppPermissions.Pages_OrganizationUnitTimes_Edit,
                L("EditOrganizationUnitTime"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            organizationUnitTimes.CreateChildPermission(
                AppPermissions.Pages_OrganizationUnitTimes_Delete,
                L("DeleteOrganizationUnitTime"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var facilityDocuments = pages.CreateChildPermission(
                AppPermissions.Pages_FacilityDocuments,
                L("FacilityDocuments"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityDocuments.CreateChildPermission(
                AppPermissions.Pages_FacilityDocuments_Create,
                L("CreateNewFacilityDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityDocuments.CreateChildPermission(
                AppPermissions.Pages_FacilityDocuments_Edit,
                L("EditFacilityDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityDocuments.CreateChildPermission(
                AppPermissions.Pages_FacilityDocuments_Delete,
                L("DeleteFacilityDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var facilityInsurers = pages.CreateChildPermission(
                AppPermissions.Pages_FacilityInsurers,
                L("FacilityInsurers"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityInsurers.CreateChildPermission(
                AppPermissions.Pages_FacilityInsurers_Create,
                L("CreateNewFacilityInsurer"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityInsurers.CreateChildPermission(
                AppPermissions.Pages_FacilityInsurers_Edit,
                L("EditFacilityInsurer"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityInsurers.CreateChildPermission(
                AppPermissions.Pages_FacilityInsurers_Delete,
                L("DeleteFacilityInsurer"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var insuranceProviders = pages.CreateChildPermission(
                AppPermissions.Pages_InsuranceProviders,
                L("InsuranceProviders")
            );
            insuranceProviders.CreateChildPermission(
                AppPermissions.Pages_InsuranceProviders_Create,
                L("CreateNewInsuranceProvider")
            );
            insuranceProviders.CreateChildPermission(
                AppPermissions.Pages_InsuranceProviders_Edit,
                L("EditInsuranceProvider")
            );
            insuranceProviders.CreateChildPermission(
                AppPermissions.Pages_InsuranceProviders_Delete,
                L("DeleteInsuranceProvider")
            );

            var facilityStaff = pages.CreateChildPermission(
                AppPermissions.Pages_FacilityStaff,
                L("FacilityStaff"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityStaff.CreateChildPermission(
                AppPermissions.Pages_FacilityStaff_Create,
                L("CreateNewFacilityStaff"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityStaff.CreateChildPermission(
                AppPermissions.Pages_FacilityStaff_Edit,
                L("EditFacilityStaff"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityStaff.CreateChildPermission(
                AppPermissions.Pages_FacilityStaff_Delete,
                L("DeleteFacilityStaff"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var staffMembers = pages.CreateChildPermission(
                AppPermissions.Pages_StaffMembers,
                L("StaffMembers")
            );
            staffMembers.CreateChildPermission(
                AppPermissions.Pages_StaffMembers_Create,
                L("CreateNewStaffMember")
            );
            staffMembers.CreateChildPermission(
                AppPermissions.Pages_StaffMembers_Edit,
                L("EditStaffMember")
            );
            staffMembers.CreateChildPermission(
                AppPermissions.Pages_StaffMembers_Delete,
                L("DeleteStaffMember")
            );

            var tenantDocuments = pages.CreateChildPermission(
                AppPermissions.Pages_TenantDocuments,
                L("TenantDocuments"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            tenantDocuments.CreateChildPermission(
                AppPermissions.Pages_TenantDocuments_Create,
                L("CreateNewTenantDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            tenantDocuments.CreateChildPermission(
                AppPermissions.Pages_TenantDocuments_Edit,
                L("EditTenantDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            tenantDocuments.CreateChildPermission(
                AppPermissions.Pages_TenantDocuments_Delete,
                L("DeleteTenantDocument"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var wardBeds = pages.CreateChildPermission(
                AppPermissions.Pages_WardBeds,
                L("WardBeds"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            wardBeds.CreateChildPermission(
                AppPermissions.Pages_WardBeds_Create,
                L("CreateNewWardBed"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            wardBeds.CreateChildPermission(
                AppPermissions.Pages_WardBeds_Edit,
                L("EditWardBed"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            wardBeds.CreateChildPermission(
                AppPermissions.Pages_WardBeds_Delete,
                L("DeleteWardBed"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var wards = pages.CreateChildPermission(
                AppPermissions.Pages_Wards,
                L("Wards"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            wards.CreateChildPermission(
                AppPermissions.Pages_Wards_Create,
                L("CreateNewWard"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            wards.CreateChildPermission(
                AppPermissions.Pages_Wards_Edit,
                L("EditWard"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            wards.CreateChildPermission(
                AppPermissions.Pages_Wards_Delete,
                L("DeleteWard"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var bedTypes = pages.CreateChildPermission(
                AppPermissions.Pages_BedTypes,
                L("BedTypes")
            );
            bedTypes.CreateChildPermission(
                AppPermissions.Pages_BedTypes_Create,
                L("CreateNewBedType")
            );
            bedTypes.CreateChildPermission(
                AppPermissions.Pages_BedTypes_Edit,
                L("EditBedType")
            );
            bedTypes.CreateChildPermission(
                AppPermissions.Pages_BedTypes_Delete,
                L("DeleteBedType")
            );

            var facilityTypes = pages.CreateChildPermission(
                AppPermissions.Pages_FacilityTypes,
                L("FacilityTypes")
            );
            facilityTypes.CreateChildPermission(
                AppPermissions.Pages_FacilityTypes_Create,
                L("CreateNewFacilityType")
            );
            facilityTypes.CreateChildPermission(
                AppPermissions.Pages_FacilityTypes_Edit,
                L("EditFacilityType")
            );
            facilityTypes.CreateChildPermission(
                AppPermissions.Pages_FacilityTypes_Delete,
                L("DeleteFacilityType")
            );

            var facilities = pages.CreateChildPermission(
                AppPermissions.Pages_Facilities,
                L("Facilities"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilities.CreateChildPermission(
                AppPermissions.Pages_Facilities_Create,
                L("CreateNewFacility"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilities.CreateChildPermission(
                AppPermissions.Pages_Facilities_Edit,
                L("EditFacility"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilities.CreateChildPermission(
                AppPermissions.Pages_Facilities_Delete,
                L("DeleteFacility"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var facilityGroups = pages.CreateChildPermission(
                AppPermissions.Pages_FacilityGroups,
                L("FacilityGroups"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityGroups.CreateChildPermission(
                AppPermissions.Pages_FacilityGroups_Create,
                L("CreateNewFacilityGroup"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityGroups.CreateChildPermission(
                AppPermissions.Pages_FacilityGroups_Edit,
                L("EditFacilityGroup"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            facilityGroups.CreateChildPermission(
                AppPermissions.Pages_FacilityGroups_Delete,
                L("DeleteFacilityGroup"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var staffCodeTemplates = pages.CreateChildPermission(
                AppPermissions.Pages_StaffCodeTemplates,
                L("StaffCodeTemplates"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            staffCodeTemplates.CreateChildPermission(
                AppPermissions.Pages_StaffCodeTemplates_Create,
                L("CreateNewStaffCodeTemplate"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            staffCodeTemplates.CreateChildPermission(
                AppPermissions.Pages_StaffCodeTemplates_Edit,
                L("EditStaffCodeTemplate"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            staffCodeTemplates.CreateChildPermission(
                AppPermissions.Pages_StaffCodeTemplates_Delete,
                L("DeleteStaffCodeTemplate"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var patientCodeTemplates = pages.CreateChildPermission(
                AppPermissions.Pages_PatientCodeTemplates,
                L("PatientCodeTemplates"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientCodeTemplates.CreateChildPermission(
                AppPermissions.Pages_PatientCodeTemplates_Create,
                L("CreateNewPatientCodeTemplate"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientCodeTemplates.CreateChildPermission(
                AppPermissions.Pages_PatientCodeTemplates_Edit,
                L("EditPatientCodeTemplate"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            patientCodeTemplates.CreateChildPermission(
                AppPermissions.Pages_PatientCodeTemplates_Delete,
                L("DeletePatientCodeTemplate"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var jobLevels = pages.CreateChildPermission(
                AppPermissions.Pages_JobLevels,
                L("JobLevels")
            );
            jobLevels.CreateChildPermission(
                AppPermissions.Pages_JobLevels_Create,
                L("CreateNewJobLevel")
            );
            jobLevels.CreateChildPermission(
                AppPermissions.Pages_JobLevels_Edit,
                L("EditJobLevel")
            );
            jobLevels.CreateChildPermission(
                AppPermissions.Pages_JobLevels_Delete,
                L("DeleteJobLevel")
            );

            var jobTitles = pages.CreateChildPermission(
                AppPermissions.Pages_JobTitles,
                L("JobTitles")
            );
            jobTitles.CreateChildPermission(
                AppPermissions.Pages_JobTitles_Create,
                L("CreateNewJobTitle")
            );
            jobTitles.CreateChildPermission(
                AppPermissions.Pages_JobTitles_Edit,
                L("EditJobTitle")
            );
            jobTitles.CreateChildPermission(
                AppPermissions.Pages_JobTitles_Delete,
                L("DeleteJobTitle")
            );

            pages.CreateChildPermission(
                AppPermissions.Pages_DemoUiComponents,
                L("DemoUiComponents")
            );

            var administration = pages.CreateChildPermission(
                AppPermissions.Pages_Administration,
                L("Administration")
            );

            var roles = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Roles,
                L("Roles")
            );
            roles.CreateChildPermission(
                AppPermissions.Pages_Administration_Roles_Create,
                L("CreatingNewRole")
            );
            roles.CreateChildPermission(
                AppPermissions.Pages_Administration_Roles_Edit,
                L("EditingRole")
            );
            roles.CreateChildPermission(
                AppPermissions.Pages_Administration_Roles_Delete,
                L("DeletingRole")
            );

            var users = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Users,
                L("Users")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_Create,
                L("CreatingNewUser")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_Edit,
                L("EditingUser")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_Delete,
                L("DeletingUser")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_ChangePermissions,
                L("ChangingPermissions")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_Impersonation,
                L("LoginForUsers")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_Unlock,
                L("Unlock")
            );
            users.CreateChildPermission(
                AppPermissions.Pages_Administration_Users_ChangeProfilePicture,
                L("UpdateUsersProfilePicture")
            );

            var languages = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Languages,
                L("Languages")
            );
            languages.CreateChildPermission(
                AppPermissions.Pages_Administration_Languages_Create,
                L("CreatingNewLanguage"),
                multiTenancySides: _isMultiTenancyEnabled
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant
            );
            languages.CreateChildPermission(
                AppPermissions.Pages_Administration_Languages_Edit,
                L("EditingLanguage"),
                multiTenancySides: _isMultiTenancyEnabled
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant
            );
            languages.CreateChildPermission(
                AppPermissions.Pages_Administration_Languages_Delete,
                L("DeletingLanguages"),
                multiTenancySides: _isMultiTenancyEnabled
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant
            );
            languages.CreateChildPermission(
                AppPermissions.Pages_Administration_Languages_ChangeTexts,
                L("ChangingTexts")
            );
            languages.CreateChildPermission(
                AppPermissions.Pages_Administration_Languages_ChangeDefaultLanguage,
                L("ChangeDefaultLanguage")
            );

            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_AuditLogs,
                L("AuditLogs")
            );

            var organizationUnits = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_OrganizationUnits,
                L("OrganizationUnits")
            );
            organizationUnits.CreateChildPermission(
                AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree,
                L("ManagingOrganizationTree")
            );
            organizationUnits.CreateChildPermission(
                AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers,
                L("ManagingMembers")
            );
            organizationUnits.CreateChildPermission(
                AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles,
                L("ManagingRoles")
            );

            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_UiCustomization,
                L("VisualSettings")
            );

            var webhooks = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_WebhookSubscription,
                L("Webhooks")
            );
            webhooks.CreateChildPermission(
                AppPermissions.Pages_Administration_WebhookSubscription_Create,
                L("CreatingWebhooks")
            );
            webhooks.CreateChildPermission(
                AppPermissions.Pages_Administration_WebhookSubscription_Edit,
                L("EditingWebhooks")
            );
            webhooks.CreateChildPermission(
                AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity,
                L("ChangingWebhookActivity")
            );
            webhooks.CreateChildPermission(
                AppPermissions.Pages_Administration_WebhookSubscription_Detail,
                L("DetailingSubscription")
            );
            webhooks.CreateChildPermission(
                AppPermissions.Pages_Administration_Webhook_ListSendAttempts,
                L("ListingSendAttempts")
            );
            webhooks.CreateChildPermission(
                AppPermissions.Pages_Administration_Webhook_ResendWebhook,
                L("ResendingWebhook")
            );

            var dynamicProperties = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicProperties,
                L("DynamicProperties")
            );
            dynamicProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicProperties_Create,
                L("CreatingDynamicProperties")
            );
            dynamicProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicProperties_Edit,
                L("EditingDynamicProperties")
            );
            dynamicProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicProperties_Delete,
                L("DeletingDynamicProperties")
            );

            var dynamicPropertyValues = dynamicProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicPropertyValue,
                L("DynamicPropertyValue")
            );
            dynamicPropertyValues.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicPropertyValue_Create,
                L("CreatingDynamicPropertyValue")
            );
            dynamicPropertyValues.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicPropertyValue_Edit,
                L("EditingDynamicPropertyValue")
            );
            dynamicPropertyValues.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicPropertyValue_Delete,
                L("DeletingDynamicPropertyValue")
            );

            var dynamicEntityProperties = dynamicProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityProperties,
                L("DynamicEntityProperties")
            );
            dynamicEntityProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityProperties_Create,
                L("CreatingDynamicEntityProperties")
            );
            dynamicEntityProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityProperties_Edit,
                L("EditingDynamicEntityProperties")
            );
            dynamicEntityProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityProperties_Delete,
                L("DeletingDynamicEntityProperties")
            );

            var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityPropertyValue,
                L("EntityDynamicPropertyValue")
            );
            dynamicEntityPropertyValues.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create,
                L("CreatingDynamicEntityPropertyValue")
            );
            dynamicEntityPropertyValues.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit,
                L("EditingDynamicEntityPropertyValue")
            );
            dynamicEntityPropertyValues.CreateChildPermission(
                AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete,
                L("DeletingDynamicEntityPropertyValue")
            );

            var massNotification = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_MassNotification,
                L("MassNotifications")
            );
            massNotification.CreateChildPermission(
                AppPermissions.Pages_Administration_MassNotification_Create,
                L("MassNotificationCreate")
            );

            var editions = pages.CreateChildPermission(
                AppPermissions.Pages_Editions,
                L("Editions")
            );
            editions.CreateChildPermission(
                AppPermissions.Pages_Editions_Create,
                L("CreatingNewEdition")
            );
            editions.CreateChildPermission(
                AppPermissions.Pages_Editions_Edit,
                L("EditingEdition")
            );

            var investigations = pages.CreateChildPermission(
                AppPermissions.Pages_Investigations,
                L("Investigations")
            );

            pages.CreateChildPermission(
                AppPermissions.Pages_WardEmergencies,
                L("WardEmergencies")
            );

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(
                AppPermissions.Pages_Tenant_Dashboard,
                L("Dashboard"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Tenant_Settings,
                L("Settings"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Tenant_SubscriptionManagement,
                L("Subscription"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            //HOST-SPECIFIC PERMISSIONS

            editions.CreateChildPermission(
                AppPermissions.Pages_Editions_Delete,
                L("DeletingEdition"),
                multiTenancySides: MultiTenancySides.Host
            );
            editions.CreateChildPermission(
                AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition,
                L("MoveTenantsToAnotherEdition"),
                multiTenancySides: MultiTenancySides.Host
            );

            var tenants = pages.CreateChildPermission(
                AppPermissions.Pages_Tenants,
                L("Tenants"),
                multiTenancySides: MultiTenancySides.Host
            );
            tenants.CreateChildPermission(
                AppPermissions.Pages_Tenants_Create,
                L("CreatingNewTenant"),
                multiTenancySides: MultiTenancySides.Host
            );
            tenants.CreateChildPermission(
                AppPermissions.Pages_Tenants_Edit,
                L("EditingTenant"),
                multiTenancySides: MultiTenancySides.Host
            );
            tenants.CreateChildPermission(
                AppPermissions.Pages_Tenants_ChangeFeatures,
                L("ChangingFeatures"),
                multiTenancySides: MultiTenancySides.Host
            );
            tenants.CreateChildPermission(
                AppPermissions.Pages_Tenants_Delete,
                L("DeletingTenant"),
                multiTenancySides: MultiTenancySides.Host
            );
            tenants.CreateChildPermission(
                AppPermissions.Pages_Tenants_Impersonation,
                L("LoginForTenants"),
                multiTenancySides: MultiTenancySides.Host
            );

            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Host_Settings,
                L("Settings"),
                multiTenancySides: MultiTenancySides.Host
            );

            var maintenance = administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Host_Maintenance,
                L("Maintenance"),
                multiTenancySides: _isMultiTenancyEnabled
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant
            );
            maintenance.CreateChildPermission(
                AppPermissions.Pages_Administration_NewVersion_Create,
                L("SendNewVersionNotification")
            );

            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_HangfireDashboard,
                L("HangfireDashboard"),
                multiTenancySides: _isMultiTenancyEnabled
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant
            );
            administration.CreateChildPermission(
                AppPermissions.Pages_Administration_Host_Dashboard,
                L("Dashboard"),
                multiTenancySides: MultiTenancySides.Host
            );
            
            
            var symtoms = pages.CreateChildPermission(
                AppPermissions.Pages_Symptoms,
                L("Symptoms"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            symtoms.CreateChildPermission(
                AppPermissions.Pages_Symptoms_Create,
                L("CreateSymptom"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            symtoms.CreateChildPermission(
                AppPermissions.Pages_Symptoms_Delete,
                L("DeleteSymptom"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            symtoms.CreateChildPermission(
                AppPermissions.Pages_Symptoms_Edit,
                L("EditSymptom"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var bedMaking = pages.CreateChildPermission(
                AppPermissions.Pages_BedMaking,
                L("BedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            bedMaking.CreateChildPermission(
                AppPermissions.Pages_BedMaking_Create,
                L("CreateBedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            bedMaking.CreateChildPermission(
                AppPermissions.Pages_BedMaking_Delete,
                L("DeleteBedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            bedMaking.CreateChildPermission(
                AppPermissions.Pages_BedMaking_Edit,
                L("EditBedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            var feeding = pages.CreateChildPermission(
                AppPermissions.Pages_Feeding,
                L("Feeding"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            feeding.CreateChildPermission(
                AppPermissions.Pages_Feeding_Create,
                L("CreateFeeding"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            feeding.CreateChildPermission(
                AppPermissions.Pages_Feeding_Delete,
                L("DeleteFeeding"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            feeding.CreateChildPermission(
                AppPermissions.Pages_Feeding_Edit,
                L("EditFeeding"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            var planItems = pages.CreateChildPermission(
                AppPermissions.Pages_PlanItems,
                L("BedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            planItems.CreateChildPermission(
                AppPermissions.Pages_PlanItems_Create,
                L("CreateBedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            planItems.CreateChildPermission(
                AppPermissions.Pages_PlanItems_Edit,
                L("DeleteBedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            planItems.CreateChildPermission(
                AppPermissions.Pages_PlanItems_Delete,
                L("EditBedMaking"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            var woundDressing = pages.CreateChildPermission(
                AppPermissions.Pages_WoundDressing,
                L("WoundDressing"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            woundDressing.CreateChildPermission(
                AppPermissions.Pages_WoundDressing_Create,
                L("CreateWoundDressing"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            woundDressing.CreateChildPermission(
                AppPermissions.Pages_WoundDressing_Edit,
                L("DeleteWoundDressing"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            woundDressing.CreateChildPermission(
                AppPermissions.Pages_WoundDressing_Delete,
                L("EditWoundDressing"),
                multiTenancySides: MultiTenancySides.Tenant
            );
                
            var meals = pages.CreateChildPermission(
                AppPermissions.Pages_Meals,
                L("Meals"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            meals.CreateChildPermission(
                AppPermissions.Pages_Meals_Create,
                L("Meals"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            meals.CreateChildPermission(
                AppPermissions.Pages_Meals_Edit,
                L("Meals"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            meals.CreateChildPermission(
                AppPermissions.Pages_Meals_Delete,
                L("Meals"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            var inputNotes = pages.CreateChildPermission(
                AppPermissions.Pages_InputNotes,
                L("InputNotes"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            inputNotes.CreateChildPermission(
                AppPermissions.Pages_InputNotes_Create,
                L("CreateInputNotes"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            inputNotes.CreateChildPermission(
                AppPermissions.Pages_InputNotes_Edit,
                L("DeleteInputNotes"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            inputNotes.CreateChildPermission(
                AppPermissions.Pages_InputNotes_Delete,
                L("EditInputNotes"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            
            var diagnosis = pages.CreateChildPermission(
               AppPermissions.Pages_Diagnosis,
               L("Diagnosis"),
              multiTenancySides: MultiTenancySides.Tenant
           );

            diagnosis.CreateChildPermission(
                AppPermissions.Pages_Diagnosis_Create,
                L("CreateDiagnosis"),
                multiTenancySides: MultiTenancySides.Tenant
                );
            diagnosis.CreateChildPermission(
                AppPermissions.Pages_Diagnosis_Edit,
                L("EditDiagnosis"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            diagnosis.CreateChildPermission(
               AppPermissions.Pages_Diagnosis_Delete,
               L("DeleteDiagnosis"),
               multiTenancySides: MultiTenancySides.Tenant
           );

            var rooms = pages.CreateChildPermission(
               AppPermissions.Pages_Rooms,
               L("Rooms"),
              multiTenancySides: MultiTenancySides.Tenant
           );

            rooms.CreateChildPermission(
                AppPermissions.Pages_Rooms_Create,
                L("CreateRooms"),
                multiTenancySides: MultiTenancySides.Tenant
                );
            rooms.CreateChildPermission(
                AppPermissions.Pages_Rooms_Edit,
                L("EditRooms"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            rooms.CreateChildPermission(
               AppPermissions.Pages_Rooms_Delete,
               L("DeleteRooms"),
               multiTenancySides: MultiTenancySides.Tenant
           );
            
            
            var medications = pages.CreateChildPermission(
                AppPermissions.Pages_Medications,
                L("Medications"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            medications.CreateChildPermission(
                AppPermissions.Pages_Medications_Create,
                L("CreateMedication"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            medications.CreateChildPermission(
                AppPermissions.Pages_Medications_Delete,
                L("DeleteMedication"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            medications.CreateChildPermission(
                AppPermissions.Pages_Medications_Edit,
                L("EditMedication"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var facilityBankDetails = pages.CreateChildPermission(
                AppPermissions.Pages_FacilityBanks,
                L("FacilityBankDetails"),
                 multiTenancySides : MultiTenancySides.Tenant
                );

            facilityBankDetails.CreateChildPermission(
                AppPermissions.Pages_FacilityBanks_Create,
                L("CreateFacilityBankDetials"),
                multiTenancySides : MultiTenancySides.Tenant
                );

            facilityBankDetails.CreateChildPermission(
                AppPermissions.Pages_FacilityBanks_Edit,
                L("EditFacilityBankDetails"),
                multiTenancySides : MultiTenancySides.Tenant
                );

            facilityBankDetails.CreateChildPermission(
                AppPermissions.Pages_FacilityBanks_Delete,
                L("DeleteFacilityBankDetails"),
                multiTenancySides: MultiTenancySides.Tenant
                );

            var contactSales = pages.CreateChildPermission(
                AppPermissions.Pages_ContactSales,
                L("ContactSales"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            contactSales.CreateChildPermission(
                AppPermissions.Pages_ContactSales_Create,
                L("CreateContact"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var intakeOutputs = pages.CreateChildPermission(
                AppPermissions.Pages_IntakeOutput, 
                L("IntakeOutput"), 
                multiTenancySides: MultiTenancySides.Tenant
            );
            intakeOutputs.CreateChildPermission(
                AppPermissions.Pages_IntakeOutput_Create, 
                L("CreateNewIntakeOutput"), 
                multiTenancySides: MultiTenancySides.Tenant
            );
            intakeOutputs.CreateChildPermission(
                AppPermissions.Pages_IntakeOutput_Delete,
                L("DeleteIntakeOutput"),
                multiTenancySides: MultiTenancySides.Tenant
            );


            var nextAppointments = pages.CreateChildPermission(
                AppPermissions.Pages_NextAppointment,
                L("NextAppointment"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            nextAppointments.CreateChildPermission(
                AppPermissions.Pages_NextAppointment_Create,
                L("CreateNewNextAppointment"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            nextAppointments.CreateChildPermission(
                AppPermissions.Pages_NextAppointment_Delete,
                L("DeleteNextAppointment"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var doctorReviewAndSave = pages.CreateChildPermission(
                AppPermissions.Pages_DoctorReviewAndSave,
                L("DoctorReviewAndSave"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var nurseReviewAndSave = pages.CreateChildPermission(
                AppPermissions.Pages_NurseReviewAndSave,
                L("NurseReviewAndSave"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            nurseReviewAndSave.CreateChildPermission(
                AppPermissions.Pages_NurseReviewAndSave_Create,
                L("CreateNursingRecords"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            

            var reviewDetailedHistory = pages.CreateChildPermission(
                AppPermissions.Pages_ReviewDetailedHistory,
                L("ReviewDetailedHistory"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            reviewDetailedHistory.CreateChildPermission(
                AppPermissions.Pages_ReviewDetailedHistory_Save,
                L("CreateReviewDetailedHistory"),
                multiTenancySides: MultiTenancySides.Tenant
            );

            var referAndConsult = pages.CreateChildPermission(
                AppPermissions.Pages_ReferConsult,
                L("ReferConsult"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            referAndConsult.CreateChildPermission(
                AppPermissions.Pages_ReferConsult_Create,
                L("CreateReferConsult"),
                multiTenancySides: MultiTenancySides.Tenant
            );
            referAndConsult.CreateChildPermission(
                AppPermissions.Pages_ReferConsult_Delete,
                L("DeleteReferConsult"),
                multiTenancySides: MultiTenancySides.Tenant
            );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, EHRConsts.LocalizationSourceName);
        }
    }
}