using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc.Dtos;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Organizations.Dtos;
using Plateaumed.EHR.Insurance.Dtos;
using Plateaumed.EHR.Insurance;
using Plateaumed.EHR.MultiTenancy.Dtos;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Staff;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using IdentityServer4.Extensions;
using Plateaumed.EHR.Auditing.Dto;
using Plateaumed.EHR.Authorization.Accounts.Dto;
using Plateaumed.EHR.Authorization.Delegation;
using Plateaumed.EHR.Authorization.Permissions.Dto;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Roles.Dto;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Authorization.Users.Delegation.Dto;
using Plateaumed.EHR.Authorization.Users.Dto;
using Plateaumed.EHR.Authorization.Users.Importing.Dto;
using Plateaumed.EHR.Authorization.Users.Profile.Dto;
using Plateaumed.EHR.Chat;
using Plateaumed.EHR.Chat.Dto;
using Plateaumed.EHR.DynamicEntityProperties.Dto;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.Editions.Dto;
using Plateaumed.EHR.Friendships;
using Plateaumed.EHR.Friendships.Cache;
using Plateaumed.EHR.Friendships.Dto;
using Plateaumed.EHR.Localization.Dto;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.MultiTenancy.Dto;
using Plateaumed.EHR.MultiTenancy.HostDashboard.Dto;
using Plateaumed.EHR.MultiTenancy.Payments;
using Plateaumed.EHR.MultiTenancy.Payments.Dto;
using Plateaumed.EHR.Notifications.Dto;
using Plateaumed.EHR.Organizations.Dto;
using Plateaumed.EHR.Sessions.Dto;
using Plateaumed.EHR.WebHooks.Dto;
using Plateaumed.EHR.Organizations;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.BedMaking.Dtos;
using Plateaumed.EHR.Diagnoses;
using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.InputNotes.Dtos;
using Plateaumed.EHR.PatientDocument.Dto;
using Plateaumed.EHR.Snowstorm.Dtos;
using Plateaumed.EHR.Symptom.Dtos;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Symptom;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.PlanItems.Dtos;
using Plateaumed.EHR.PriceSettings.Dto;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.Vaccines.Dto;
using Plateaumed.EHR.ValueObjects;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.VitalSigns.Dto;
using Plateaumed.EHR.WoundDressing.Dtos;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Feeding.Dtos;
using Plateaumed.EHR.Meals.Dtos;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.NurseCarePlans.Dto;
using Plateaumed.EHR.NurseHistory.Dtos;

namespace Plateaumed.EHR
{
    public static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditInvoiceDto, Invoice>().ReverseMap();
            configuration.CreateMap<InvoiceDto, Invoice>().ReverseMap();
            configuration.CreateMap<CreateOrEditInvoiceItemDto, InvoiceItem>().ReverseMap();
            configuration.CreateMap<InvoiceItemDto, InvoiceItem>().ReverseMap();
            configuration.CreateMap<CreateOrEditPatientAppointmentDto, PatientAppointment>().ReverseMap();
            configuration.CreateMap<PatientAppointmentDto, PatientAppointment>().ReverseMap();
            configuration.CreateMap<EditAppointmentStatusDto, PatientAppointment>().ReverseMap();
                
            
            configuration
                .CreateMap<CreateOrEditPatientReferralDocumentDto, PatientReferralDocument>()
                .ReverseMap();
            configuration.CreateMap<PatientReferralDocumentDto, PatientReferralDocument>().ReverseMap();
            configuration.CreateMap<CreateOrEditPatientInsurerDto, PatientInsurer>().ReverseMap();
            configuration.CreateMap<PatientInsurerDto, PatientInsurer>().ReverseMap();
            configuration.CreateMap<CreateOrEditPatientRelationDto, PatientRelation>().ReverseMap();
            configuration.CreateMap<CreateOrEditPatientRelationDiagnosisDto, PatientRelationDiagnosis>().ReverseMap();
            configuration.CreateMap<PatientRelationDto, PatientRelation>().ReverseMap();
            configuration.CreateMap<CreateOrEditPatientDto, Patient>().ReverseMap();
            configuration.CreateMap<PatientDto, Patient>().ReverseMap(); 
            configuration.CreateMap<ReferralLetterUploadRequest, PatientReferralDocument>()
                .ForMember(x=>x.ReferralDocument, opt => opt.MapFrom(f=>f.FileId) )
                .ReverseMap();
            configuration.CreateMap<Patient, SimplePatientInfoResponseDto>(); 
            
