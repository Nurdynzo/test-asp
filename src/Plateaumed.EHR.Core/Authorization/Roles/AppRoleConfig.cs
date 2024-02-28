using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Plateaumed.EHR.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host,
                    grantAllPermissionsByDefault: true)
            );

            //Static tenant roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant,
                    grantAllPermissionsByDefault: true)
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.User,
                    MultiTenancySides.Tenant)
            );

            //Static super user roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.TeamRoles.CMD,
                    MultiTenancySides.Tenant)
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.TeamRoles.HOD,
                    MultiTenancySides.Tenant)
            );

            //Static job roles
            // TODO Add permissions for each and group permissions into reusable chunks

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Doctor,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions =
                    {
                        AppPermissions.Pages_Invoices,
                        AppPermissions.Pages_Invoices_Create,
                        AppPermissions.Pages_Invoices_Edit,
                        AppPermissions.Pages_PatientAppointments,
                        AppPermissions.Pages_PatientAppointments_Create,
                        AppPermissions.Pages_PatientAppointments_Edit,
                        AppPermissions.Pages_PatientInsurers,
                        AppPermissions.Pages_PatientInsurers_Create,
                        AppPermissions.Pages_PatientInsurers_Edit,
                        AppPermissions.Pages_PatientOccupationCategories,
                        AppPermissions.Pages_PatientOccupationCategories_Create,
                        AppPermissions.Pages_PatientOccupationCategories_Edit,
                        AppPermissions.Pages_PatientOccupations,
                        AppPermissions.Pages_PatientOccupations_Create,
                        AppPermissions.Pages_PatientOccupations_Edit,
                        AppPermissions.Pages_PatientReferralDocuments,
                        AppPermissions.Pages_PatientReferralDocuments_Create,
                        AppPermissions.Pages_PatientReferralDocuments_Edit,
                        AppPermissions.Pages_PatientRelations,
                        AppPermissions.Pages_PatientRelations_Create,
                        AppPermissions.Pages_PatientRelations_Edit,
                        AppPermissions.Pages_Patients,
                        AppPermissions.Pages_Patients_Create,
                        AppPermissions.Pages_Patients_Edit,
                        AppPermissions.Pages_Countries,
                        AppPermissions.Pages_StaffMembers,
                        AppPermissions.Pages_Symptoms,
                        AppPermissions.Pages_Symptoms_Create,
                        AppPermissions.Pages_Symptoms_Delete,
                        AppPermissions.Pages_Symptoms_Edit,
                        AppPermissions.Pages_Diagnosis,
                        AppPermissions.Pages_Diagnosis_Create,
                        AppPermissions.Pages_Diagnosis_Edit,
                        AppPermissions.Pages_Diagnosis_Delete,
                        AppPermissions.Pages_Rooms,
                        AppPermissions.Pages_Rooms_Create,
                        AppPermissions.Pages_Rooms_Edit,
                        AppPermissions.Pages_Rooms_Delete,
                        AppPermissions.Pages_Medications,
                        AppPermissions.Pages_Medications_Create,
                        AppPermissions.Pages_Medications_Edit,
                        AppPermissions.Pages_Medications_Delete,
                        AppPermissions.Pages_BedMaking,
                        AppPermissions.Pages_BedMaking_Create,
                        AppPermissions.Pages_BedMaking_Delete,
                        AppPermissions.Pages_BedMaking_Edit,
                        AppPermissions.Pages_Feeding,
                        AppPermissions.Pages_Feeding_Create,
                        AppPermissions.Pages_Feeding_Delete,
                        AppPermissions.Pages_Feeding_Edit,
                        AppPermissions.Pages_Investigations,
                        AppPermissions.Pages_PlanItems,
                        AppPermissions.Pages_PlanItems_Create,
                        AppPermissions.Pages_PlanItems_Delete,
                        AppPermissions.Pages_PlanItems_Edit,
                        AppPermissions.Pages_InputNotes,
                        AppPermissions.Pages_InputNotes_Create,
                        AppPermissions.Pages_InputNotes_Delete,
                        AppPermissions.Pages_InputNotes_Edit,
                        AppPermissions.Pages_WoundDressing,
                        AppPermissions.Pages_WoundDressing_Create,
                        AppPermissions.Pages_WoundDressing_Delete,
                        AppPermissions.Pages_WoundDressing_Edit,
                        AppPermissions.Pages_Meals,
                        AppPermissions.Pages_Meals_Create,
                        AppPermissions.Pages_Meals_Delete,
                        AppPermissions.Pages_Meals_Edit,
                        AppPermissions.Pages_Patient_Profiles,
                        AppPermissions.Pages_Patient_Profiles_Edit,
                        AppPermissions.Pages_Patient_Profiles_Delete,
                        AppPermissions.Pages_Patient_Profiles_Create,
                        AppPermissions.Pages_Patient_Profiles_View,
                        AppPermissions.Pages_Discharge,                      
                        AppPermissions.Pages_Discharge_Create,
                        AppPermissions.Pages_Discharge_Edit,
                        AppPermissions.Pages_IntakeOutput,
                        AppPermissions.Pages_IntakeOutput_Create,
                        AppPermissions.Pages_IntakeOutput_Delete,
                        AppPermissions.Pages_NextAppointment,
                        AppPermissions.Pages_NextAppointment_Create,
                        AppPermissions.Pages_NextAppointment_Delete,
                        AppPermissions.Pages_Wards,
                        AppPermissions.Pages_Wards_Create,
                        AppPermissions.Pages_Wards_Edit,
                        AppPermissions.Pages_Wards_Delete,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_View,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_Create,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_Delete,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_View,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Create,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Delete,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Edit,
                        AppPermissions.Pages_Patient_Profiles_BloodGroup_Create,
                        AppPermissions.Pages_Patient_Profiles_BloodGroup_View,
                        AppPermissions.Pages_Patient_Profiles_Physical_Exercise_View,
                        AppPermissions.Pages_Patient_Profiles_Physical_Exercise_Create,
                        AppPermissions.Pages_Patient_Profiles_Chronic_Condition_View,
                        AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Create,
                        AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Delete,
                        AppPermissions.Pages_Patient_Profiles_Travel_History_View,
                        AppPermissions.Pages_Patient_Profiles_Travel_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Travel_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_View,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Edit,
                        AppPermissions.Pages_Patient_Profiles_Vaccination_History_View,
                        AppPermissions.Pages_Patient_Profiles_Vaccination_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Vaccination_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_View,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_Edit,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_Edit,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_View,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Implant_View,
                        AppPermissions.Pages_Patient_Profiles_Implant_Create,
                        AppPermissions.Pages_Patient_Profiles_Implant_Delete,
                        AppPermissions.Pages_Patient_Profiles_Implant_Edit,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_Delete,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_Edit,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_Create,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_View,
                        AppPermissions.Pages_Patient_Profiles_Treatement_Plan_View,
                        AppPermissions.Pages_Patient_Profiles_Clinical_Investigation_View,
                        AppPermissions.Pages_NextAppointment_Delete,
                        AppPermissions.Pages_DoctorReviewAndSave,
                        AppPermissions.Pages_ReviewDetailedHistory,
                        AppPermissions.Pages_ReviewDetailedHistory_Save,
                        AppPermissions.Pages_ReferConsult,
                        AppPermissions.Pages_ReferConsult_Create,
                        AppPermissions.Pages_ReferConsult_Delete,
                        AppPermissions.Pages_Administration_OrganizationUnits,
                    }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.DentalDoctor,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );
            
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Nurse,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions =
                    {
                        AppPermissions.Pages_Invoices,
                        AppPermissions.Pages_Invoices_Create,
                        AppPermissions.Pages_Invoices_Edit,
                        AppPermissions.Pages_PatientAppointments,
                        AppPermissions.Pages_PatientAppointments_Create,
                        AppPermissions.Pages_PatientAppointments_Edit,
                        AppPermissions.Pages_PatientInsurers,
                        AppPermissions.Pages_PatientInsurers_Create,
                        AppPermissions.Pages_PatientInsurers_Edit,
                        AppPermissions.Pages_PatientOccupationCategories,
                        AppPermissions.Pages_PatientOccupationCategories_Create,
                        AppPermissions.Pages_PatientOccupationCategories_Edit,
                        AppPermissions.Pages_PatientOccupations,
                        AppPermissions.Pages_PatientOccupations_Create,
                        AppPermissions.Pages_PatientOccupations_Edit,
                        AppPermissions.Pages_PatientReferralDocuments,
                        AppPermissions.Pages_PatientReferralDocuments_Create,
                        AppPermissions.Pages_PatientReferralDocuments_Edit,
                        AppPermissions.Pages_PatientRelations,
                        AppPermissions.Pages_PatientRelations_Create,
                        AppPermissions.Pages_PatientRelations_Edit,
                        AppPermissions.Pages_Patients,
                        AppPermissions.Pages_Patients_Create,
                        AppPermissions.Pages_Patients_Edit,
                        AppPermissions.Pages_Countries,
                        AppPermissions.Pages_StaffMembers,
                        AppPermissions.Pages_Symptoms,
                        AppPermissions.Pages_Symptoms_Create,
                        AppPermissions.Pages_Symptoms_Delete,
                        AppPermissions.Pages_Symptoms_Edit,
                        AppPermissions.Pages_Diagnosis,
                        AppPermissions.Pages_Diagnosis_Create,
                        AppPermissions.Pages_Diagnosis_Edit,
                        AppPermissions.Pages_Diagnosis_Delete,
                        AppPermissions.Pages_Rooms,
                        AppPermissions.Pages_Rooms_Create,
                        AppPermissions.Pages_Rooms_Edit,
                        AppPermissions.Pages_Rooms_Delete,
                        AppPermissions.Pages_Medications,
                        AppPermissions.Pages_Medications_Create,
                        AppPermissions.Pages_Medications_Edit,
                        AppPermissions.Pages_Medications_Delete,
                        AppPermissions.Pages_BedMaking,
                        AppPermissions.Pages_BedMaking_Create,
                        AppPermissions.Pages_BedMaking_Delete,
                        AppPermissions.Pages_BedMaking_Edit,
                        AppPermissions.Pages_Feeding,
                        AppPermissions.Pages_Feeding_Create,
                        AppPermissions.Pages_Feeding_Delete,
                        AppPermissions.Pages_Feeding_Edit,
                        AppPermissions.Pages_Investigations,
                        AppPermissions.Pages_PlanItems,
                        AppPermissions.Pages_PlanItems_Create,
                        AppPermissions.Pages_PlanItems_Delete,
                        AppPermissions.Pages_PlanItems_Edit,
                        AppPermissions.Pages_InputNotes,
                        AppPermissions.Pages_InputNotes_Create,
                        AppPermissions.Pages_InputNotes_Delete,
                        AppPermissions.Pages_InputNotes_Edit,
                        AppPermissions.Pages_WoundDressing,
                        AppPermissions.Pages_WoundDressing_Create,
                        AppPermissions.Pages_WoundDressing_Delete,
                        AppPermissions.Pages_Meals,
                        AppPermissions.Pages_Meals_Create,
                        AppPermissions.Pages_Meals_Delete,
                        AppPermissions.Pages_Meals_Edit,
                        AppPermissions.Pages_WoundDressing_Edit,
                        AppPermissions.Pages_Discharge,
                        AppPermissions.Pages_Discharge_Finalize,
                        AppPermissions.Pages_IntakeOutput,
                        AppPermissions.Pages_IntakeOutput_Create,
                        AppPermissions.Pages_IntakeOutput_Delete,
                        AppPermissions.Pages_WardEmergencies,
                        AppPermissions.Pages_NextAppointment,
                        AppPermissions.Pages_NextAppointment_Create,
                        AppPermissions.Pages_NextAppointment_Delete,
                        AppPermissions.Pages_Wards,
                        AppPermissions.Pages_Wards_Create,
                        AppPermissions.Pages_Wards_Edit,
                        AppPermissions.Pages_Wards_Delete,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_View,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_Create,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_Delete,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_View,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Create,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Delete,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Edit,
                        AppPermissions.Pages_Patient_Profiles_BloodGroup_Create,
                        AppPermissions.Pages_Patient_Profiles_BloodGroup_View,
                        AppPermissions.Pages_Patient_Profiles_Physical_Exercise_View,
                        AppPermissions.Pages_Patient_Profiles_Physical_Exercise_Create,
                        AppPermissions.Pages_Patient_Profiles_Chronic_Condition_View,
                        AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Create,
                        AppPermissions.Pages_Patient_Profiles_Chronic_Condition_Delete,
                        AppPermissions.Pages_Patient_Profiles_Travel_History_View,
                        AppPermissions.Pages_Patient_Profiles_Travel_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Travel_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_View,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Edit,
                        AppPermissions.Pages_Patient_Profiles_Vaccination_History_View,
                        AppPermissions.Pages_Patient_Profiles_Vaccination_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Vaccination_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_View,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Drug_History_Edit,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_Edit,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_Create,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_View,
                        AppPermissions.Pages_Patient_Profiles_Allergy_History_Delete,
                        AppPermissions.Pages_Patient_Profiles_Implant_View,
                        AppPermissions.Pages_Patient_Profiles_Implant_Create,
                        AppPermissions.Pages_Patient_Profiles_Implant_Delete,
                        AppPermissions.Pages_Patient_Profiles_Implant_Edit,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_Delete,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_Edit,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_Create,
                        AppPermissions.Pages_Patient_Profiles_Review_of_System_View,
                        AppPermissions.Pages_Patient_Profiles_Treatement_Plan_View,
                        AppPermissions.Pages_Patient_Profiles_Clinical_Investigation_View,
                        AppPermissions.Pages_NurseReviewAndSave,
                        AppPermissions.Pages_NurseReviewAndSave_Create,
                        AppPermissions.Pages_Administration_OrganizationUnits,

                    }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Pharmacist,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions =
                    {
                        AppPermissions.Pages_Invoices,
                        AppPermissions.Pages_Invoices_Create,
                        AppPermissions.Pages_Invoices_Edit,
                        AppPermissions.Pages_PatientAppointments,
                        AppPermissions.Pages_PatientAppointments_Create,
                        AppPermissions.Pages_PatientAppointments_Edit,
                        AppPermissions.Pages_PatientInsurers,
                        AppPermissions.Pages_PatientInsurers_Create,
                        AppPermissions.Pages_PatientInsurers_Edit,
                        AppPermissions.Pages_Patients,
                        AppPermissions.Pages_Patients_Create,
                        AppPermissions.Pages_Patients_Edit,
                        AppPermissions.Pages_Countries,
                        AppPermissions.Pages_StaffMembers,
                        AppPermissions.Pages_Symptoms,
                        AppPermissions.Pages_Symptoms_Create,
                        AppPermissions.Pages_Symptoms_Delete,
                        AppPermissions.Pages_Symptoms_Edit,
                        AppPermissions.Pages_Diagnosis,
                        AppPermissions.Pages_Diagnosis_Create,
                        AppPermissions.Pages_Diagnosis_Edit,
                        AppPermissions.Pages_Diagnosis_Delete,
                        AppPermissions.Pages_Medications,
                        AppPermissions.Pages_Medications_Create,
                        AppPermissions.Pages_Medications_Edit,
                        AppPermissions.Pages_Medications_Delete,
                    }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.PharmacyTechnician,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.LaboratoryScientist,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions =
                    {
                        AppPermissions.Pages_Invoices,
                        AppPermissions.Pages_Invoices_Create,
                        AppPermissions.Pages_Invoices_Edit,
                        AppPermissions.Pages_PatientInsurers,
                        AppPermissions.Pages_PatientInsurers_Create,
                        AppPermissions.Pages_PatientInsurers_Edit,
                        AppPermissions.Pages_Patients,
                        AppPermissions.Pages_Patients_Create,
                        AppPermissions.Pages_Patients_Edit,
                        AppPermissions.Pages_Countries,
                        AppPermissions.Pages_StaffMembers,
                        AppPermissions.Pages_Symptoms,
                        AppPermissions.Pages_Symptoms_Create,
                        AppPermissions.Pages_Symptoms_Delete,
                        AppPermissions.Pages_Symptoms_Edit,
                        AppPermissions.Pages_Diagnosis,
                        AppPermissions.Pages_Diagnosis_Create,
                        AppPermissions.Pages_Diagnosis_Edit,
                        AppPermissions.Pages_Diagnosis_Delete,
                        AppPermissions.Pages_Investigations,
                        AppPermissions.Pages_PlanItems,
                        AppPermissions.Pages_PlanItems_Create,
                        AppPermissions.Pages_PlanItems_Delete,
                        AppPermissions.Pages_PlanItems_Edit,
                        AppPermissions.Pages_InputNotes,
                        AppPermissions.Pages_InputNotes_Create,
                        AppPermissions.Pages_InputNotes_Delete,
                        AppPermissions.Pages_InputNotes_Edit,
                        AppPermissions.Pages_WoundDressing,
                        AppPermissions.Pages_WoundDressing_Create,
                        AppPermissions.Pages_WoundDressing_Delete,
                        AppPermissions.Pages_WoundDressing_Edit,
                        AppPermissions.Pages_Meals,
                        AppPermissions.Pages_Meals_Create,
                        AppPermissions.Pages_Meals_Delete,
                        AppPermissions.Pages_Meals_Edit,
                    }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.LaboratoryTechnician,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );
            
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Radiographer,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.XrayTechnician,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );
            
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Physiotherapist,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Dietetics,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.SocialCare,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );

            roleManagementConfig.StaticRoles.Add(
               new StaticRoleDefinition(
                   StaticRoleNames.JobRoles.FrontDesk,
                   MultiTenancySides.Tenant)
               {
                   GrantedPermissions =
                   {
                        AppPermissions.Pages_Invoices,
                        AppPermissions.Pages_Invoices_Create,
                        AppPermissions.Pages_Invoices_Edit,
                        AppPermissions.Pages_PatientAppointments,
                        AppPermissions.Pages_PatientAppointments_Create,
                        AppPermissions.Pages_PatientAppointments_Edit,
                        AppPermissions.Pages_PatientInsurers,
                        AppPermissions.Pages_PatientInsurers_Create,
                        AppPermissions.Pages_PatientInsurers_Edit,
                        AppPermissions.Pages_PatientOccupationCategories,
                        AppPermissions.Pages_PatientOccupationCategories_Create,
                        AppPermissions.Pages_PatientOccupationCategories_Edit,
                        AppPermissions.Pages_PatientOccupations,
                        AppPermissions.Pages_PatientOccupations_Create,
                        AppPermissions.Pages_PatientOccupations_Edit,
                        AppPermissions.Pages_PatientReferralDocuments,
                        AppPermissions.Pages_PatientReferralDocuments_Create,
                        AppPermissions.Pages_PatientReferralDocuments_Edit,
                        AppPermissions.Pages_PatientRelations,
                        AppPermissions.Pages_PatientRelations_Create,
                        AppPermissions.Pages_PatientRelations_Edit,
                        AppPermissions.Pages_Patients,
                        AppPermissions.Pages_Patients_Create,
                        AppPermissions.Pages_Patients_Edit,
                        AppPermissions.Pages_Countries,
                        AppPermissions.Pages_Symptoms,
                        AppPermissions.Pages_Symptoms_Create,
                        AppPermissions.Pages_Symptoms_Delete,
                        AppPermissions.Pages_Symptoms_Edit,
                        AppPermissions.Pages_ScanDocument,
                        AppPermissions.Pages_ScanDocument_Review,
                        AppPermissions.Pages_ScanDocument_Upload,
                        AppPermissions.Pages_Diagnosis,
                        AppPermissions.Pages_Diagnosis_Create,
                        AppPermissions.Pages_Diagnosis_Edit,
                        AppPermissions.Pages_Diagnosis_Delete,
                        AppPermissions.Pages_Rooms,
                        AppPermissions.Pages_Rooms_Create,
                        AppPermissions.Pages_Rooms_Edit,
                        AppPermissions.Pages_Rooms_Delete,
                        AppPermissions.Pages_Medications,
                        AppPermissions.Pages_Medications_Create,
                        AppPermissions.Pages_Medications_Edit,
                        AppPermissions.Pages_Medications_Delete,
                        AppPermissions.Pages_BedMaking,
                        AppPermissions.Pages_BedMaking_Create,
                        AppPermissions.Pages_BedMaking_Delete,
                        AppPermissions.Pages_BedMaking_Edit,
                        AppPermissions.Pages_Feeding,
                        AppPermissions.Pages_Feeding_Create,
                        AppPermissions.Pages_Feeding_Delete,
                        AppPermissions.Pages_Feeding_Edit,
                        AppPermissions.Pages_PlanItems,
                        AppPermissions.Pages_PlanItems_Create,
                        AppPermissions.Pages_PlanItems_Delete,
                        AppPermissions.Pages_PlanItems_Edit,
                        AppPermissions.Pages_InputNotes,
                        AppPermissions.Pages_InputNotes_Create,
                        AppPermissions.Pages_InputNotes_Delete,
                        AppPermissions.Pages_InputNotes_Edit,
                        AppPermissions.Pages_WoundDressing,
                        AppPermissions.Pages_WoundDressing_Create,
                        AppPermissions.Pages_WoundDressing_Delete,
                        AppPermissions.Pages_WoundDressing_Edit,
                        AppPermissions.Pages_Meals,
                        AppPermissions.Pages_Meals_Create,
                        AppPermissions.Pages_Meals_Delete,
                        AppPermissions.Pages_Meals_Edit,
                        AppPermissions.Pages_Administration_OrganizationUnits,
                        AppPermissions.Pages_IntakeOutput,
                        AppPermissions.Pages_IntakeOutput_Create,
                        AppPermissions.Pages_IntakeOutput_Delete,
                        AppPermissions.Pages_NextAppointment,
                        AppPermissions.Pages_NextAppointment_Create,
                        AppPermissions.Pages_NextAppointment_Delete,
                        AppPermissions.Pages_Patient_Profiles,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_View,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_Create,
                        AppPermissions.Pages_Patient_Profiles_FamilyHistory_Delete,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_View,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Create,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Delete,
                        AppPermissions.Pages_Patient_Profiles_OccupationHistory_Edit,
                        AppPermissions.Pages_Patient_Profiles_Surgical_History_Create,
                        AppPermissions.Pages_Regions,
                        AppPermissions.Pages_Districts
                   }
               }
           );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.Accountant,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.JobRoles.AdminPersonnel,
                    MultiTenancySides.Tenant)
                {
                    GrantedPermissions = { }
                }
            );
        }
    }
}
