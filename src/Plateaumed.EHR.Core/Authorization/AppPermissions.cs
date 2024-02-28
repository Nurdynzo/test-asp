namespace Plateaumed.EHR.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_Invoices = "Pages.Invoices";
        public const string Pages_Invoices_Create = "Pages.Invoices.Create";
        public const string Pages_Invoices_Edit = "Pages.Invoices.Edit";
        public const string Pages_Invoices_Delete = "Pages.Invoices.Delete";

        public const string Pages_InvoiceItems = "Pages.InvoiceItems";
        public const string Pages_InvoiceItems_Create = "Pages.InvoiceItems.Create";
        public const string Pages_InvoiceItems_Edit = "Pages.InvoiceItems.Edit";
        public const string Pages_InvoiceItems_Delete = "Pages.InvoiceItems.Delete";

        public const string Pages_PatientAppointments = "Pages.PatientAppointments";
        public const string Pages_PatientAppointments_Create = "Pages.PatientAppointments.Create";
        public const string Pages_PatientAppointments_Edit = "Pages.PatientAppointments.Edit";
        public const string Pages_PatientAppointments_Delete = "Pages.PatientAppointments.Delete";
        
        public const string Pages_Patient_Profiles = "Pages.Patient_Profiles";
        public const string Pages_Patient_Profiles_Create = "Pages.Patient_Profiles.Create";
        public const string Pages_Patient_Profiles_Edit = "Pages.Patient_Profiles.Edit";
        public const string Pages_Patient_Profiles_Delete = "Pages.Patient_Profiles.Delete";
        public const string Pages_Patient_Profiles_View = "Pages.Patient_Profiles.View";

        public const string Pages_Patient_Profiles_FamilyHistory_View = "Pages.Patient_Profiles.FamilyHistory_View";
        public const string Pages_Patient_Profiles_FamilyHistory_Create = "Pages.Patient_Profiles.FamilyHistory_Create";
        public const string Pages_Patient_Profiles_FamilyHistory_Delete = "Pages.Patient_Profiles.FamilyHistory_Delete";

        public const string Pages_Patient_Profiles_OccupationHistory_View = "Pages.Patient_Profiles.OccupationHistory_View";
        public const string Pages_Patient_Profiles_OccupationHistory_Create = "Pages.Patient_Profiles.OccupationHistory_Create";
        public const string Pages_Patient_Profiles_OccupationHistory_Delete = "Pages.Patient_Profiles.OccupationHistory_Delete";
        public const string Pages_Patient_Profiles_OccupationHistory_Edit = "Pages.Patient_Profiles.OccupationHistory_Edit";

        public const string Pages_Patient_Profiles_BloodGroup_View = "Pages.Patient_Profiles.BloodGroup_View";
        public const string Pages_Patient_Profiles_BloodGroup_Create = "Pages.Patient_Profiles.BloodGroup_Create";

        public const string Pages_Patient_Profiles_Physical_Exercise_View = "Pages.Patient_Profiles.Physical_Exercise_View";
        public const string Pages_Patient_Profiles_Physical_Exercise_Create = "Pages.Patient_Profiles.Physical_Exercise_Create";

        public const string Pages_Patient_Profiles_Chronic_Condition_View = "Pages.Patient_Profiles.Chronic_Condition_View";
        public const string Pages_Patient_Profiles_Chronic_Condition_Create = "Pages.Patient_Profiles.Chronic_Condition_Create";
        public const string Pages_Patient_Profiles_Chronic_Condition_Delete = "Pages.Patient_Profiles.Chronic_Condition_Delete";

        public const string Pages_Patient_Profiles_Travel_History_View = "Pages.Patient_Profiles.Travel_History_View";
        public const string Pages_Patient_Profiles_Travel_History_Create = "Pages.Patient_Profiles.Travel_History_Create";
        public const string Pages_Patient_Profiles_Travel_History_Delete = "Pages.Patient_Profiles.Travel_History_Delete";

        public const string Pages_Patient_Profiles_Surgical_History_View = "Pages.Patient_Profiles.Surgical_History_View";
        public const string Pages_Patient_Profiles_Surgical_History_Create = "Pages.Patient_Profiles.Surgical_History_Create";
        public const string Pages_Patient_Profiles_Surgical_History_Delete = "Pages.Patient_Profiles.Surgical_History_Delete";
        public const string Pages_Patient_Profiles_Surgical_History_Edit = "Pages.Patient_Profiles.Surgical_History_Edit";

        public const string Pages_Patient_Profiles_Vaccination_History_View = "Pages.Patient_Profiles.Vaccination_History_View";
        public const string Pages_Patient_Profiles_Vaccination_History_Create = "Pages.Patient_Profiles.Vaccination_History_Create";
        public const string Pages_Patient_Profiles_Vaccination_History_Delete = "Pages.Patient_Profiles.Vaccination_History_Delete";

        public const string Pages_Patient_Profiles_Drug_History_View = "Pages.Patient_Profiles.Drug_History_View";
        public const string Pages_Patient_Profiles_Drug_History_Create = "Pages.Patient_Profiles.Drug_History_Create";
        public const string Pages_Patient_Profiles_Drug_History_Delete = "Pages.Patient_Profiles.Drug_History_Delete";
        public const string Pages_Patient_Profiles_Drug_History_Edit = "Pages.Patient_Profiles.Drug_History_Edit";

        public const string Pages_Patient_Profiles_Allergy_History_View = "Pages.Patient_Profiles.Allergy_History_View";
        public const string Pages_Patient_Profiles_Allergy_History_Create = "Pages.Patient_Profiles.Allergy_History_Create";
        public const string Pages_Patient_Profiles_Allergy_History_Delete = "Pages.Patient_Profiles.Allergy_History_Delete";
        public const string Pages_Patient_Profiles_Allergy_History_Edit = "Pages.Patient_Profiles.Allergy_History_Edit";

        public const string Pages_Patient_Profiles_Implant_View = "Pages.Patient_Profiles.Implant_View";
        public const string Pages_Patient_Profiles_Implant_Create = "Pages.Patient_Profiles.Implant_Create";
        public const string Pages_Patient_Profiles_Implant_Delete = "Pages.Patient_Profiles.Implant_Delete";
        public const string Pages_Patient_Profiles_Implant_Edit = "Pages.Patient_Profiles.Implant_Edit";

        public const string Pages_Patient_Profiles_Review_of_System_View = "Pages.Patient_Profiles.Review_of_System_View";
        public const string Pages_Patient_Profiles_Review_of_System_Create = "Pages.Patient_Profiles.Review_of_System_Create";
        public const string Pages_Patient_Profiles_Review_of_System_Delete = "Pages.Patient_Profiles.Review_of_System_Delete";
        public const string Pages_Patient_Profiles_Review_of_System_Edit = "Pages.Patient_Profiles.Review_of_System_Edit";

        public const string Pages_Patient_Profiles_Treatement_Plan_View = "Pages.Patient_Profiles.Treatement_Plan_View";

        public const string Pages_Patient_Profiles_Clinical_Investigation_View = "Pages.Patient_Profiles.Clinical_Investigation_View";

        public const string Pages_PatientReferralDocuments = "Pages.PatientReferralDocuments";
        public const string Pages_PatientReferralDocuments_Create =
            "Pages.PatientReferralDocuments.Create";
        public const string Pages_PatientReferralDocuments_Edit = "Pages.PatientReferralDocuments.Edit";
        public const string Pages_PatientReferralDocuments_Delete =
            "Pages.PatientReferralDocuments.Delete";
        public const string Pages_ScanDocument_Review = "Pages.ScanDocument.Review";
        public const string Pages_ScanDocument_Upload = "Pages.ScanDocument.Upload";
        public const string Pages_ScanDocument = "Pages.ScanDocument";

        public const string Pages_PatientInsurers = "Pages.PatientInsurers";
        public const string Pages_PatientInsurers_Create = "Pages.PatientInsurers.Create";
        public const string Pages_PatientInsurers_Edit = "Pages.PatientInsurers.Edit";
        public const string Pages_PatientInsurers_Delete = "Pages.PatientInsurers.Delete";

        public const string Pages_PatientRelations = "Pages.PatientRelations";
        public const string Pages_PatientRelations_Create = "Pages.PatientRelations.Create";
        public const string Pages_PatientRelations_Edit = "Pages.PatientRelations.Edit";
        public const string Pages_PatientRelations_Delete = "Pages.PatientRelations.Delete";

        public const string Pages_Patients = "Pages.Patients";
        public const string Pages_Patients_Create = "Pages.Patients.Create";
        public const string Pages_Patients_Edit = "Pages.Patients.Edit";
        public const string Pages_Patients_Delete = "Pages.Patients.Delete";

        public const string Pages_Countries = "Pages.Countries";
        public const string Pages_Countries_Create = "Pages.Countries.Create";
        public const string Pages_Countries_Edit = "Pages.Countries.Edit";
        public const string Pages_Countries_Delete = "Pages.Countries.Delete";

        public const string Pages_Regions = "Pages.Regions";
        public const string Pages_Regions_Create = "Pages.Regions.Create";
        public const string  Pages_Regions_Edit = "Pages.Regions.Edit";
        public const string Pages_Regions_Delete = "Pages.Regions.Delete";

        public const string Pages_Districts = "Pages.Districts";
        public const string Pages_Districts_Create = "Pages.Districts.Create";
        public const string Pages_Districts_Edit = "Pages.Districts.Edit";
        public const string Pages_Districts_Delete = "Pages.Districts.Delete";

        public const string Pages_PatientOccupations = "Pages.PatientOccupations";
        public const string Pages_PatientOccupations_Create = "Pages.PatientOccupations.Create";
        public const string Pages_PatientOccupations_Edit = "Pages.PatientOccupations.Edit";
        public const string Pages_PatientOccupations_Delete = "Pages.PatientOccupations.Delete";

        public const string Pages_PatientOccupationCategories = "Pages.PatientOccupationCategories";
        public const string Pages_PatientOccupationCategories_Create =
            "Pages.PatientOccupationCategories.Create";
        public const string Pages_PatientOccupationCategories_Edit =
            "Pages.PatientOccupationCategories.Edit";
        public const string Pages_PatientOccupationCategories_Delete =
            "Pages.PatientOccupationCategories.Delete";

        public const string Pages_OrganizationUnitTimes = "Pages.OrganizationUnitTimes";
        public const string Pages_OrganizationUnitTimes_Create =
            "Pages.OrganizationUnitTimes.Create";
        public const string Pages_OrganizationUnitTimes_Edit = "Pages.OrganizationUnitTimes.Edit";
        public const string Pages_OrganizationUnitTimes_Delete =
            "Pages.OrganizationUnitTimes.Delete";

        public const string Pages_FacilityDocuments = "Pages.FacilityDocuments";
        public const string Pages_FacilityDocuments_Create = "Pages.FacilityDocuments.Create";
        public const string Pages_FacilityDocuments_Edit = "Pages.FacilityDocuments.Edit";
        public const string Pages_FacilityDocuments_Delete = "Pages.FacilityDocuments.Delete";

        public const string Pages_FacilityInsurers = "Pages.FacilityInsurers";
        public const string Pages_FacilityInsurers_Create = "Pages.FacilityInsurers.Create";
        public const string Pages_FacilityInsurers_Edit = "Pages.FacilityInsurers.Edit";
        public const string Pages_FacilityInsurers_Delete = "Pages.FacilityInsurers.Delete";

        public const string Pages_InsuranceProviders = "Pages.InsuranceProviders";
        public const string Pages_InsuranceProviders_Create = "Pages.InsuranceProviders.Create";
        public const string Pages_InsuranceProviders_Edit = "Pages.InsuranceProviders.Edit";
        public const string Pages_InsuranceProviders_Delete = "Pages.InsuranceProviders.Delete";

        public const string Pages_FacilityStaff = "Pages.FacilityStaff";
        public const string Pages_FacilityStaff_Create = "Pages.FacilityStaff.Create";
        public const string Pages_FacilityStaff_Edit = "Pages.FacilityStaff.Edit";
        public const string Pages_FacilityStaff_Delete = "Pages.FacilityStaff.Delete";

        public const string Pages_StaffMembers = "Pages.StaffMembers";
        public const string Pages_StaffMembers_Create = "Pages.StaffMembers.Create";
        public const string Pages_StaffMembers_Edit = "Pages.StaffMembers.Edit";
        public const string Pages_StaffMembers_Delete = "Pages.StaffMembers.Delete";

        public const string Pages_TenantDocuments = "Pages.TenantDocuments";
        public const string Pages_TenantDocuments_Create = "Pages.TenantDocuments.Create";
        public const string Pages_TenantDocuments_Edit = "Pages.TenantDocuments.Edit";
        public const string Pages_TenantDocuments_Delete = "Pages.TenantDocuments.Delete";

        public const string Pages_WardBeds = "Pages.WardBeds";
        public const string Pages_WardBeds_Create = "Pages.WardBeds.Create";
        public const string Pages_WardBeds_Edit = "Pages.WardBeds.Edit";
        public const string Pages_WardBeds_Delete = "Pages.WardBeds.Delete";

        public const string Pages_Wards = "Pages.Wards";
        public const string Pages_Wards_Create = "Pages.Wards.Create";
        public const string Pages_Wards_Edit = "Pages.Wards.Edit";
        public const string Pages_Wards_Delete = "Pages.Wards.Delete";

        public const string Pages_BedTypes = "Pages.BedTypes";
        public const string Pages_BedTypes_Create = "Pages.BedTypes.Create";
        public const string Pages_BedTypes_Edit = "Pages.BedTypes.Edit";
        public const string Pages_BedTypes_Delete = "Pages.BedTypes.Delete";

        public const string Pages_Facilities = "Pages.Facilities";
        public const string Pages_Facilities_Create = "Pages.Facilities.Create";
        public const string Pages_Facilities_Edit = "Pages.Facilities.Edit";
        public const string Pages_Facilities_Delete = "Pages.Facilities.Delete";

        public const string Pages_FacilityBanks = "Pages_FacilityBanks";
        public const string Pages_FacilityBanks_Create = "Pages_FacilityBanks.Create";
        public const string Pages_FacilityBanks_Edit = "Pages_FacilityBanks.Edit";
        public const string Pages_FacilityBanks_Delete = "Pages_FacilityBanks.Delete";

        public const string Pages_FacilityGroups = "Pages.FacilityGroups";
        public const string Pages_FacilityGroups_Create = "Pages.FacilityGroups.Create";
        public const string Pages_FacilityGroups_Edit = "Pages.FacilityGroups.Edit";
        public const string Pages_FacilityGroups_Delete = "Pages.FacilityGroups.Delete";

        public const string Pages_FacilityTypes = "Pages.FacilityTypes";
        public const string Pages_FacilityTypes_Create = "Pages.FacilityTypes.Create";
        public const string Pages_FacilityTypes_Edit = "Pages.FacilityTypes.Edit";
        public const string Pages_FacilityTypes_Delete = "Pages.FacilityTypes.Delete";

        public const string Pages_StaffCodeTemplates = "Pages.StaffCodeTemplates";
        public const string Pages_StaffCodeTemplates_Create = "Pages.StaffCodeTemplates.Create";
        public const string Pages_StaffCodeTemplates_Edit = "Pages.StaffCodeTemplates.Edit";
        public const string Pages_StaffCodeTemplates_Delete = "Pages.StaffCodeTemplates.Delete";

        public const string Pages_PatientCodeTemplates = "Pages.PatientCodeTemplates";
        public const string Pages_PatientCodeTemplates_Create = "Pages.PatientCodeTemplates.Create";
        public const string Pages_PatientCodeTemplates_Edit = "Pages.PatientCodeTemplates.Edit";
        public const string Pages_PatientCodeTemplates_Delete = "Pages.PatientCodeTemplates.Delete";

        public const string Pages_JobLevels = "Pages.JobLevels";
        public const string Pages_JobLevels_Create = "Pages.JobLevels.Create";
        public const string Pages_JobLevels_Edit = "Pages.JobLevels.Edit";
        public const string Pages_JobLevels_Delete = "Pages.JobLevels.Delete";

        public const string Pages_JobTitles = "Pages.JobTitles";
        public const string Pages_JobTitles_Create = "Pages.JobTitles.Create";
        public const string Pages_JobTitles_Edit = "Pages.JobTitles.Edit";
        public const string Pages_JobTitles_Delete = "Pages.JobTitles.Delete";

        public const string Pages_Investigations = "Pages.Investigations";

        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents = "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions =
            "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation =
            "Pages.Administration.Users.Impersonation";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";
        public const string Pages_Administration_Users_ChangeProfilePicture =
            "Pages.Administration.Users.ChangeProfilePicture";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create =
            "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit =
            "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete =
            "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts =
            "Pages.Administration.Languages.ChangeTexts";
        public const string Pages_Administration_Languages_ChangeDefaultLanguage =
            "Pages.Administration.Languages.ChangeDefaultLanguage";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits =
            "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree =
            "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers =
            "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles =
            "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard =
            "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization =
            "Pages.Administration.UiCustomization";

        public const string Pages_Administration_WebhookSubscription =
            "Pages.Administration.WebhookSubscription";
        public const string Pages_Administration_WebhookSubscription_Create =
            "Pages.Administration.WebhookSubscription.Create";
        public const string Pages_Administration_WebhookSubscription_Edit =
            "Pages.Administration.WebhookSubscription.Edit";
        public const string Pages_Administration_WebhookSubscription_ChangeActivity =
            "Pages.Administration.WebhookSubscription.ChangeActivity";
        public const string Pages_Administration_WebhookSubscription_Detail =
            "Pages.Administration.WebhookSubscription.Detail";
        public const string Pages_Administration_Webhook_ListSendAttempts =
            "Pages.Administration.Webhook.ListSendAttempts";
        public const string Pages_Administration_Webhook_ResendWebhook =
            "Pages.Administration.Webhook.ResendWebhook";

        public const string Pages_Administration_DynamicProperties =
            "Pages.Administration.DynamicProperties";
        public const string Pages_Administration_DynamicProperties_Create =
            "Pages.Administration.DynamicProperties.Create";
        public const string Pages_Administration_DynamicProperties_Edit =
            "Pages.Administration.DynamicProperties.Edit";
        public const string Pages_Administration_DynamicProperties_Delete =
            "Pages.Administration.DynamicProperties.Delete";

        public const string Pages_Administration_DynamicPropertyValue =
            "Pages.Administration.DynamicPropertyValue";
        public const string Pages_Administration_DynamicPropertyValue_Create =
            "Pages.Administration.DynamicPropertyValue.Create";
        public const string Pages_Administration_DynamicPropertyValue_Edit =
            "Pages.Administration.DynamicPropertyValue.Edit";
        public const string Pages_Administration_DynamicPropertyValue_Delete =
            "Pages.Administration.DynamicPropertyValue.Delete";

        public const string Pages_Administration_DynamicEntityProperties =
            "Pages.Administration.DynamicEntityProperties";
        public const string Pages_Administration_DynamicEntityProperties_Create =
            "Pages.Administration.DynamicEntityProperties.Create";
        public const string Pages_Administration_DynamicEntityProperties_Edit =
            "Pages.Administration.DynamicEntityProperties.Edit";
        public const string Pages_Administration_DynamicEntityProperties_Delete =
            "Pages.Administration.DynamicEntityProperties.Delete";

        public const string Pages_Administration_DynamicEntityPropertyValue =
            "Pages.Administration.DynamicEntityPropertyValue";
        public const string Pages_Administration_DynamicEntityPropertyValue_Create =
            "Pages.Administration.DynamicEntityPropertyValue.Create";
        public const string Pages_Administration_DynamicEntityPropertyValue_Edit =
            "Pages.Administration.DynamicEntityPropertyValue.Edit";
        public const string Pages_Administration_DynamicEntityPropertyValue_Delete =
            "Pages.Administration.DynamicEntityPropertyValue.Delete";

        public const string Pages_Administration_MassNotification =
            "Pages.Administration.MassNotification";
        public const string Pages_Administration_MassNotification_Create =
            "Pages.Administration.MassNotification.Create";

        public const string Pages_Administration_NewVersion_Create =
            "Pages_Administration_NewVersion_Create";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings =
            "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement =
            "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition =
            "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance =
            "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings =
            "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard =
            "Pages.Administration.Host.Dashboard";
        
        
        public const string Pages_BedMaking = "Pages.BedMaking";
        public const string Pages_BedMaking_Create = "Pages.BedMaking.Create";
        public const string Pages_BedMaking_Edit = "Pages.BedMaking.Edit";
        public const string Pages_BedMaking_Delete = "Pages.BedMaking.Delete";
        
        public const string Pages_Feeding = "Pages.Feeding";
        public const string Pages_Feeding_Create = "Pages.Feeding.Create";
        public const string Pages_Feeding_Edit = "Pages.Feeding.Edit";
        public const string Pages_Feeding_Delete = "Pages.Feeding.Delete";
        
        public const string Pages_PlanItems = "Pages.PlanItems";
        public const string Pages_PlanItems_Create = "Pages.PlanItems.Create";
        public const string Pages_PlanItems_Edit = "Pages.PlanItems.Edit";
        public const string Pages_PlanItems_Delete = "Pages.PlanItems.Delete";
  
        public const string Pages_InputNotes = "Pages.InputNotes";
        public const string Pages_InputNotes_Create = "Pages.InputNotes.Create";
        public const string Pages_InputNotes_Edit = "Pages.InputNotes.Edit";
        public const string Pages_InputNotes_Delete = "Pages.InputNotes.Delete";
        
        public const string Pages_WoundDressing = "Pages.WoundDressing";
        public const string Pages_WoundDressing_Create = "Pages.WoundDressing.Create";
        public const string Pages_WoundDressing_Edit = "Pages.WoundDressing.Edit";
        public const string Pages_WoundDressing_Delete = "Pages.WoundDressing.Delete";
        
        public const string Pages_Meals = "Pages.Meals";
        public const string Pages_Meals_Create = "Pages.Meals.Create";
        public const string Pages_Meals_Edit = "Pages.Meals.Edit";
        public const string Pages_Meals_Delete = "Pages.Meals.Delete";
        
        public const string Pages_Symptoms = "Pages.Symptoms";
        public const string Pages_Symptoms_Create = "Pages.Symptoms.Create";
        public const string Pages_Symptoms_Edit = "Pages.Symptoms.Edit";
        public const string Pages_Symptoms_Delete = "Pages.Symptoms.Delete";

        public const string Pages_Diagnosis = "Pages_Diagnosis";
        public const string Pages_Diagnosis_Create = "Pages.Diagnosis.Create";
        public const string Pages_Diagnosis_Edit = "Pages_Diagnosis.Edit";
        public const string Pages_Diagnosis_Delete = "Pages.Diagnosis.Delete";


        public const string Pages_Rooms = "Pages_Rooms";
        public const string Pages_Rooms_Create = "Pages_Rooms_Create";
        public const string Pages_Rooms_Edit = "Pages_Rooms_Edit";
        public const string Pages_Rooms_Delete = "Pages_Rooms_Delete";



        public const string Pages_Medications = "Pages.Medications";
        public const string Pages_Medications_Create = "Pages.Medications.Create";
        public const string Pages_Medications_Edit = "Pages.Medications.Edit";
        public const string Pages_Medications_Delete = "Pages.Medications.Delete";



        public const string Pages_ContactSales = "Pages.ContactSales";
        public const string Pages_ContactSales_Create = "Pages.ContactSales.Create";


        public const string Pages_Discharge = "Pages.Discharge";
        public const string Pages_Discharge_Create = "Pages_Discharge_Create";
        public const string Pages_Discharge_Edit = "Pages_Discharge_Edit";
        public const string Pages_Discharge_Delete = "Pages_Discharge_Delete";
        public const string Pages_Discharge_Finalize = "Pages_Discharge_Finalize";

        public const string Pages_IntakeOutput = "Pages.IntakeOutput";
        public const string Pages_IntakeOutput_Create = "Pages_IntakeOutput_Create";
        public const string Pages_IntakeOutput_Delete = "Pages_IntakeOutput_Delete";

        public const string Pages_WardEmergencies = "Pages.WardEmergencies";

        public const string Pages_NextAppointment = "Pages.NextAppointment";
        public const string Pages_NextAppointment_Create = "Pages_NextAppointment_Create";
        public const string Pages_NextAppointment_Delete = "Pages_NextAppointment_Delete";

        public const string Pages_DoctorReviewAndSave = "Pages.DoctorReviewAndSave";
        public const string Pages_NurseReviewAndSave = "Pages.NurseReviewAndSave";
        public const string Pages_ReviewDetailedHistory = "Pages_ReviewDetailedHistory";
        public const string Pages_ReviewDetailedHistory_Save = "Pages_ReviewDetailedHistory_Save";
        public const string Pages_NurseReviewAndSave_Create = "Pages.NurseReviewAndSave_Create";

        public const string Pages_ReferConsult = "Pages.ReferConsult";
        public const string Pages_ReferConsult_Create = "Pages_ReferConsult_Create";
        public const string Pages_ReferConsult_Delete = "Pages_ReferConsult_Delete";

    }
}
