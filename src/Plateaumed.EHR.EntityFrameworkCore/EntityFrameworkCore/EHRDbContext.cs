using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.IdentityServer4vNext;
using Abp.Localization;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Plateaumed.EHR.Authorization.Delegation;
using Plateaumed.EHR.Authorization.Roles;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Chat;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.Friendships;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.MultiTenancy.Accounting;
using Plateaumed.EHR.MultiTenancy.Payments;
using Plateaumed.EHR.Storage;
using Plateaumed.EHR.Insurance;
using Plateaumed.EHR.Organizations;
using Plateaumed.EHR.Misc;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Plateaumed.EHR.AllInputs;
using Plateaumed.EHR.EntityFrameworkCore.Interceptors;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.VitalSigns;
using Plateaumed.EHR.WardEmergencies;

namespace Plateaumed.EHR.EntityFrameworkCore
{
    public class EHRDbContext
        : AbpZeroDbContext<Tenant, Role, User, EHRDbContext>,
            IAbpPersistedGrantDbContext
    {
        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

        public virtual DbSet<PatientAppointment> PatientAppointments { get; set; }

        public virtual DbSet<PatientReferralDocument> PatientReferralDocuments { get; set; }

        public virtual DbSet<PatientInsurer> PatientInsurers { get; set; }

        public virtual DbSet<PatientRelation> PatientRelations { get; set; }

        public virtual DbSet<PatientRelationDiagnosis> PatientRelationsDiagnoses { get; set; }
        
        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Occupation> Occupations { get; set; }

        public virtual DbSet<OccupationCategory> OccupationCategories { get; set; }

        public virtual DbSet<PatientOccupation> PatientOccupations { get; set; }

        public virtual DbSet<OrganizationUnitTime> OrganizationUnitTimes { get; set; }

        public virtual DbSet<FacilityDocument> FacilityDocuments { get; set; }

        public virtual DbSet<FacilityInsurer> FacilityInsurers { get; set; }

        public virtual DbSet<InsuranceProvider> InsuranceProviders { get; set; }

        public virtual DbSet<FacilityStaff> FacilityStaff { get; set; }

        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        
        public virtual DbSet<AllInputs.Symptom> Symptoms { get; set; }

        public virtual DbSet<BedMaking> BedMakings { get; set; }
        
        public virtual DbSet<AllInputs.Feeding> Feeding { get; set; }
        
        public virtual DbSet<AllInputs.FeedingSuggestions> FeedingSuggestions { get; set; }
        
        public virtual DbSet<ProcedureConsentStatement> ProcedureConsentStatement { get; set; }
        
        public virtual DbSet<SpecializedProcedure> SpecializedProcedures { get; set; }

        public virtual DbSet<ScheduleProcedure> ScheduleProcedures { get; set; }

        public virtual DbSet<PlanItems> PlanItems { get; set; }

        public virtual DbSet<InputNotes> InputNotes { get; set; }
        
        public virtual DbSet<WoundDressing> WoundDressing { get; set; }
        
        public virtual DbSet<Meals> Meals { get; set; }

        public virtual DbSet<SnowmedBodyPart> SnowmedBodyParts { get; set; }
        
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        
        public virtual DbSet<GenericDrug> GenericDrugs { get; set; }
        
        public virtual DbSet<Product> Products { get; set; } 
        
        public virtual DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
        
        public virtual DbSet<SnowmedSuggestion> SnowmedSuggestions { get; set; }
        
        public virtual DbSet<Medication> Medications { get; set; }

        public virtual DbSet<TenantDocument> TenantDocuments { get; set; }

        public virtual DbSet<WardBed> WardBeds { get; set; }

        public virtual DbSet<Ward> Wards { get; set; }

        public virtual DbSet<BedType> BedTypes { get; set; }

        public virtual DbSet<Facility> Facilities { get; set; }

        public virtual DbSet<FacilityGroup> FacilityGroups { get; set; }

        public virtual DbSet<FacilityType> FacilityTypes { get; set; }

        public virtual DbSet<StaffCodeTemplate> StaffCodeTemplates { get; set; }

        public virtual DbSet<PatientCodeTemplate> PatientCodeTemplates { get; set; }

        public virtual DbSet<JobLevel> JobLevels { get; set; }

        public virtual DbSet<JobTitle> JobTitles { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<OrganizationUnitExtended> OrganizationUnitExtended { get; set; }

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<TenantInvoice> TenantInvoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<Region> Regions {get; set; }

        public virtual DbSet<District> Districts {get; set;}

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }
        
        public virtual DbSet<SerialCode> SerialCodes { get; set; }
        
        public virtual DbSet<PatientCodeMapping> PatientCodeMappings { get; set; }
        
        public virtual DbSet<PricingDiscountSetting> PricingDiscountSettings { get; set; }
        
        public virtual DbSet<ItemPricing> ItemPricing { get; set; }
        
        public virtual DbSet<ItemPricingCategory> ItemPricingCategories { get; set; }
        
        public virtual DbSet<PatientEncounter> PatientEncounters { get; set; }
        public virtual DbSet<StaffEncounter> StaffEncounters { get; set; }

        public virtual DbSet<PatientScanDocument> PatientScanDocuments { get; set; }

        public virtual DbSet<Diagnoses.Diagnosis> Diagnosis { get; set; }
        
        public virtual DbSet<Wallet> Wallets { get; set; }
        
        public virtual DbSet<WalletHistory> WalletHistory { get; set; }

        public virtual DbSet<PaymentActivityLog> PaymentActivityLogs { get; set; }

        public virtual DbSet<InvoiceRefundRequest> InvoiceRefundRequests { get; set; }

        public virtual DbSet<Rooms> Rooms { get; set; }

        public virtual DbSet<RoomAvailability> RoomAvailabilities { get; set; }

        public virtual DbSet<InvoiceCancelRequest> InvoiceCancelRequests { get; set; }

        public virtual DbSet<Vaccine> Vaccines { get; set; }

        public virtual DbSet<VaccineGroup> VaccineGroups { get; set; }
        
        public virtual DbSet<Vaccination> Vaccinations { get; set; }
        
        public virtual DbSet<VaccinationHistory> VaccineHistories { get; set; }

        public virtual DbSet<Procedure> Procedures { get; set; }
        
        public virtual DbSet<FacilityBank> FacilityBanks { get; set; }
        
        public virtual DbSet<PatientTravelHistory> PatientTravelHistories { get; set; }

        public virtual DbSet<Investigation> Investigations { get; set; }

        public virtual DbSet<InvestigationResultReviewer> InvestigationResultReviewer { get; set; }

        public virtual DbSet<InvestigationSuggestion> InvestigationSuggestions { get; set; }

        public virtual DbSet<PatientPhysicalExercise> PatientPhysicalExercises { get; set; }
        
        public virtual DbSet<ChronicDisease> ChronicDiseases { get; set; }
        
        public virtual DbSet<PatientAllergy> PatientAllergies { get; set; }
        
        public virtual DbSet<PatientAllergyTypeSuggestion> PatientAllergyTypeSuggestions { get; set; }
        
        public virtual DbSet<AllergyReactionExperiencedSuggestion> AllergyReactionExperiencedSuggestions { get; set; }
        
        public virtual DbSet<PatientPastMedicalCondition> PatientPastMedicalConditions { get; set; }

        public virtual DbSet<PatientMajorInjury> PatientMajorInjuries { get; set; }
        
        public virtual DbSet<PatientMensurationDuration> PatientMensurationDurations { get; set; }

        public virtual DbSet<PatientMenstrualFlow> PatientMenstrualFlows { get; set; }
        
        public virtual DbSet<PatientMenstrualPain> PatientMenstrualPains { get; set; }
        
        public virtual DbSet<PatientMenarcheAndMenopause> PatientMenarcheAndMenopause { get; set; }
        
        public virtual DbSet<PatientContraception> PatientContraception { get; set; }
        
        public virtual DbSet<PatientProductivePlan> PatientProductivePlans { get; set; }

        public virtual DbSet<PatientGynaecologicalIllness> PatientGynaecologicalIllness { get; set; }

        public virtual DbSet<PatientGynaecologicalProcedure> PatientGynaecologicalProcedures { get; set; }
        
        public virtual DbSet<PostmenopausalSymptomSuggestion> PostmenopausalSymptomSuggestions { get; set; }
        
        public virtual DbSet<TypeOfContraceptionSuggestion> TypeOfContraceptionSuggestions { get; set; }
        
        public virtual DbSet<VitalSign> VitalSigns { get; set; }
        
        public virtual DbSet<PatientGynaecologicalIllnessSuggestion> PatientGynaecologicalIllnessSuggestions { get; set; }
        
        public virtual DbSet<GCSScoring> GCSScorings{ get; set; }
        
        public virtual DbSet<ApgarScoring> ApgarScorings{ get; set; }
        
        public virtual DbSet<PatientGynaecologicProcedureSuggestion> PatientGynaecologicProcedureSuggestions { get; set; }
        
        public virtual DbSet<DelegatedUser> DelegatedUsers{ get; set; }
        
        public virtual DbSet<PatientImplantSuggestion> PatientImplantSuggestions { get; set; }

        public virtual DbSet<PatientImplant> PatientImplants { get; set; }

        public virtual DbSet<InvestigationRequest> InvestigationRequests { get; set; }

        public virtual DbSet<PhysicalExamination> PhysicalExaminations { get; set; }
        
        public virtual DbSet<PhysicalExaminationType> PhysicalExaminationTypes { get; set; }
        
        public virtual DbSet<PatientPhysicalExamination> PatientPhysicalExaminations { get; set; }
        
        public virtual DbSet<PatientPhysicalExaminationImageFile> PatientPhysicalExaminationImageFiles { get; set; }

        public virtual DbSet<ExaminationQualifier> ExaminationQualifiers { get; set; }

        public virtual DbSet<DiagnosisSuggestion> DiagnosisSuggestions { get; set; }

        public virtual DbSet<ProcedureSuggestion> ProcedureSuggestions { get; set; }

        public virtual DbSet<InvestigationResult> InvestigationResults { get; set; }

        public virtual DbSet<InvestigationComponentResult> InvestigationComponentResult { get; set; }

        public virtual DbSet<PriceWardAdmissionSetting> PriceWardAdmissionSettings { get; set; }

        public virtual DbSet<PriceMealSetting> PriceMealSettings { get; set; }

        public virtual DbSet<PriceMealPlanDefinition> PriceMealPlanDefinitions { get; set; }

        public virtual DbSet<PriceCategoryDiscount> PriceCategoryDiscounts { get; set; }
        public virtual DbSet<PriceConsultationSetting> PriceConsultationSettings { get; set; }
        public virtual DbSet<ReviewOfSystemsSuggestion> ReviewOfSystemsSuggestions { get; set; }
        
        public virtual DbSet<BloodTransfusionHistory> BloodTransfusionHistories { get; set; }

        public virtual DbSet<InvestigationPricing > Pricings { get; set; }

        public virtual DbSet<SurgicalHistory> SurgicalHistories { get; set; }
        
        public virtual DbSet<PatientVital> PatientVitals { get; set; }
        

        public virtual DbSet<AlcoholHistory> AlcoholHistories { get; set; }

        public virtual DbSet<RecreationalDrugHistory> RecreationalDrugHistories { get; set; }

        public virtual DbSet<CigeretteAndTobaccoHistory> CigeretteAndTobaccoHistories { get; set; }

        public virtual DbSet<RecreationalDrugSuggestion> RecreationalDrugSuggestions { get; set; }
        
        public virtual DbSet<AlcoholType> AlcoholTypes { get; set; }
        
        public virtual DbSet<TobaccoSuggestion> TobaccoSuggestions { get; set; }

        public virtual DbSet<Discharge> Discharges { get; set; }
        public virtual DbSet<DischargeMedication> DischargeMedications { get; set; }
        public virtual DbSet<DischargePlanItem> DischargePlanItems { get; set; }
        public virtual DbSet<DrugHistory> DrugHistories { get; set; }
        public virtual DbSet<VaccineSchedule> VaccineSchedules { get; set; }
        public virtual DbSet<Admission> Admissions { get; set; }

        public virtual DbSet<NursingActivity> NursingActivities { get; set; }
        public virtual DbSet<NursingCareIntervention> NursingCareInterventions { get; set; }
        public virtual DbSet<NursingDiagnosis> NursingDiagnosis { get; set; }
        public virtual DbSet<NursingEvaluation> NursingEvaluation { get; set; }
        public virtual DbSet<NursingOutcome> NursingOutComes { get; set; }
        public virtual DbSet<NursingCareSummary> NursingCareSummary { get; set; }

        public virtual DbSet<WardEmergency> WardEmergency { get; set; }

        public virtual DbSet<NursingIntervention> NursingInterventions { get; set; }

        public virtual DbSet<PatientIntervention> PatientInterventions { get; set; }
        public virtual DbSet<VaccinationSuggestion> VaccinationSuggestions { get; set; }
        public virtual DbSet<PatientPastMedicalConditionMedication> PatientPastMedicalConditionMedications { get; set; }
        public virtual DbSet<PatientFamilyHistory> PatientFamilyHistories { get; set; }
        public virtual DbSet<PatientFamilyMembers> PatientFamilyMembers { get; set; }
        public virtual DbSet<IntakeOutputCharting> IntakeOutputChartings { get; set; }

        public virtual DbSet<ReviewOfSystem> ReviewOfSystems { get; set; }

        public virtual DbSet<OccupationalHistory> OccupationalHistories { get; set; }

        public virtual DbSet<ReviewDetailsHistoryState> ReviewDetailsHistoryStates { get; set; }
        
        public virtual DbSet<NoteTemplate> NoteTemplates { get; set; }
        
        public virtual DbSet<ProcedureNote> ProcedureNotes { get; set; }
        
        public virtual DbSet<AnaesthesiaNote> AnaesthesiaNotes { get; set; }
        public virtual DbSet<PatientReviewDetailedHistory> PatientReviewDetailedHistories { get; set; }
        
        public virtual DbSet<NursingHistory> NursingHistories { get; set; }
        public virtual DbSet<PatientReferralOrConsultLetter> PatientReferralOrConsultLetters { get; set; }

        public virtual DbSet<PatientStability> PatientStability { get; set; }
        public virtual DbSet<SpecializedProcedureNurseDetail> SpecializedProcedureNurseDetails { get; set; }
        public virtual DbSet<DischargeNote> DischargeNotes { get; set; }
        public virtual DbSet<PatientCauseOfDeath> PatientCausesOfDeath { get; set; }
        public virtual DbSet<SpecializedProcedureSafetyCheckList> SpecializedProcedureSafetyCheckLists { get; set; }

        public virtual DbSet<ElectroRadPulmInvestigationResult> ElectroRadPulmInvestigationResult { get; set; }

        public virtual DbSet<ElectroRadPulmInvestigationResultImages> ElectroRadPulmInvestigationResultImages { get; set; }
        public virtual DbSet<NursingRecord> NursingRecords { get; set; }
        public virtual DbSet<MeasurementRange> MeasurementRange { get; set; }
        public virtual DbSet<RadiologyAndPulmonaryInvestigation> RadiologyAndPulmonaryInvestigation { get; set; }
        public virtual DbSet<MedicationAdministrationActivity> MedicationAdministrationActivities { get; set; }

        [Obsolete("Obsolete")]
        public EHRDbContext(DbContextOptions<EHRDbContext> options) : base(options)
        {
            //workaround for issue: https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //FIXME: Use NpgsqlDataSourceBuilder, GlobalTypeMapper is obsolete
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TitleType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GenderType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<IdentificationType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UserType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InsuranceProviderType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<FacilityLevel>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AppointmentRepeatType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AppointmentStatusType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AppointmentType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TenantDocumentType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<DaysOfTheWeek>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Religion>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<MaritalStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<BloodGenotype>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<BloodGroup>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InsuranceBenefiaryType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Relationship>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TenantOnboardingStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<OrganizationUnitType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PaymentTypes>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PaymentStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InvoiceType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InvoiceSource>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionAction>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InvoiceItemStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionSource>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InvoiceRefundStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<WalletFundingStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InvoiceCancelStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UnitOfTime>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Intensity>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<BloodGroupAndGenotypeSource>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ConditionControl>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Severity>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<MenstrualFlowType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PricingType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PricingCategory>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<SymptomsCategory>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PriceTimeFrequency>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<InvestigationStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EncounterStatusType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ServiceCentreType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<PatientStabilityStatus>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.AddInterceptors(new StaffEncounterEntityInterceptor());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EHRDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresEnum<TitleType>();
            modelBuilder.HasPostgresEnum<GenderType>();
            modelBuilder.HasPostgresEnum<IdentificationType>();
            modelBuilder.HasPostgresEnum<UserType>();
            modelBuilder.HasPostgresEnum<InsuranceProviderType>();
            modelBuilder.HasPostgresEnum<FacilityLevel>();
            modelBuilder.HasPostgresEnum<AppointmentRepeatType>();
            modelBuilder.HasPostgresEnum<AppointmentStatusType>();
            modelBuilder.HasPostgresEnum<AppointmentType>();
            modelBuilder.HasPostgresEnum<TenantDocumentType>();
            modelBuilder.HasPostgresEnum<DaysOfTheWeek>();
            modelBuilder.HasPostgresEnum<Religion>();
            modelBuilder.HasPostgresEnum<MaritalStatus>();
            modelBuilder.HasPostgresEnum<BloodGenotype>();
            modelBuilder.HasPostgresEnum<BloodGroup>();
            modelBuilder.HasPostgresEnum<InsuranceBenefiaryType>();
            modelBuilder.HasPostgresEnum<Relationship>();
            modelBuilder.HasPostgresEnum<TenantOnboardingStatus>();
            modelBuilder.HasPostgresEnum<OrganizationUnitType>();
            modelBuilder.HasPostgresEnum<PaymentTypes>();
            modelBuilder.HasPostgresEnum<PaymentStatus>();
            modelBuilder.HasPostgresEnum<InvoiceType>();
            modelBuilder.HasPostgresEnum<InvoiceSource>();
            modelBuilder.HasPostgresEnum<TransactionType>();
            modelBuilder.HasPostgresEnum<TransactionAction>();
            modelBuilder.HasPostgresEnum<InvoiceItemStatus>();
            modelBuilder.HasPostgresEnum<TransactionSource>();
            modelBuilder.HasPostgresEnum<TransactionStatus>();
            modelBuilder.HasPostgresEnum<InvoiceRefundStatus>();
            modelBuilder.HasPostgresEnum<RoomType>();
            modelBuilder.HasPostgresEnum<WalletFundingStatus>();
            modelBuilder.HasPostgresEnum<InvoiceCancelStatus>();
            modelBuilder.HasPostgresEnum<UnitOfTime>();
            modelBuilder.HasPostgresEnum<Intensity>();
            modelBuilder.HasPostgresEnum<BloodGroupAndGenotypeSource>();
            modelBuilder.HasPostgresEnum<ConditionControl>();
            modelBuilder.HasPostgresEnum<Severity>();
            modelBuilder.HasPostgresEnum<MenstrualFlowType>();
            modelBuilder.HasPostgresEnum<PricingType>();
            modelBuilder.HasPostgresEnum<PricingCategory>();
            modelBuilder.HasPostgresEnum<SymptomsCategory>();
            modelBuilder.HasPostgresEnum<PriceTimeFrequency>();
            modelBuilder.HasPostgresEnum<InvestigationStatus>();
            modelBuilder.HasPostgresEnum<EncounterStatusType>();
            modelBuilder.HasPostgresEnum<ServiceCentreType>();
            modelBuilder.HasPostgresEnum<PatientStabilityStatus>();

            modelBuilder.Entity<Wallet>().OwnsOne(x => x.Balance).WithOwner();
            modelBuilder.Entity<WalletHistory>().OwnsOne(x => x.Amount).WithOwner();
            modelBuilder.Entity<WalletHistory>().OwnsOne(x => x.CurrentBalance).WithOwner();
            modelBuilder.Entity<Invoice>().OwnsOne(x => x.DiscountPercentage).WithOwner();
            modelBuilder.Entity<Invoice>().OwnsOne(x=>x.TotalAmount).WithOwner();
            modelBuilder.Entity<Invoice>().OwnsOne(x=>x.OutstandingAmount).WithOwner();
            modelBuilder.Entity<Invoice>().OwnsOne(x=>x.AmountPaid).WithOwner();
            modelBuilder.Entity<InvoiceItem>().OwnsOne(x => x.SubTotal).WithOwner();
            modelBuilder.Entity<InvoiceItem>().OwnsOne(x=>x.DiscountAmount).WithOwner();
            modelBuilder.Entity<InvoiceItem>().OwnsOne(x => x.AmountPaid).WithOwner();
            modelBuilder.Entity<InvoiceItem>().OwnsOne(x=>x.UnitPrice).WithOwner();
            modelBuilder.Entity<InvoiceItem>().OwnsOne(x=>x.OutstandingAmount).WithOwner();
            modelBuilder.Entity<InvoiceItem>().OwnsOne(x => x.DebtReliefAmount).WithOwner();
            modelBuilder.Entity<ItemPricing>().OwnsOne(x => x.Amount).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.ToUpAmount).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.EditAmount).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.ActualAmount).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.AmountRefund).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.AmountPaid).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.OutStandingAmount).WithOwner();
            modelBuilder.Entity<PaymentActivityLog>().OwnsOne(x => x.ReliefAmount).WithOwner();
            modelBuilder.Entity<InvestigationPricing>().OwnsOne(x => x.Amount).WithOwner();
            modelBuilder.Entity<Invoice>(i =>
            {
                i.HasIndex(e => new { e.TenantId });

                i.HasMany(i => i.InvoiceItems)
                .WithOne(it => it.InvoiceFk)
                .HasForeignKey(it => it.InvoiceId);
            });

            modelBuilder.Entity<InvoiceItem>(i =>
            {
                i.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<PatientAppointment>(p =>
            {
                p.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<PatientInsurer>(p =>
            {
                p.HasIndex(e => new { e.TenantId });
            });
          

            modelBuilder.Entity<Country>(c =>
            {
                c.HasIndex(e => new { e.Name });

                c.HasMany(x => x.Regions)
                    .WithOne(x => x.CountryFk)
                    .HasForeignKey(x => x.CountryId);
            });
            modelBuilder.Entity<SubscribableEdition>(c =>
            {
                c.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<Region>(c => {
                c.HasIndex(e => new{e.Name});
                c.HasOne(x => x.CountryFk)
                    .WithMany(e => e.Regions)
                    .HasForeignKey(e => e.CountryId);

                c.HasMany(e => e.Districts)
                 .WithOne(e => e.RegionFk)
                 .HasForeignKey(e => e.RegionId);
            });

            modelBuilder.Entity<District>(c => {
                c.HasIndex(e => new{e.Name});

                c.HasOne(e => e.RegionFk)
                 .WithMany(e => e.Districts)
                 .HasForeignKey(e => e.RegionId);
            });

            modelBuilder.Entity<OrganizationUnit>(o =>
            {
                o.UseTptMappingStrategy();
            });

            modelBuilder.Entity<OrganizationUnitExtended>(o =>
            {
                o.ToTable("AppOrganizationUnitsExtended");

                o.HasIndex(e => new { e.TenantId });

                o.HasMany(e => e.OperatingTimes)
                .WithOne(ot => ot.OrganizationUnitExtendedFk)
                .HasForeignKey(ot => ot.OrganizationUnitExtendedId);
            });

            modelBuilder.Entity<OrganizationUnitTime>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<FacilityDocument>(f =>
            {
                f.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<FacilityInsurer>(f =>
            {
                f.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<InsuranceProvider>(i =>
            {
                i.HasIndex(e => new { e.TenantId });
                i.Property(e => e.isActive)
                .HasDefaultValue(true);
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<StaffMember>(s =>
            {
                s.HasOne(s => s.UserFk)
                    .WithOne(u => u.StaffMemberFk)
                    .HasForeignKey<StaffMember>(s => s.UserId);

                s.HasMany(s => s.AssignedFacilities)
                    .WithOne(a => a.StaffMemberFk)
                    .HasForeignKey(a => a.StaffMemberId);

                s.HasMany(s => s.Jobs)
                    .WithOne(j => j.StaffMember)
                    .HasForeignKey(j => j.StaffMemberId);
            });

            modelBuilder.Entity<Job>(j =>
            {
                j.HasIndex(e => new { e.TenantId });
                j.HasMany(js => js.JobServiceCentres)
                    .WithOne(s => s.Job)
                    .HasForeignKey(s => s.JobId);
                j.HasMany(jw => jw.WardsJobs)
                    .WithOne(w => w.Job)
                    .HasForeignKey(w => w.JobId);
            });

            modelBuilder.Entity<WardJob>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<JobServiceCentre>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<TenantDocument>(t =>
            {
                t.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<AllInputs.Symptom>(s =>
            { 
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.Symptoms)
                    .HasForeignKey(s => s.PatientId); 
                
                s.Property(p => p.OtherNote).HasColumnType("varchar(2000)");
            });
            
            modelBuilder.Entity<BedMaking>(s =>
            { 
                s.Property(p => p.BedMakingSnowmedIds).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
                
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.BedMakings)
                    .HasForeignKey(s => s.PatientId); 
                
                s.Property(p => p.Note).HasColumnType("varchar(2000)");
            });
            
            modelBuilder.Entity<AllInputs.Feeding>(s =>
            { 
                s.Property(p => p.FeedingSnowmedIds).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
                
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.Feeding)
                    .HasForeignKey(s => s.PatientId); 
                
                s.Property(p => p.Description).HasColumnType("varchar(2000)");
            });
            
            modelBuilder.Entity<PlanItems>(s =>
            { 
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.PlanItems)
                    .HasForeignKey(s => s.PatientId); 
                
                s.Property(p => p.Description).HasColumnType("varchar(2000)");

                s.Property(p => p.PlanItemsSnowmedIds).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
            });
               
            modelBuilder.Entity<InputNotes>(s =>
            { 
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.InputNotes)
                    .HasForeignKey(s => s.PatientId); 
                
                s.Property(p => p.Description).HasColumnType("varchar(2000)");

                s.Property(p => p.InputNotesSnowmedIds).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
            });
            
            modelBuilder.Entity<WoundDressing>(s =>
            { 
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.WoundDressing)
                    .HasForeignKey(s => s.PatientId); 
                
                s.Property(p => p.Description).HasColumnType("varchar(2000)");

                s.Property(p => p.WoundDressingSnowmedIds).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
            });
            
            modelBuilder.Entity<Meals>(s =>
            { 
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.Meals)
                    .HasForeignKey(s => s.PatientId);

                s.Property(p => p.Description).HasColumnType("varchar(2000)");

                s.Property(p => p.MealSnowmedIds).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
            });
            
            modelBuilder.Entity<SnowmedBodyPart>(s =>
            {
                s.HasIndex(i => i.Part);
                s.HasIndex(i => i.Synonym);
                
                s.Property(p => p.SnowmedId).HasColumnType("varchar(300)");
                s.Property(p => p.Part).HasColumnType("varchar(100)");
                s.Property(p => p.SubPart).HasColumnType("varchar(255)");
                s.Property(p => p.Synonym).HasColumnType("varchar(300)");
                s.Property(p => p.Description).HasColumnType("varchar(500)");
                s.Property(p => p.Gender).HasColumnType("varchar(2)");
            });

            modelBuilder.Entity<InvestigationPricing >(x =>
            {
                x.Property(p => p.Notes).HasColumnType("varchar(300)");
                x.Property(p => p.IsActive).HasDefaultValue(true);


            });
            modelBuilder.Entity<SnowmedSuggestion>(s =>
            {  
                s.HasIndex(i => i.Type);
                
                s.Property(p => p.SourceSnowmedId).HasColumnType("varchar(255)");
                s.Property(p => p.SourceName).HasColumnType("varchar(255)");
                s.Property(p => p.Name).HasColumnType("varchar(255)");
                s.Property(p => p.SnowmedId).HasColumnType("varchar(255)"); 
            });
            
            modelBuilder.Entity<Product>(s =>
            {
                s.HasIndex(i => i.ProductName); 
                
                s.Property(p => p.ProductName).HasColumnType("varchar(500)");
                s.Property(p => p.NigeriaRegNo).HasColumnType("varchar(500)");
                s.Property(p => p.BrandName).HasColumnType("varchar(500)");
                s.Property(p => p.Manufacturer).HasColumnType("varchar(500)");
                s.Property(p => p.ActiveIngredients).HasColumnType("varchar(500)");
                s.Property(p => p.DoseFormName).HasColumnType("varchar(500)");
                s.Property(p => p.DoseStrengthName).HasColumnType("varchar(500)");
                s.Property(p => p.CountryOfManufacture).HasColumnType("varchar(500)");
                s.Property(p => p.Synonyms).HasColumnType("varchar(500)");
                s.Property(p => p.Synonyms2).HasColumnType("varchar(500)");
                s.Property(p => p.Synonyms3).HasColumnType("varchar(500)");
                s.Property(p => p.Notes).HasColumnType("varchar(2000)");
                s.Property(p => p.SnowmedId).HasColumnType("varchar(100)");
                s.Property(p => p.SnowmedCategoryId1).HasColumnType("varchar(100)");
                s.Property(p => p.SnowmedCategoryId2).HasColumnType("varchar(100)");
                s.Property(p => p.SnowmedCategoryId3).HasColumnType("varchar(100)");
                s.Property(p => p.SnowmedCategoryId4).HasColumnType("varchar(100)");
                s.Property(p => p.SnowmedCategoryId5).HasColumnType("varchar(100)");
                s.Property(p => p.SnowmedCategoryName1).HasColumnType("varchar(500)");
                s.Property(p => p.SnowmedCategoryName2).HasColumnType("varchar(500)");
                s.Property(p => p.SnowmedCategoryName3).HasColumnType("varchar(500)");
                s.Property(p => p.SnowmedCategoryName4).HasColumnType("varchar(500)");
                s.Property(p => p.SnowmedCategoryName5).HasColumnType("varchar(500)"); 
            });
            
            modelBuilder.Entity<GenericDrug>(s =>
            {  
                s.HasIndex(i => i.GenericSctName);
                s.Property(p => p.GenericSctName).HasColumnType("varchar(500)");
                s.Property(p => p.ActiveIngredients).HasColumnType("varchar(500)");
                s.Property(p => p.DoseForm).HasColumnType("varchar(500)"); 
            });
            
            modelBuilder.Entity<ProductCategory>(s =>
            {  
                s.HasIndex(i => i.CategoryName);
                s.Property(p => p.CategoryName).HasColumnType("varchar(255)"); 
            });
            
            modelBuilder.Entity<Medication>(s =>
            {  
                s.HasIndex(i => i.PatientId);
                
                s.Property(p => p.ProductName).HasColumnType("varchar(300)");
                s.Property(p => p.ProductSource).HasColumnType("varchar(50)");
                s.Property(p => p.DoseUnit).HasColumnType("varchar(100)");
                s.Property(p => p.Frequency).HasColumnType("varchar(100)"); 
                s.Property(p => p.Duration).HasColumnType("varchar(100)"); 
                s.Property(p => p.Direction).HasColumnType("varchar(500)"); 
                s.Property(p => p.Note).HasColumnType("varchar(2000)"); 
            });
            
            modelBuilder.Entity<PatientVital>(s =>
            {  
                s.HasIndex(i => i.PatientId);
                
                s.Property(p => p.VitalReading).HasColumnType("varchar(100)"); 
            });

            modelBuilder.Entity<WardBed>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });


            modelBuilder.Entity<Rooms>(r =>
            {
                r.HasIndex(e => new { e.TenantId, e.FacilityId }).IsUnique(false);

                r.HasMany(r => r.Availabilities)
                    .WithOne(ra => ra.Rooms)
                    .HasForeignKey(ra => ra.RoomsId);

            });

            modelBuilder.Entity<RoomAvailability>(a =>
            {
                a.HasOne(ra => ra.Rooms)
                    .WithMany(r => r.Availabilities)
                    .HasForeignKey(ra => ra.RoomsId);
            });


            modelBuilder.Entity<Diagnoses.Diagnosis>(diagnosis =>
            {
                diagnosis.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
            });
            modelBuilder.Entity<Ward>(w =>
            {
                w.HasIndex(e => new { e.TenantId });

                w.HasMany(wb => wb.WardBeds)
                .WithOne(w => w.WardFk)
                .HasForeignKey(w => w.WardId);
            });

            modelBuilder.Entity<BedType>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<Rooms>(r =>
            {
                r.HasIndex(e => new { e.TenantId, e.FacilityId }).IsUnique(false);

                r.HasMany(r => r.Availabilities)
                    .WithOne(ra => ra.Rooms)
                    .HasForeignKey(ra => ra.RoomsId);

            });

            modelBuilder.Entity<RoomAvailability>(a =>
            {
                a.HasOne(ra => ra.Rooms)
                    .WithMany(r => r.Availabilities)
                    .HasForeignKey(ra => ra.RoomsId);
            });


            modelBuilder.Entity<ApplicationLanguageText>().Property(p => p.Value).HasMaxLength(100);

            modelBuilder.Entity<Facility>(f =>
            {
                f.HasIndex(e => new { e.TenantId });

                f.HasMany(f => f.Wards)
                .WithOne(w => w.FacilityFk)
                .HasForeignKey(w => w.FacilityId);

                f.HasMany(f => f.AssignedStaff)
                    .WithOne(a => a.FacilityFk)
                    .HasForeignKey(a => a.FacilityId);

               f.HasMany(f => f.FacilityBanks)
                .WithOne(b => b.Facility)
                .HasForeignKey(b => b.FacilityId);

                f.HasMany(f => f.Rooms)
                 .WithOne(b => b.FacilityFk)
                .HasForeignKey(b => b.FacilityId);
            });

            modelBuilder.Entity<FacilityGroup>(f =>
            {
                f.HasIndex(f => new { f.TenantId });

                f.HasMany(f => f.ChildFacilities)
                 .WithOne(cf => cf.GroupFk)
                 .HasForeignKey(cf => cf.GroupId);
            });

            modelBuilder.Entity<FacilityType>(f =>
            {
                f.HasIndex(e => new { e.Name });
            });

            
            

            modelBuilder.Entity<JobLevel>(j =>
            {
                j.HasIndex(e => new { e.TenantId });

                j.Property(e => e.IsActive).HasDefaultValue(true);

            });

            modelBuilder.Entity<JobTitle>(j =>
            {
                j.HasIndex(e => new { e.TenantId });

                j.HasMany(e => e.JobLevels)
                .WithOne(e => e.JobTitleFk)
                .HasForeignKey(e => e.JobTitleId);

                j.Property(e => e.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
                b.HasMany(td => td.TenantDocuments)
                    .WithOne(e => e.Tenant)
                    .HasForeignKey(e => e.TenantId);
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("\"IsDeleted\" = false");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.Entity<Vaccine>(b =>
            {
                b.HasMany(v => v.Schedules)
                    .WithOne(s => s.Vaccine)
                    .HasForeignKey(s => s.VaccineId);
            });

            modelBuilder.Entity<VaccineGroup>(b =>
            {
                b.HasMany(vg => vg.Vaccines)
                    .WithOne(v => v.Group)
                    .HasForeignKey(v => v.GroupId);
            });
            
            modelBuilder.Entity<Vaccination>(s =>
            {
                s.HasIndex(e => e.PatientId);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.Vaccinations)
                    .HasForeignKey(s => s.PatientId); 
                 
                s.Property(p => p.VaccineBrand).HasColumnType("varchar(100)");
                s.Property(p => p.VaccineBatchNo).HasColumnType("varchar(50)"); 
                s.Property(p => p.Note).HasColumnType("varchar(2000)");
            });
            
            modelBuilder.Entity<VaccinationHistory>(s =>
            {
                s.HasIndex(e => e.PatientId);
                
                s.HasOne(s => s.Patient)
                    .WithMany(u => u.VaccineHistories)
                    .HasForeignKey(s => s.PatientId); 
                 
                s.Property(p => p.LastVaccineDuration).HasColumnType("varchar(30)"); 
                s.Property(p => p.Note).HasColumnType("varchar(2000)");
            });

            modelBuilder.Entity<Investigation>(s =>
            {
                s.HasMany(i => i.Components)
                    .WithOne(i => i.PartOf)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasMany(i => i.Ranges)
                    .WithOne(i => i.Investigation)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasMany(i => i.Results)
                    .WithOne(i => i.Investigation)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasMany(i => i.Suggestions)
                    .WithOne(i => i.Investigation)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasOne(i => i.Microbiology)
                    .WithOne(i => i.Investigation)
                    .OnDelete(DeleteBehavior.Cascade);
                s.HasMany(i => i.Dipstick)
                    .WithOne(x => x.Investigation)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<DipstickInvestigation>(m =>
            {
                m.HasMany(i => i.Ranges)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DipstickRange>(m =>
            {
                m.HasMany(i => i.Results)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            
            modelBuilder.Entity<Procedure>(s =>
            {
                s.HasIndex(e => e.PatientId);
                  
                s.Property(p => p.Note).HasColumnType("varchar(3000)"); 
            });
            
            modelBuilder.Entity<ProcedureConsentStatement>(s =>
            {
                s.HasIndex(e => e.ProcedureId);
                s.HasIndex(e => e.PatientId);
                 
                s.Property(p => p.SignatureOfNextOfKinOrGuardian).HasColumnType("varchar(250)");
                s.Property(p => p.SignatureOfWitness).HasColumnType("varchar(250)"); 
                s.Property(p => p.SecondaryLanguageOfInterpretation).HasColumnType("varchar(250)"); 
                s.Property(p => p.SecondarySignatureOfNextOfKinOrGuardian).HasColumnType("varchar(250)"); 
                s.Property(p => p.SecondarySignatureOfWitness).HasColumnType("varchar(250)"); 
                s.Property(p => p.IntendedBenefits).HasColumnType("varchar(2000)");  
                
                s.Property(p => p.FrequentlyOccuringRisks).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
                
                s.Property(p => p.ExtraProcedures).HasConversion(v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>(),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())); 
                
                s.Property(p => p.NextOfKinOrGuardianGovIssuedIdNumber).HasColumnType("varchar(100)");
                s.Property(p => p.SignatureOfWitnessGovIssuedIdNumber).HasColumnType("varchar(100)");
                s.Property(p => p.SecondaryNextOfKinOrGuardianGovIssuedIdNumber).HasColumnType("varchar(100)");
                s.Property(p => p.SecondarySignatureOfWitnessGovIssuedIdNumber).HasColumnType("varchar(100)");
            });
            
            modelBuilder.Entity<SpecializedProcedure>(s =>
            {
                s.HasIndex(e => e.ProcedureId);
                s.HasIndex(e => e.SnowmedId); 
                
                s.Property(p => p.Duration).HasColumnType("varchar(20)");
                s.Property(p => p.Time).HasColumnType("varchar(20)");    
                
                s.Property(p => p.RequireAnaesthetist).HasDefaultValue(false);
                s.Property(p => p.IsProcedureInSameSession).HasDefaultValue(false);
            });

            modelBuilder.Entity<VitalSign>(v =>
            {
                v.HasMany(x => x.Ranges).WithOne().OnDelete(DeleteBehavior.Cascade);
                v.HasMany(x => x.Sites).WithOne().OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GCSScoring>(v =>
            {
                v.HasMany(x => x.Ranges).WithOne().OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<ApgarScoring>(v =>
            {
                v.HasMany(x => x.Ranges).WithOne().OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.ConfigurePersistedGrantEntity("App");

            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("App");


            modelBuilder.Entity<Discharge>(s =>
            {
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);

                s.HasIndex(i => i.PatientId);
                s.Property(p => p.AppointmentId);
                s.Property(p => p.IsFinalized);
                s.Property(p => p.IsBroughtInDead);
            });

            modelBuilder.Entity<DischargeMedication>(s =>
            {
                s.HasIndex(e => new { e.TenantId, e.DischargeId, e.MedicationId }).IsUnique(false);

            });

            modelBuilder.Entity<DischargePlanItem>(s =>
            {
                s.HasIndex(e => new { e.TenantId, e.DischargeId, e.PlanItemId }).IsUnique(false);

            });

            modelBuilder.Entity<IntakeOutputCharting>(s =>
            {
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);

            });
            
            modelBuilder.Entity<PhysicalExaminationType>(s =>
            { 
                s.HasIndex(e => e.Type).IsUnique(true);
                s.Property(p => p.Name).HasColumnType("varchar(200)");
                s.Property(p => p.Type).HasColumnType("varchar(200)");
            });
            
            modelBuilder.Entity<NoteTemplate>(s =>
            { 
                s.Property(p => p.NoteTitle).HasColumnType("varchar(200)");
            });
            
            modelBuilder.Entity<PatientPhysicalExamination>(s =>
            { 
                s.HasIndex(e => new { e.TenantId, e.PatientId }).IsUnique(false);
                
                s.Property(p => p.OtherNote).HasColumnType("varchar(2000)");
                
                s.HasMany(x => x.TypeNotes).WithOne().OnDelete(DeleteBehavior.Cascade);
                s.HasMany(x => x.Suggestions).WithOne().OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<PatientPhysicalExamTypeNote>(s =>
            {  
                s.Property(p => p.Type).HasColumnType("varchar(300)");
                s.Property(p => p.Note).HasColumnType("varchar(5000)");
            });
            
            modelBuilder.Entity<PatientPhysicalExamSuggestionQuestion>(s =>
            {  
                s.Property(p => p.HeaderName).HasColumnType("varchar(300)");
            });
            
            modelBuilder.Entity<PatientPhysicalExamSuggestionAnswer>(s =>
            {  
                s.Property(p => p.SnowmedId).HasColumnType("varchar(300)");
                s.Property(p => p.Description).HasColumnType("varchar(500)");
            });
            
            modelBuilder.Entity<PatientPhysicalExamSuggestionQualifier>(s =>
            {  
                s.Property(p => p.Name).HasColumnType("varchar(500)");
            });
            
            modelBuilder.Entity<PatientPhysicalExaminationImageFile>(s =>
            {  
                s.Property(p => p.FileId).HasColumnType("varchar(300)");
                s.Property(p => p.FileName).HasColumnType("varchar(300)");
                s.Property(p => p.FileUrl).HasColumnType("varchar(2000)");
            });

            modelBuilder.Entity<PatientEncounter>(pe =>
            {
                pe.HasOne(x => x.Admission).WithMany();
            });

            modelBuilder.Entity<Admission>(a =>
            {
                a.HasOne(x => x.AdmittingEncounter).WithOne();
            });
        }
    }
}