            configuration
            .CreateMap<Country,CountryDto>();
            configuration.CreateMap<CreateOrEditCountryDto, Country>().ReverseMap();
            configuration
                .CreateMap<CreateOrEditPatientOccupationDto, PatientOccupation>()
                .ReverseMap();
            configuration.CreateMap<PatientOccupationDto, PatientOccupation>().ReverseMap();
            configuration
                .CreateMap<CreateOrEditOrganizationUnitTimeDto, OrganizationUnitTime>()
                .ReverseMap();
            configuration.CreateMap<OrganizationUnitTimeDto, OrganizationUnitTime>().ReverseMap();

            //Facility Documents
            configuration
                .CreateMap<CreateOrEditFacilityDocumentDto, FacilityDocument>()
                .ReverseMap();
            configuration.CreateMap<FacilityDocumentDto, FacilityDocument>().ReverseMap();

            //Facility Bank Details
            configuration.CreateMap<CreateOrEditBankRequest, FacilityBank>().ReverseMap();
            configuration.CreateMap<FacilityBank, FacilityBankResponseDto>().ReverseMap();
            
            //Facility Insurers
            configuration.CreateMap<CreateOrEditFacilityInsurerDto, FacilityInsurer>().ReverseMap();
            configuration.CreateMap<FacilityInsurerDto, FacilityInsurer>().ReverseMap();

            //Insurance Providers
            configuration
                .CreateMap<CreateOrEditInsuranceProviderDto, InsuranceProvider>()
                .ReverseMap();
            configuration.CreateMap<InsuranceProviderDto, InsuranceProvider>().ReverseMap();

