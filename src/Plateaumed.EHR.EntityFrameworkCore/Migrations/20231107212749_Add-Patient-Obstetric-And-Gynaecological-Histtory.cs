using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.PatientProfile;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientObstetricAndGynaecologicalHisttory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .Annotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .Annotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .Annotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .Annotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .Annotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .Annotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .Annotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .Annotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .Annotation("Npgsql:Enum:menstrual_flow_type", "Regular,Heavy")
                .Annotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .Annotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .Annotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .Annotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .Annotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .Annotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .Annotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .Annotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .OldAnnotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .OldAnnotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .OldAnnotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .OldAnnotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .OldAnnotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .OldAnnotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .OldAnnotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .OldAnnotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .OldAnnotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .OldAnnotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .OldAnnotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .OldAnnotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .OldAnnotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .OldAnnotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .OldAnnotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected");

            migrationBuilder.CreateTable(
                name: "PatientContraception",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsContraceptionEverUsed = table.Column<bool>(type: "boolean", nullable: false),
                    TypeOfContraceptionUsed = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ContraceptionSnomedId = table.Column<long>(type: "bigint", nullable: true),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsContraceptionInUsed = table.Column<bool>(type: "boolean", nullable: false),
                    IsComplicationExperienced = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientContraception", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientContraception_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientGynaecologicalIllness",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoesPatientHaveSeriousGynaecologicalIllnessHistory = table.Column<bool>(type: "boolean", nullable: false),
                    DoesPatientHaveGynaecologicalSurgicalHistory = table.Column<bool>(type: "boolean", nullable: false),
                    Diagnosis = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    DiagnosisSnomedId = table.Column<long>(type: "bigint", nullable: true),
                    DiagnosisPeriod = table.Column<int>(type: "integer", nullable: false),
                    DiagnosisPeriodUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    ConditionControl = table.Column<ConditionControl>(type: "condition_control", nullable: false),
                    IsCurrentlyOnMedication = table.Column<bool>(type: "boolean", nullable: false),
                    TypeOfMedication = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Dosage = table.Column<int>(type: "integer", nullable: false),
                    DosageUnit = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    PrescriptionFrequency = table.Column<int>(type: "integer", nullable: false),
                    PrescriptionFrequencyUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    IsComplaintWithMedication = table.Column<bool>(type: "boolean", nullable: false),
                    MedicationUsageFrequency = table.Column<int>(type: "integer", nullable: false),
                    MedicationUsageFrequencyUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientGynaecologicalIllness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientGynaecologicalIllness_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientGynaecologicalProcedures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Procedure = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ProcedureSnomedId = table.Column<long>(type: "bigint", nullable: true),
                    ProcedurePeriod = table.Column<int>(type: "integer", nullable: false),
                    ProcedurePeriodUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    IsComplicationExperienced = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientGynaecologicalProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientGynaecologicalProcedures_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMenarcheAndMenopause",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsMenarcheKnown = table.Column<bool>(type: "boolean", nullable: false),
                    IsMenopauseKnown = table.Column<bool>(type: "boolean", nullable: false),
                    MenarcheAge = table.Column<int>(type: "integer", nullable: false),
                    MenopauseAge = table.Column<int>(type: "integer", nullable: false),
                    PostMenopausalSymptoms = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    PostMenopausalSymptomsSnomedId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMenarcheAndMenopause", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientMenarcheAndMenopause_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMenstrualFlows",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPeriodHeavierThanUsual = table.Column<bool>(type: "boolean", nullable: false),
                    IsBloodClotLargerThanRegular = table.Column<bool>(type: "boolean", nullable: false),
                    SanitaryPadUsedPerDay = table.Column<int>(type: "integer", nullable: false),
                    IsHeavyPeriodImpactDayToDayLife = table.Column<bool>(type: "boolean", nullable: false),
                    IsFlowFloodThroughSanitaryTowel = table.Column<bool>(type: "boolean", nullable: false),
                    FlowType = table.Column<MenstrualFlowType>(type: "menstrual_flow_type", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMenstrualFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientMenstrualFlows_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMenstrualPains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPeriodPainInterfereWithDayToDayLife = table.Column<bool>(type: "boolean", nullable: false),
                    IsRecentPeriodMorePainfulThanUsual = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMenstrualPains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientMenstrualPains_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMensurationDurations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastDayOfPeriod = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AveragePeriodDuration = table.Column<int>(type: "integer", nullable: false),
                    IsPeriodPredictable = table.Column<bool>(type: "boolean", nullable: false),
                    AverageCycleLength = table.Column<int>(type: "integer", nullable: false),
                    AverageCycleLengthUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    RequestedTest = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMensurationDurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientMensurationDurations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientProductivePlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoesPatientPlanToGetPregnantSoon = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProductivePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientProductivePlans_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientContraception_PatientId",
                table: "PatientContraception",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGynaecologicalIllness_PatientId",
                table: "PatientGynaecologicalIllness",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGynaecologicalProcedures_PatientId",
                table: "PatientGynaecologicalProcedures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMenarcheAndMenopause_PatientId",
                table: "PatientMenarcheAndMenopause",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMenstrualFlows_PatientId",
                table: "PatientMenstrualFlows",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMenstrualPains_PatientId",
                table: "PatientMenstrualPains",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMensurationDurations_PatientId",
                table: "PatientMensurationDurations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProductivePlans_PatientId",
                table: "PatientProductivePlans",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientContraception");

            migrationBuilder.DropTable(
                name: "PatientGynaecologicalIllness");

            migrationBuilder.DropTable(
                name: "PatientGynaecologicalProcedures");

            migrationBuilder.DropTable(
                name: "PatientMenarcheAndMenopause");

            migrationBuilder.DropTable(
                name: "PatientMenstrualFlows");

            migrationBuilder.DropTable(
                name: "PatientMenstrualPains");

            migrationBuilder.DropTable(
                name: "PatientMensurationDurations");

            migrationBuilder.DropTable(
                name: "PatientProductivePlans");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .Annotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .Annotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .Annotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .Annotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .Annotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .Annotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .Annotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .Annotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .Annotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .Annotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .Annotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .Annotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .Annotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .Annotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .Annotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .Annotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .Annotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .Annotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .Annotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .Annotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .Annotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .Annotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .Annotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .Annotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .Annotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .Annotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .Annotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .Annotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:appointment_repeat_type", "Daily,Weekly,Weekends,Weekdays,Monthly,Annually,Custom")
                .OldAnnotation("Npgsql:Enum:appointment_status_type", "Pending,Executed,Missed,Rescheduled,Not_Arrived,Arrived,Processing,Awaiting_Vitals,Awaiting_Clinician,Awaiting_Doctor,Seen_Doctor,Seen_Clinician,Admitted_To_Ward,Tranferred,Awaiting_Admission")
                .OldAnnotation("Npgsql:Enum:appointment_type", "Walk_In,Referral,Consultation,Follow_Up,Medical_Exam")
                .OldAnnotation("Npgsql:Enum:blood_genotype", "AA,AS,AC,SS,SC")
                .OldAnnotation("Npgsql:Enum:blood_group", "A+,A-,B+,B-,O+,O-,AB+,AB-")
                .OldAnnotation("Npgsql:Enum:blood_group_and_genotype_source", "ClinicalInvestigation,SelfReport")
                .OldAnnotation("Npgsql:Enum:condition_control", "Well_Controlled,Poorly_Controlled")
                .OldAnnotation("Npgsql:Enum:days_of_the_week", "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")
                .OldAnnotation("Npgsql:Enum:facility_level", "Primary,Secondary,Tertiary")
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:insurance_benefiary_type", "Primary,Dependent")
                .OldAnnotation("Npgsql:Enum:insurance_provider_type", "National,State,Private")
                .OldAnnotation("Npgsql:Enum:intensity", "Low,Moderate,High")
                .OldAnnotation("Npgsql:Enum:invoice_cancel_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_item_status", "Unpaid,AwaitingApproval,Paid,Cancelled,Approved,ReliefApplied,Refunded,AwaitCancellationApproval")
                .OldAnnotation("Npgsql:Enum:invoice_refund_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:invoice_source", "AccidentAndEmergency,OutPatient,InPatient,Pharmacy,Lab,Others")
                .OldAnnotation("Npgsql:Enum:invoice_type", "InvoiceCreation,Proforma")
                .OldAnnotation("Npgsql:Enum:marital_status", "Single,Married,Divorced,Widowed,Separated")
                .OldAnnotation("Npgsql:Enum:menstrual_flow_type", "Regular,Heavy")
                .OldAnnotation("Npgsql:Enum:organization_unit_type", "Unit,Department,Facility,Clinic")
                .OldAnnotation("Npgsql:Enum:payment_status", "Unpaid,Paid,PartiallyPaid,Proforma")
                .OldAnnotation("Npgsql:Enum:payment_types", "ServiceOnCredit,Wallet,SplitPayment,Insurance")
                .OldAnnotation("Npgsql:Enum:relationship", "Husband,Wife,Father,Mother,Step-Father,Step-Mother,Son,Daughter,Step-Son,Step-Daughter,Brother,Sister,GrandParent,GrandFather,GrandMother,GrandSon,GrandDaughter,Uncle,Aunt,Cousin,Nephew,Niece,Father-In-Law,Mother-In-Law,Brother-In-Law,Sister-In-Law,Son-In-Law,Daughter-In-Law,Friend,BoyFriend,GirlFriend")
                .OldAnnotation("Npgsql:Enum:religion", "Christianity,Islam,African Traditional Religion,Agnosticism,Atheism,Babism,Bahai Faith,Buddhism,Caodaism,Cheondogyo,Confucianism,Daejongism,Druze,Hinduism,Jainism,Judaism,Mandaeism,Rastafarianism,Ryukuan Religion,Shamanism,Shintoism,Shugendo,Sikhism,Taoism,Yarsanism,Yazdanism,Zoroastrianism")
                .OldAnnotation("Npgsql:Enum:room_type", "OperatingRoom,ConsultingRoom")
                .OldAnnotation("Npgsql:Enum:severity", "Mild,Moderate,Severe")
                .OldAnnotation("Npgsql:Enum:tenant_document_type", "Medical_Degree,Practicing_License")
                .OldAnnotation("Npgsql:Enum:tenant_onboarding_status", "Facility_Details,Documentation,Additional_Details,Departments,Clinics,Wards,Job_Titles_And_Levels,Staff,Review_Details,Finalize,Trial,Checkout")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:transaction_action", "FundWallet,PaidInvoice,CreateInvoice,CreateProforma,ApproveWalletFunding,RejectWalletFunding,CancelledInvoiceItem,ClearInvoice,EditInvoice,RefundAmount,ApproveRefund,RejectRefund,RequestWalletFunding,PaidInvoiceItem,DebtRelief")
                .OldAnnotation("Npgsql:Enum:transaction_source", "Direct,Indirect")
                .OldAnnotation("Npgsql:Enum:transaction_status", "Pending,Approved,Rejected")
                .OldAnnotation("Npgsql:Enum:transaction_type", "Credit,Debit,Other")
                .OldAnnotation("Npgsql:Enum:unit_of_time", "Day,Week,Month,Year")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host")
                .OldAnnotation("Npgsql:Enum:wallet_funding_status", "Pending,Approved,Rejected");
        }
    }
}