            //Staff
            configuration.CreateMap<StaffMemberDto, User>()
                .ForMember(user => user.Password, options => options.Ignore())
                .ForMember(user => user.StaffMemberFk, options => options.MapFrom(dto => dto))
                .ReverseMap()
                .ForMember(dto => dto.Password, options => options.Ignore());
            configuration.CreateMap<CreateOrEditFacilityStaffDto, FacilityStaff>().ReverseMap();
            configuration.CreateMap<FacilityStaffDto, FacilityStaff>().ReverseMap();
            configuration.CreateMap<FacilityStaff, StaffAssignedFacilitiesListDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(facilityStaff => facilityStaff.FacilityId))
                .ForMember(dto => dto.IsDefault, options => options.MapFrom(facilityStaff => facilityStaff.IsDefault))
                .ForMember(dto => dto.Name, options => options.MapFrom(facilityStaff => facilityStaff.FacilityFk == null ? string.Empty : facilityStaff.FacilityFk.Name));
            configuration.CreateMap<CreateOrEditStaffMemberDto, StaffMember>()
                .ForMember(staffMember => staffMember.AssignedFacilities, options => options.Ignore())
                .ReverseMap()
                .ForMember(dto => dto.AssignedFacilities, options => options.MapFrom(staffMember => staffMember.AssignedFacilities));
            configuration.CreateMap<StaffMemberDto, StaffMember>()
                .ReverseMap();
            configuration.CreateMap<User, GetStaffMembersResponse>()
                .ForMember(dto => dto.StaffMemberId,
                    options => options.MapFrom(v => v.StaffMemberFk == null ? 0 : v.StaffMemberFk.Id));
            configuration.CreateMap<User, GetStaffMembersSimpleResponseDto>()
                .ForMember(dto => dto.StaffCode, options => options.MapFrom(v => v.StaffMemberFk == null ? string.Empty : v.StaffMemberFk.StaffCode))
                .ForMember(dto => dto.StaffMemberId, options => options.MapFrom(v => v.StaffMemberFk == null ? 0 : v.StaffMemberFk.Id));

            //Tenant Document
            configuration.CreateMap<CreateOrEditTenantDocumentDto, TenantDocument>().ReverseMap();
            configuration.CreateMap<TenantDocumentDto, TenantDocument>().ReverseMap();

            //Wards
            configuration.CreateMap<CreateOrEditWardBedDto, WardBed>().ReverseMap();
            configuration.CreateMap<WardBedDto, WardBed>().ReverseMap();
            configuration.CreateMap<CreateOrEditWardDto, Ward>().ReverseMap();
            configuration.CreateMap<WardDto, Ward>().ReverseMap();

            // Nurse Care plan
            configuration.CreateMap<NursingOutcome, GetNurseCareSummaryResponse>()
                .ForMember(dest => dest.Outcomes, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Name))     
                .ForMember(dest => dest.Evaluation, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Interventions, opt => opt.MapFrom(src => src.Name));       
            
            configuration.CreateMap<CreateNurseHistoryDto, NursingHistory>();
            configuration.CreateMap<NursingHistory, NurseHistoryResponseDto>();
            
            //Beds
            configuration.CreateMap<CreateOrEditBedTypeDto, BedType>().ReverseMap();
            configuration.CreateMap<BedTypeDto, BedType>().ReverseMap();

            //Rooms
            configuration.CreateMap<CreateOrEditRoomsDto, Rooms>();
            configuration.CreateMap<RoomAvailabilityDto, RoomAvailability>();
            configuration.CreateMap<Rooms, CreateOrEditRoomsDto>();
            configuration.CreateMap<RoomAvailability, RoomAvailabilityDto>();
            configuration.CreateMap<CreateOrEditRoomsAvailabilityDto,  RoomAvailability>().ReverseMap();
            configuration.CreateMap<Rooms, ConsultingRoomDto>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Name));

            //Facility
            configuration.CreateMap<CreateOrEditFacilityDto, Facility>().ReverseMap();
            configuration.CreateMap<Facility, FacilityDto>()
                .ForMember(dto => dto.FacilityGroup, options => options.MapFrom(f => f.GroupFk.Name))
                .ForMember(dto => dto.FacilityType, options => options.MapFrom(f => f.TypeFk.Name));
            configuration.CreateMap<CreateOrEditFacilityPatientCodeTemplateDto, Facility>()
                .ForMember(f => f.PatientCodeTemplate, options => options.MapFrom(dto => dto.PatientCodeTemplate ?? null))
                .ReverseMap();
            configuration.CreateMap<CreateOrEditBankRequest, Facility>().ReverseMap();
            configuration.CreateMap<UserFacilityDto, Facility>().ReverseMap();


            //Facility Group
            configuration.CreateMap<CreateOrEditFacilityGroupDto, FacilityGroup>().ReverseMap();
            configuration.CreateMap<FacilityGroup, FacilityGroupDto>()
                .ForMember(dto => dto.ChildFacilities, options => options.MapFrom(f => f.ChildFacilities));
            configuration.CreateMap<CreateOrEditFacilityGroupPatientCodeTemplateDto, FacilityGroup>().ReverseMap();
            configuration.CreateMap<CreateOrEditFacilityGroupBankRequest, FacilityGroup>().ReverseMap();
            
            //Facility Type
            configuration.CreateMap<CreateOrEditFacilityTypeDto, FacilityType>().ReverseMap();
            configuration.CreateMap<FacilityTypeDto, FacilityType>().ReverseMap();

            //Staff Code Template
            configuration
                .CreateMap<CreateOrEditStaffCodeTemplateDto, StaffCodeTemplate>()
                .ReverseMap();
            configuration.CreateMap<StaffCodeTemplateDto, StaffCodeTemplate>().ReverseMap();

            //Patient Code Template
            configuration
                .CreateMap<CreateOrEditPatientCodeTemplateDto, PatientCodeTemplate>()
                .ReverseMap();
            configuration.CreateMap<PatientCodeTemplateDto, PatientCodeTemplate>().ReverseMap();

            //Patient
            configuration.CreateMap<SaveMenstrualBloodFlowCommandRequest, PatientMenstrualFlow>().ReverseMap();
            configuration.CreateMap<CreatePhysicalExerciseCommandRequest, PatientPhysicalExercise>().ReverseMap();
            configuration.CreateMap<CreatePatientTravelHistoryCommand, PatientTravelHistory>().ReverseMap();
            configuration.CreateMap<PatientPastMedicalConditionCommandRequest, PatientPastMedicalCondition>().ReverseMap();
            configuration.CreateMap<PatientPastMedicalConditionMedicationRequest, PatientPastMedicalConditionMedication>().ReverseMap();
            configuration.CreateMap<GetChronicDiseaseSuggestionQueryResponse, ChronicDisease>().ReverseMap();
            configuration.CreateMap<CreatePatientAllergyCommandRequest, PatientAllergy>().ReverseMap();
            configuration.CreateMap<CreatePatientMajorInjuryRequest, PatientMajorInjury>().ReverseMap();
            configuration.CreateMap<PatientMensurationDuration, SaveMenstruationAndFrequencyCommand>().ReverseMap();
            configuration.CreateMap<PatientFamilyHistoryDto, PatientFamilyHistory>().ReverseMap();
            configuration.CreateMap<PatientFamilyMembersDto, PatientFamilyMembers>().ReverseMap();
            //Job Level
            configuration.CreateMap<CreateOrEditJobLevelDto, JobLevel>().ReverseMap();
            configuration.CreateMap<JobLevelDto, JobLevel>().ReverseMap();

            //Job Title
            configuration.CreateMap<CreateOrEditJobTitleDto, JobTitle>().ReverseMap();
            configuration.CreateMap<JobTitleDto, JobTitle>().ReverseMap();

            //Organization Units
            configuration.CreateMap<OrganizationUnitExtended, OrganizationUnitDto>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type.ToString())).ReverseMap();
            configuration.CreateMap<OrganizationUnitExtended, ClinicListDto>().ReverseMap();

            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration
                .CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<
                StaticLocalizableComboboxItemSource,
                LocalizableComboboxItemSourceDto
            >();
            configuration
                .CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration
                .CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration
                .CreateMap<Edition, EditionInfoDto>()
                .Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>()
                .ForMember(dto => dto.TenantName, options => options.MapFrom(t => t.TenantFk != null ? t.TenantFk.Name : null))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(t => t.CountryFk != null ? t.CountryFk.Name : null));
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<
                NotificationDefinition,
                NotificationSubscriptionWithDisplayNameDto
            >();
            configuration
                .CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>()
                .ForMember(dto => dto.CountryId, options => options.MapFrom(t => t.CountryFk != null ? (int?)t.CountryFk.Id : null))
                .ForMember(dto => dto.Country, options => options.MapFrom(t => t.CountryFk != null ? t.CountryFk.Name : null))
                .ForMember(dto => dto.Currency, options => options.MapFrom(t => t.CountryFk != null ? t.CountryFk.Currency : null))
                .ForMember(dto => dto.CurrencyCode, options => options.MapFrom(t => t.CountryFk != null ? t.CountryFk.CurrencyCode : null))
                .ForMember(dto => dto.CurrencySymbol, options => options.MapFrom(t => t.CountryFk != null ? t.CountryFk.CurrencySymbol : null));
            configuration.CreateMap<Tenant, TenantListDto>()
                .ForMember(dto => dto.Country, options => options.MapFrom(t => t.CountryFk != null ? t.CountryFk.Name : null));
            configuration.CreateMap<Tenant, GetTenantOnboardingProgressOutput>().ReverseMap();
            configuration.CreateMap<TenantOnboardingProgress, TenantOnboardingProgressDto>().ReverseMap();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            

            configuration
                .CreateMap<Region,RegionDto>()
                .ForMember(dto => dto.CountryCode,opt => opt.MapFrom(t => t.CountryFk.Code))
                .ReverseMap();

            configuration
                .CreateMap<Region, GetRegionsForListDto>()
                .ForMember(dto => dto.CountryCode, opt => opt.MapFrom(src => src.CountryFk.Code));

            configuration
                .CreateMap<Country, GetCountryForViewDto>()
                .ForPath(dto => dto.Country, opt => opt.MapFrom(src => src));

            configuration
                .CreateMap<District,GetDistrictForViewDto>()
                .ForPath(dto => dto.District,opt => opt.MapFrom(src => src))
                .ForPath(dto => dto.District.RegionId,opt => opt.MapFrom(src => src.RegionFk.Id));

            configuration
            .CreateMap<Region, GetRegionForViewDto>()
            .ForMember(dto => dto.Region, opt => opt.MapFrom(src => src))
            .ForPath(dto => dto.Region.CountryCode, opt => opt.MapFrom(src => src.CountryFk.Code));


            configuration
                .CreateMap<CreateOrEditRegionDto,Region>()
                .ReverseMap();

            configuration
            .CreateMap<District,DistrictDto>()
            .ForMember(dto => dto.RegionId,opt => opt.MapFrom(src => src.RegionFk.Id)); 

            configuration
            .CreateMap<CreateOrEditDistrictDto,District>()
            .ReverseMap();

            //User
            configuration
                .CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<StaffMember, StaffLoginInfoDto>()
                .ForMember(dto => dto.AssignedFacilities, options => options.MapFrom(staff => staff.AssignedFacilities));
            configuration.CreateMap<Patient, PatientLoginInfoDto>();
            configuration.CreateMap<User, UserLoginInfoDto>()
                .ForMember(dto => dto.Age, options => options.MapFrom(user => user.DateOfBirth.HasValue ? (user.DateOfBirth.Value.Year - DateTime.Now.Year) : (int?)null))
                .ForMember(dto => dto.StaffInfo, options => options.MapFrom(user => user.StaffMemberFk != null ? user.StaffMemberFk : null))
                .ForMember(dto => dto.PatientInfo, options => options.MapFrom(user => user.PatientFk != null ? user.PatientFk : null));
            configuration.CreateMap<User, UserListDto>()
                .ForMember(dto => dto.Country, options => options.MapFrom(user => user.CountryFk != null ? user.CountryFk.Name : null));
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();
            configuration.CreateMap<CreateOrEditStaffMemberRequest, StaffMember>()
                .ForMember(dto => dto.AdminRole, options => options.Ignore());
            configuration.CreateMap<JobDto, Job>()
                .ForMember(dto => dto.TeamRole, options => options.Ignore())
                .ForMember(dto => dto.Id, options => options.Ignore());
            configuration.CreateMap<Job, JobDto>()
                .ForMember(dto => dto.TeamRole, options => options.Ignore());

            configuration.CreateMap<GetStaffJob, Job>()
                .ForMember(dto => dto.TeamRole, options => options.Ignore())
                .ForMember(dto => dto.Id, options => options.Ignore());
            configuration.CreateMap<Job, GetStaffJob>()
                .ForMember(dto => dto.TeamRole, options => options.Ignore());

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitExtended>();
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration
                .CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(
                    webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName)
                )
                .ForMember(
                    webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data)
                );

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration
                .CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(
                    dto => dto.DynamicPropertyName,
                    options =>
                        options.MapFrom(
                            entity =>
                                entity.DynamicProperty.DisplayName.IsNullOrEmpty()
                                    ? entity.DynamicProperty.PropertyName
                                    : entity.DynamicProperty.DisplayName
                        )
                );
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration
                .CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>()
                .ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();
            configuration.CreateMap<User, StaffMemberForReturnDto>()
                .ForMember(dto => dto.UserId, options => options.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.StaffMemberId, options => options.MapFrom(entity => entity.StaffMemberFk != null ? entity.StaffMemberFk.Id : 0))
                .ForMember(dto => dto.StaffCode, options => options.MapFrom(entity => entity.StaffMemberFk != null ? entity.StaffMemberFk.StaffCode : null))
                .ForMember(dto => dto.Name, options => options.MapFrom(entity => $"{entity.Name} {entity.Surname}"))
                .ReverseMap();

            // SnowStorm
            configuration.CreateMap<SnowItem, SnowstormSimpleResponseDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(v => v.Concept != null ? v.Concept.Id : null))
                .ForMember(dto => dto.Name, options => options.MapFrom(v => v.Term));
            
            configuration.CreateMap<SnowmedBodyPart, SnowstormSimpleResponseDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(v => v.SnowmedId))
                .ForMember(dto => dto.Name, options => options.MapFrom(v => v.SubPart));
            
            configuration.CreateMap<SnowmedSuggestion, SnowstormSimpleResponseDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(v => v.Type != AllInputType.Procedure ? v.SnowmedId : v.SourceSnowmedId))
                .ForMember(dto => dto.Name, options => options.MapFrom(v => v.Type != AllInputType.Procedure ? v.Name : v.SourceName)); 
            
            configuration.CreateMap<SnowstormSimpleResponseDto, SearchMedicationForReturnDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(v => Convert.ToInt64(v.Id)))
                .ForMember(dto => dto.ProductName, options => options.MapFrom(v => v.Name))
                .ForMember(dto => dto.Source, options => options.MapFrom(v => "Snowmed"));

            configuration.CreateMap<CreateMealsDto, AllInputs.Meals>();
            
            configuration.CreateMap<AllInputs.Meals, MealsSummaryForReturnDto>()
                .ForMember(dto => dto.Description, options => options.MapFrom(x => x.Description.ToString()));
            
            configuration.CreateMap<CreateBedMakingDto, AllInputs.BedMaking>();
            
            configuration.CreateMap<AllInputs.BedMaking, PatientBedMakingSummaryForReturnDto>()
                .ForMember(dto => dto.Note, options => options.MapFrom(x => x.Note.ToString()));
            
            configuration.CreateMap<CreateFeedingDto, AllInputs.Feeding>();
            
            configuration.CreateMap<AllInputs.Feeding, FeedingSummaryForReturnDto>()
                .ForMember(dto => dto.Description, options => options.MapFrom(x => x.Description.ToString()));
            
            configuration.CreateMap<AllInputs.FeedingSuggestions, FeedingSuggestionsReturnDto>()
                .ForMember(dto => dto.Name, options => options.MapFrom(x => x.Name.ToString()));
            
            configuration.CreateMap<CreatePlanItemsDto, AllInputs.PlanItems>();

            configuration.CreateMap<AllInputs.PlanItems, PlanItemsSummaryForReturnDto>()
                .ForMember(dto => dto.Description, options => options.MapFrom(x => x.Description.ToString()))
                .ForMember(dto => dto.ProcedureEntryType,
                    options => options.MapFrom(x =>
                        x.ProcedureEntryType != null ? x.ProcedureEntryType.ToString() : string.Empty))
                .ForMember(dto => dto.ProcedureId, options => options.MapFrom(x => x.ProcedureId));
            
            configuration.CreateMap<CreateInputNotesDto, AllInputs.InputNotes>();

            configuration.CreateMap<AllInputs.InputNotes, InputNotesSummaryForReturnDto>()
                .ForMember(dto => dto.Description, options => options.MapFrom(x => x.Description.ToString()));

            configuration.CreateMap<CreateWoundDressingDto, AllInputs.WoundDressing>();

            configuration.CreateMap<AllInputs.WoundDressing, WoundDressingSummaryForReturnDto>()
                .ForMember(dto => dto.Description, options => options.MapFrom(x => x.Description.ToString()));
            
            configuration.CreateMap<CreateSymptomDto, AllInputs.Symptom>();
            
            configuration.CreateMap<AllInputs.Symptom, PatientSymptomSummaryForReturnDto>()
                .ForMember(dto => dto.SymptomEntryTypeName, options => options.MapFrom(x => x.SymptomEntryType.ToString()));
            
            configuration.CreateMap<SuggestionQuestionForCreationDto, SymptomSuggestionQuestionDto>()
                .ForMember(dto => dto.SuggestionQuestionType, options => options.MapFrom(x => (SuggestionQuestionType)Enum.Parse(typeof(SuggestionQuestionType), x.SuggestionQuestionType, true)));
           
            configuration.CreateMap<CreateMedicationDto, AllInputs.Medication>();
            configuration.CreateMap<AllInputs.Medication, PatientMedicationForReturnDto>()
                .ForMember(dto => dto.ProcedureEntryType,
                    options => options.MapFrom(x =>
                        x.ProcedureEntryType != null ? x.ProcedureEntryType.ToString() : string.Empty));
            configuration.CreateMap<MedicationAdministrationActivityRequest, MedicationAdministrationActivity>().ReverseMap();


            //Diagnosis
            configuration.CreateMap<CreateDiagnosisDto, Diagnosis>();
            configuration.CreateMap<Diagnosis, PatientDiagnosisReturnDto>();


            // Invoice
            configuration.CreateMap<Invoice, CreateNewInvoiceCommand>()
                .ForMember(dto => dto.InvoiceNo, options => options.MapFrom(x => x.InvoiceId))
                .ForMember(dto => dto.AppointmentId, options => options.MapFrom(x => x.PatientAppointmentId))
                .ForMember(dto => dto.TotalAmount, options => options.MapFrom(x => x.TotalAmount))
                .ReverseMap();
            configuration.CreateMap<Invoice, CreateNewInvestigationInvoiceCommand>()
                .ForMember(dto => dto.InvoiceNo, options => options.MapFrom(x => x.InvoiceId))
                .ForMember(dto => dto.AppointmentId, options => options.MapFrom(x => x.PatientAppointmentId))
                .ForMember(dto => dto.TotalAmount, options => options.MapFrom(x => x.TotalAmount))
                .ReverseMap();
            configuration.CreateMap<InvoiceItem, InvoiceItemRequest>()
                .ForMember(dto => dto.SubTotal, options => options.MapFrom(x => x.SubTotal))
                .ForMember(dto => dto.UnitPrice, options => options.MapFrom(x => x.UnitPrice))
                .ReverseMap();
            configuration.CreateMap<MoneyDto, Money>()
                .ConstructUsing(dto => new Money(dto.Amount, dto.Currency))
                .ReverseMap();
            configuration.CreateMap<CreateNewPricingCommandRequest, ItemPricing>().ReverseMap();
            configuration.CreateMap<CreatePriceConsultationSettingsCommand, PriceConsultationSetting>().ReverseMap();
            configuration.CreateMap<CreatePriceWardAdmissionSettingCommand, PriceWardAdmissionSetting>().ReverseMap();
            configuration.CreateMap<CreatePriceMealSettingCommand, PriceMealSetting>().ReverseMap();
            configuration.CreateMap<PriceMealPlanDefinitionCommand, PriceMealPlanDefinition>().ReverseMap();
            configuration.CreateMap<CreatePriceDiscountSettingCommand, PricingDiscountSetting>().ReverseMap();
            configuration.CreateMap<PriceCategoryDiscountCommand, PriceCategoryDiscount>().ReverseMap();
            configuration.CreateMap<CreateInvestigationPricingDto, InvestigationPricing>().ReverseMap();

            // Vaccines
            configuration.CreateMap<Vaccine, GetAllVaccinesResponse>();
            configuration.CreateMap<Vaccine, GetVaccineResponse>();
            configuration.CreateMap<VaccineSchedule, VaccineScheduleDto>();
            configuration.CreateMap<VaccineGroup, GetAllVaccineGroupsResponse>();
            configuration.CreateMap<VaccineGroup, GetVaccineGroupResponse>();
            
            configuration.CreateMap<CreateVaccinationDto, Vaccination>();
            configuration.CreateMap<CreateVaccinationHistoryDto, VaccinationHistory>();
            
            configuration.CreateMap<Vaccination, VaccinationResponseDto>()
                .ForMember(dto => dto.ProcedureEntryType,
                    options => options.MapFrom(x =>
                        x.ProcedureEntryType != null ? x.ProcedureEntryType.ToString() : string.Empty))
                .ForMember(dto => dto.ProcedureId, options => options.MapFrom(x => x.Id));
            configuration.CreateMap<VaccinationHistory, VaccinationHistoryResponseDto>();


            // Investigations
            configuration.CreateMap<Investigation, GetInvestigationResponse>();
            configuration.CreateMap<InvestigationRange, InvestigationRangeDto>();
            configuration.CreateMap<InvestigationSuggestion, InvestigationSuggestionDto>();
            configuration.CreateMap<InvestigationBinaryResult, InvestigationResultsDto>();
            configuration.CreateMap<DipstickInvestigation, DipstickDto>();
            configuration.CreateMap<DipstickRange, DipstickRangeDto>();
            configuration.CreateMap<DipstickResult, DipstickResultDto>();

            configuration.CreateMap<RecordInvestigationRequest, InvestigationResult>();
            configuration.CreateMap<InvestigationComponentResultDto, InvestigationComponentResult>().ReverseMap();
            configuration.CreateMap<InvestigationResult, GetInvestigationResultsResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(x => x.Investigation.Type));


            configuration.CreateMap<CreateProcedureDto, Procedure>();
            configuration.CreateMap<Procedure, PatientProcedureResponseDto>()
                .ForMember(dto => dto.SelectedProcedures,
                    options => options.MapFrom(x =>
                        JsonConvert.DeserializeObject<List<SelectedProcedureDto>>(x.SelectedProcedures)))
                .ForMember(dto => dto.ProcedureEntryType,
                    options => options.MapFrom(x =>
                        x.ProcedureEntryType != null ? x.ProcedureEntryType.ToString() : string.Empty));
            
            configuration.CreateMap<CreateNoteTemplateDto, NoteTemplate>();
            configuration.CreateMap<NoteTemplate, NoteTemplateResponseDto>()
                .ForMember(dto => dto.NoteTypeName, options => options.MapFrom(x => x.NoteType.ToString()));
            configuration.CreateMap<CreateProcedureNoteDto, ProcedureNote>();
            configuration.CreateMap<ProcedureNote, NoteResponseDto>();
            configuration.CreateMap<CreateAnaesthesiaNoteDto, AnaesthesiaNote>();
            configuration.CreateMap<AnaesthesiaNote, NoteResponseDto>();
            
            configuration.CreateMap<CreateStatementOfHealthProfessionalDto, ProcedureConsentStatement>();
            configuration.CreateMap<CreateStatementOfPatientOrNextOfKinOrGuardianDto, ProcedureConsentStatement>();
            configuration.CreateMap<SignConfirmationOfConsentDto, ProcedureConsentStatement>().ReverseMap();
            
            configuration.CreateMap<ProcedureConsentStatement, StatementOfHealthProfessionalResponseDto>();
            configuration.CreateMap<ProcedureConsentStatement, StatementOfPatientOrNextOfKinOrGuardianResponseDto>()
                .ForMember(dto => dto.AdditionalProcedures,
                    options => options.MapFrom(x =>
                        JsonConvert.DeserializeObject<List<AddtionalProcedure>>(x.AdditionalProcedures)));

            configuration.CreateMap<CreateSpecializedProcedureDto, SpecializedProcedure>();
            configuration.CreateMap<ScheduleProcedureDto, ScheduleProcedure>();
            configuration.CreateMap<SpecializedProcedure, SpecializedProcedureResponseDto>();
            configuration.CreateMap<ScheduleProcedure, ScheduledProcedureResponseDto>();
            configuration.CreateMap<SpecializedProcedureNurseDetail, CreateSpecializedProcedureNurseDetailCommand>()
                .ReverseMap();
            

            // Vital Signs
            configuration.CreateMap<VitalSign, GetAllVitalSignsResponse>();
            configuration.CreateMap<VitalSign, GetSimpleVitalSignsResponse>();
            
            configuration.CreateMap<MeasurementSite, MeasurementSiteDto>();
            configuration.CreateMap<MeasurementRange, MeasurementRangeDto>();

            configuration.CreateMap<GCSScoring, GetGCSScoringResponse>();
            configuration.CreateMap<GCSScoringRange, GCSScoringRangeDto>();

            configuration.CreateMap<ApgarScoring, GetApgarScoringResponse>();
            configuration.CreateMap<ApgarScoringRange, ApgarScoringRangeDto>();
            
            configuration.CreateMap<CreatePatientVitalDto, PatientVital>();
            configuration.CreateMap<RecheckPatientVitalDto, PatientVital>()
                .ForMember(dto => dto.Id, options => options.Ignore());
            configuration.CreateMap<PatientVital, PatientVitalResponseDto>()
                .ForMember(dto => dto.PatientVitalType, options => options.MapFrom(x => x.PatientVitalType.ToString()))
                .ForMember(dto => dto.Position, options => options.MapFrom(x => x.VitalPosition.ToString()))
                .ForMember(dto => dto.ProcedureEntryType,
                    options => options.MapFrom(x =>
                        x.ProcedureEntryType != null ? x.ProcedureEntryType.ToString() : string.Empty))
                .ForMember(dto => dto.ProcedureId, options => options.MapFrom(x => x.ProcedureId));
            
            

            // Physical Examination
            configuration.CreateMap<PhysicalExamination, GetPhysicalExaminationListResponse>()
                .ForMember(x => x.HasQualifiers,
                    x => x.MapFrom(y => y.Plane || y.Site || !string.IsNullOrWhiteSpace(y.Qualifiers)));
            configuration.CreateMap<PhysicalExamination, GetPhysicalExaminationResponse>()
                .ForMember(x => x.Qualifiers, x => x.Ignore());
            configuration.CreateMap<ExaminationQualifier, ExaminationQualifierDto>();
            configuration.CreateMap<PhysicalExaminationType, GetPhysicalExaminationTypeResponseDto>();
            
            configuration.CreateMap<CreatePatientPhysicalExaminationDto, PatientPhysicalExamination>();
            configuration.CreateMap<PatientPhysicalExamTypeNoteRequestDto, PatientPhysicalExamTypeNote>().ReverseMap();
            configuration.CreateMap<PatientPhysicalExamSuggestionQuestionDto, PatientPhysicalExamSuggestionQuestion>().ReverseMap();
            configuration.CreateMap<PatientPhysicalExamSuggestionAnswerDto, PatientPhysicalExamSuggestionAnswer>().ReverseMap();
            configuration.CreateMap<PatientPhysicalExamSuggestionQualifierDto, PatientPhysicalExamSuggestionQualifier>().ReverseMap();
            
            configuration.CreateMap<PatientPhysicalExaminationImageFile, PatientPhysicalExaminationImageFileResponseDto>();
            configuration.CreateMap<PatientPhysicalExamination, PatientPhysicalExaminationResponseDto>()
                .ForMember(dto => dto.PhysicalExaminationEntryTypeName,
                    options => options.MapFrom(x => x.PhysicalExaminationEntryType.ToString()))
                .ForMember(dto => dto.ProcedureEntryType,
                    options => options.MapFrom(x =>
                        x.ProcedureEntryType != null ? x.ProcedureEntryType.ToString() : string.Empty))
                .ForMember(dto => dto.ProcedureId, options => options.MapFrom(x => x.ProcedureId));


            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */

            //Patient Implant
            configuration.CreateMap<CreatePatientImplantCommandRequestDto, PatientImplant>();

            //Surgical History
            configuration.CreateMap<CreateBloodTransfusionHistoryRequestDto, BloodTransfusionHistory>().ReverseMap();
            configuration.CreateMap<CreateSurgicalHistoryRequestDto, SurgicalHistory>().ReverseMap();
            configuration.CreateMap<GetSurgicalHistoryResponseDto, SurgicalHistory>().ReverseMap();
            configuration.CreateMap<GetPatientBloodTransfusionHistoryResponseDto, BloodTransfusionHistory>().ReverseMap();
            
            

            configuration.CreateMap<CreateAlcoholHistoryRequestDto, AlcoholHistory>().ReverseMap();
            configuration.CreateMap<GetAlcoholHistoryResponseDto, AlcoholHistory>().ReverseMap();
            configuration.CreateMap<CreateCigaretteHistoryRequestDto, CigeretteAndTobaccoHistory>().ReverseMap();
            configuration.CreateMap<GetCigaretteHistoryResponseDto, CigeretteAndTobaccoHistory>().ReverseMap();
            configuration.CreateMap<CreateRecreationalDrugsHistoryRequestDto, RecreationalDrugHistory>().ReverseMap();
            configuration.CreateMap<GetRecreationalDrugHistoryResponseDto, RecreationalDrugHistory>().ReverseMap();
            configuration.CreateMap<DrugHistory, CreateDrugHistoryRequestDto>().ReverseMap();
            configuration.CreateMap<DrugHistory, GetDrugHistoryResponseDto>().ReverseMap();
            configuration.CreateMap<CreateReviewOfSystemsRequestDto, ReviewOfSystem>().ReverseMap();
            configuration.CreateMap<ReviewOfSystem, GetPatientReviewOfSystemsDataResponseDto>().ReverseMap();
            configuration.CreateMap<OccupationalHistory, CreateOccupationalHistoryDto>().ReverseMap();
            configuration.CreateMap<ReviewDetailsHistoryState, ReviewDetailsHistoryStateDto>().ReverseMap();

            //Encounters
            configuration.CreateMap<CreateAppointmentEncounterRequest, PatientEncounter>().ReverseMap();
            configuration.CreateMap<CreateAdmissionEncounterRequest, PatientEncounter>().ReverseMap();
        }
    }
}
